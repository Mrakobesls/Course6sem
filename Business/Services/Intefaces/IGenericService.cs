namespace Business.Services
{
    public interface IGenericService<T>
        where T : class
    {
        T Create(T entity);
        T Get(int id);
        List<T> GetAll();
        void Update(T entity);
        void Delete(int id);
    }
}
