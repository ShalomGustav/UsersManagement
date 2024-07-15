namespace UsersManagement.Repositories.Common;

public interface IDateEntity<TEntity, TModel>
{
    public TModel ToModel(TModel model);

    public TEntity FromModel(TModel model);

    public void Patch(TEntity target);
}
