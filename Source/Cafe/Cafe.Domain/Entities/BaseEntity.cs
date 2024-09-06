using Cafe.SharedKernel;
using Cafe.SharedKernel.Util;

namespace Cafe.Domain.Entities;

/// <summary>
/// Base entity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the domain events.
    /// </summary>
    /// <value>
    /// Domain Events.
    /// </value>
    private readonly List<IDomainEvent> domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    protected BaseEntity()
    {
    }

    /// <summary>
    /// Gets the domain events. This collection is readonly.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

    /// <summary>
    /// Clears all the domain events from the <see cref="AggregateRoot"/>.
    /// </summary>
    public void ClearDomainEvents() => this.domainEvents.Clear();

    /// <summary>
    /// raise domain events
    /// </summary>
    /// <param name="domainEvent"> domain event</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => this.domainEvents.Add(domainEvent);
}
