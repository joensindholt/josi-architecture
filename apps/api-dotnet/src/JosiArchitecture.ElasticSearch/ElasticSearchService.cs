using System.Text;
using Elastic.Clients.Elasticsearch;
using JosiArchitecture.Core.Search;
using JosiArchitecture.Core.Shared.Extensions;
using Microsoft.Extensions.Options;

namespace JosiArchitecture.ElasticSearch;

public class ElasticSearchService : ISearchService
{
    private const string IndexNameUsers = "users";

    private readonly ElasticsearchClient _client;
    private readonly HttpClient _httpClient;

    public ElasticSearchService(IOptions<ElasticSearchServiceOptions> options)
    {
        _client = new ElasticsearchClient(new Uri(options.Value.Uri!));
        _httpClient = new HttpClient { BaseAddress = new Uri(options.Value.Uri!) };
    }

    public void Initialize()
    {
        if (_client.Indices.Exists(IndexNameUsers).Exists)
        {
            return;
        }

        CreateUsersIndex();
    }

    public async Task AddAsync(SearchableUser user, CancellationToken cancellationToken)
    {
        var response = await _client.IndexAsync(user, IndexNameUsers, cancellationToken);

        if (!response.IsValidResponse)
        {
            throw new SearchProviderException(response.ToString());
        }
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync(IndexNameUsers, id, cancellationToken);

        if (!response.IsValidResponse)
        {
            throw new SearchProviderException(response.ToString());
        }
    }

    public async Task<IEnumerable<SearchableUser>> QueryUsersAsync(string? orderBy, CancellationToken cancellationToken)
    {
        var query = new SearchRequest(IndexNameUsers)
        {
            Size = 10_000
        };

        if (orderBy is not null && orderBy.Length > 0)
        {
            var orderByCapitalized = orderBy.Capitalize();

            if (typeof(SearchableUser).GetProperty(orderByCapitalized) is null)
            {
                throw new SearchProviderException($"Could not find property '{orderByCapitalized}' on User");
            }

            query.Sort = new List<SortOptions>
            {
                SortOptions.Field(orderBy.CamelCase() + ".sort", new FieldSort { Order = SortOrder.Asc }),
            };
        }

        var response = await _client.SearchAsync<SearchableUser>(query, cancellationToken);

        if (!response.IsValidResponse)
        {
            throw new SearchProviderException(response.ToString());
        }

        return response.Documents;
    }

    private void CreateUsersIndex()
    {
        var content = $@"
                {{
                  ""mappings"": {{
                        ""properties"": {{
                            ""{nameof(SearchableUser.Name).CamelCase()}"": {{
                                ""type"": ""keyword"",
                                ""fields"": {{
                                    ""sort"": {{
                                        ""type"": ""icu_collation_keyword"",
                                        ""index"": false,
                                        ""language"": ""da"",
                                        ""country"": ""DK""
                                    }}
                                }}
                            }}
                        }}
                    }}
                }}";

        var putResponse = _httpClient.PutAsync($"/{IndexNameUsers}", new StringContent(content, Encoding.UTF8, "application/json")).Result;

        if (!putResponse.IsSuccessStatusCode)
        {
            throw new Exception(putResponse.Content.ReadAsStringAsync().Result);
        }
    }
}
