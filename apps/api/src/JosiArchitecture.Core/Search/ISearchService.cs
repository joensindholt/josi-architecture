using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Core.Search;

public interface ISearchService
{
    Task AddAsync(SearchableUser user, CancellationToken cancellationToken);

    Task<IEnumerable<SearchableUser>> QueryUsersAsync(string? orderBy, CancellationToken cancellationToken);
}