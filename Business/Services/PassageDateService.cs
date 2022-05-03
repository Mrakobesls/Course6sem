using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class PassageDateService : GenericService, IPassageDateService
    {
        public PassageDateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public PassageDate Create(PassageDate entity)
        {
            var dbUser = Uow.PassageDates.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public PassageDate Get(int id)
        {
            return Uow.PassageDates.Read(id);
        }

        public List<PassageDate> GetAll()
        {
            return Uow.PassageDates.ReadAll()
                    .Select(c => (PassageDate)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PassageDate entity)
        {
            Uow.PassageDates.Update(entity);

            Uow.SaveChanges();
        }
    }
}
