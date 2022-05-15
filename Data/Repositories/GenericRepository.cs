using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly AppDbContext AppDbContext;

        public GenericRepository()
        { }

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
            AppDbContext.Remove(Read(id));
        }

        public virtual void DbCrouch(string serializedAccessLevels)
        {
        }

        protected List<T> DbEntities(string serializedAccessLevels)
        {
            return JsonSerializer.Deserialize<List<T>>(serializedAccessLevels);
        }

        protected void DbOverride(List<T> entities)
        {
            using var transaction = AppDbContext.Database.BeginTransaction();

            var tName = typeof(T).Name;

            AppDbContext.Database.ExecuteSqlRaw($"DELETE FROM [{tName}]");

            foreach (var entiry in entities)
            {
                Create(entiry);
            }

            AppDbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [dbo].[{tName}] ON;");
            AppDbContext.SaveChanges();
            AppDbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [dbo].[{tName}] OFF;");

            AppDbContext.ChangeTracker.Clear();
            transaction.Commit();
        }
    }
}
