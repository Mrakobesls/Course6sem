using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class RoleService : GenericService, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Role Create(Role entity)
        {
            var dbUser = Uow.Roles.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public Role Get(int id)
        {
            return Uow.Roles.Read(id);
        }

        public List<Role> GetAll()
        {
            return Uow.Roles.ReadAll()
                    .Select(c => (Role)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            Uow.Roles.Update(entity);

            Uow.SaveChanges();
        }
    }
}
