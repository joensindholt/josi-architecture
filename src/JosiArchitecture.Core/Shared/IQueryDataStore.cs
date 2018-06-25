using System.Linq;
using JosiArchitecture.Core.Contacts;
using JosiArchitecture.Core.Todos;

namespace JosiArchitecture.Core.Shared
{
    /// <summary>
    /// The IQueryDataStore is used only for queries...hence exposing IQueryables
    /// Commands uses repositories
    /// </summary>
    public interface IQueryDataStore
    {
        IQueryable<Contact> Contacts { get; }

        IQueryable<Todo> Todos { get; }
    }
}