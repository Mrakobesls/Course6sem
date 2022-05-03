namespace Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly AppDbContext AppDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public T Create(T newEntity)
        {
            AppDbContext.Add(newEntity);
            return newEntity;
        }

        public T Read(int id)
        {
            return AppDbContext.Find<T>(id);
        }

        public IQueryable<T> ReadAll()
        {
            return AppDbContext.Set<T>();
        }
        public T Update(T newEnt)
        {
            AppDbContext.ChangeTracker.Clear();
            return AppDbContext.Update(newEnt).Entity;
        }
        public void Delete(int id)
        {
            AppDbContext.Remove(id);
        }
    }
}
