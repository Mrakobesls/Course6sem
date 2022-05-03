using Data;
using Data.Model;
using Data.Repositories;

namespace ShemTeh.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<AccessLevel> AccessLevels { get; }
        IGenericRepository<Checkpoint> Checkpoints { get; }
        IGenericRepository<MonthUserRoomTimeSpent> MonthUserRoomTimeSpents { get; }
        IGenericRepository<PassageDate> PassageDates { get; }
        IGenericRepository<Position> Positions { get; }
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<Room> Rooms { get; }
        IGenericRepository<RoomTimeSpent> RoomTimeSpents { get; }
        IGenericRepository<User> Users { get; }

        void SaveChanges();
        AppDbContext GetContext();
        void DetachAllEntities();
    }
}
