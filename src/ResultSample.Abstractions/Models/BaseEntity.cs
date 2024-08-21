namespace ResultSample.Abstractions.Models;

public abstract class BaseEntity : ErrorObject
{
    protected BaseEntity() : base()
    { }

    protected BaseEntity(Guid id) : base()
    {
        Id = id;
    }

    public Guid Id { get; protected set; }
}