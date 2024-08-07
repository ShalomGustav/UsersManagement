namespace UsersManagement.Repositories.Common;

public abstract class CrudService<TModel,TEntity> 
    where TModel : Entity 
    where TEntity : Entity, IDateEntity<TEntity,TModel>
{
    protected readonly Func<IRepository> _repositoryFactory;

    public CrudService(Func<IRepository> repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<List<TModel>> GetAsync(IEnumerable<string> ids)
    {
        var models = new List<TModel>();
        using(var repository = _repositoryFactory())
        {
            var entities = await LoadEntities(repository, ids);

            foreach(var entity in entities)
            {
                var model = entity.ToModel(AbstractTypeFactory<TModel>.TryCreateInstance());
                if (model != null)
                {
                    models.Add(model);
                }
            }
        }

        return models;
    }

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

    public async Task SaveChangesAsync(IEnumerable<TModel> models)
    {
        if(models == null)
        {
            throw new ArgumentNullException(nameof(models));
        }

        using (var repository = _repositoryFactory())
        {
            var existEntities = await LoadEntities(repository,
                models.Where(x => !x.IsTransient()).Select(x => x.Id).ToArray());

            foreach(var model in models)
            {
                var originalEntity = existEntities.FirstOrDefault(x => x.Id == model.Id);
                var modifyEntity = AbstractTypeFactory<TEntity>.TryCreateInstance().FromModel(model);
                if(originalEntity != null)
                {
                    modifyEntity.Patch(originalEntity);
                }
                else
                {
                    repository.Add(modifyEntity);
                }
            }

            await repository.UnitOfWork.CommitAsync();
        }
    }

    public async Task DeleteAsync(IEnumerable<string> ids)
    {
        var models = await GetAsync(ids.ToArray());

        using(var repository = _repositoryFactory())
        {
            foreach(var model in models)
            {
                var entity = AbstractTypeFactory<TEntity>.TryCreateInstance().FromModel(model);
                repository.Remove(entity);
            }
            await repository.UnitOfWork.CommitAsync();
        }
    }
}
