namespace JosiArchitecture.Core.Shared.Model
{
    /// <summary>
    /// Aggregate roots are the ones stored and fetched from the datastore
    /// whereas entities are children of aggregate roots and must be reference through these
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
    }
}