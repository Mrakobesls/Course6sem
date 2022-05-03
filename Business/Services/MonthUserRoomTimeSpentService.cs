using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class MonthUserRoomTimeSpentService : GenericService, IMonthUserRoomTimeSpentService
    {
        public MonthUserRoomTimeSpentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public MonthUserRoomTimeSpent Create(MonthUserRoomTimeSpent entity)
        {
            var dbUser = Uow.MonthUserRoomTimeSpents.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public MonthUserRoomTimeSpent Get(int id)
        {
            return Uow.MonthUserRoomTimeSpents.Read(id);
        }

        public List<MonthUserRoomTimeSpent> GetAll()
        {
            return Uow.MonthUserRoomTimeSpents.ReadAll()
                    .Select(c => (MonthUserRoomTimeSpent)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(MonthUserRoomTimeSpent entity)
        {
            Uow.MonthUserRoomTimeSpents.Update(entity);

            Uow.SaveChanges();
        }
    }
}
