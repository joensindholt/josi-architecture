using System.Linq;
using JosiArchitecture.Core.Todos;

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