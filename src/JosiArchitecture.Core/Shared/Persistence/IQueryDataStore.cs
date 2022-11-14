using JosiArchitecture.Core.Todos;
using System.Linq;

namespace JosiArchitecture.Core.Shared.Persistence
{
    /// <summary>
    /// The IQueryDataStore is used only for queries...hence exposing IQueryables
    /// Commands uses repositories
    /// </summary>
    public interface IQueryDataStore
    {
        IQueryable<TodoList> TodoLists { get; }
    }
}
