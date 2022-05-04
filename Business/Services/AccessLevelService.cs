using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class AccessLevelService : GenericService, IAccessLevelService
    {
        public AccessLevelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public AccessLevel Create(AccessLevel entity)
        {
            var dbUser = Uow.AccessLevels.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public AccessLevel Get(int id)
        {
            return Uow.AccessLevels.Read(id);
        }

        public List<AccessLevel> GetAll()
        {
            return Uow.AccessLevels.ReadAll()
                    .Select(c => (AccessLevel)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AccessLevel entity)
        {
            Uow.AccessLevels.Update(entity);

            Uow.SaveChanges();
        }

        public AccessLevel ReadByName(string name)
        {
            return Uow.AccessLevels.ReadAll()
                        .FirstOrDefault(r => r.Name == name);
        }
    }
}
