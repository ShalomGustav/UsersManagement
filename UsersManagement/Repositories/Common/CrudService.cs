namespace UsersManagement.Repositories.Common;

public abstract class CrudService<TModel,TEntity> 
    where TModel : Entity 
    where TEntity : Entity, IDateEntity<TEntity,TModel>
{
    protected readonly Func<IRepository> _repositoryFactory;


    public CrudService(Func<IRepository> repositoryFactory)//почитать про фабрики
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<List<TModel>> GetAsync(string[] ids)
    {
        var models = new List<TModel>();
        using(var repository = _repositoryFactory())
        {
            var entities = await LoadEntities(repository, ids);

            foreach(var entity in entities)
            {
                var model = entity.ToModel(entity as TModel);
                if (model != null)
                {
                    models.Add(model);
                }
            }
        }

        return models;
    }

    //protected virtual Task<List<TEntity>> LoadEntities(
    //    IRepository repository,
    //    IEnumerable<string> ids)
    //{
    //    return LoadEntities(repository, ids);
    //}

    protected abstract Task<TEntity[]> LoadEntities(
        IRepository repository,
        IEnumerable<string> ids);
    

    public async Task<TModel> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        var entities = await GetAsync(new[] { id });

        return entities.FirstOrDefault();
    }

    public async Task SaveChangesAsync(List<TModel> models)
    {

    }

    public async Task DeleteAsync(List<TModel> ids)
    {

    }


}
