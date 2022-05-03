namespace Data.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        T Create(T newEntity);
        T Read(int id);
        IQueryable<T> ReadAll();
        T Update(T newUser);
        void Delete(int id);
    }
}
