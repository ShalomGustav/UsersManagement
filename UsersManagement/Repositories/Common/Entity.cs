namespace UsersManagement.Repositories.Common;

public abstract class Entity : IEntity
{
    public string Id { get; set; }

    public bool IsTransient()
    {
        return Id == null;
    }
}
