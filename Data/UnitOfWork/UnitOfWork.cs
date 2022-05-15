using Data.Models;
using Data.Repositories;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AppDbContext _context;


        private IGenericRepository<Role> _roles;
        public IGenericRepository<Role> Roles
            => _roles ??= new RoleRepository(_context);

        private IGenericRepository<Position> _positions;
        public IGenericRepository<Position> Positions
            => _positions ??= new PositionRepository(_context);

        private IGenericRepository<AccessLevel> _accessLevel;
        public IGenericRepository<AccessLevel> AccessLevels
            => _accessLevel ??= new AccessLevelRepository(_context);

        private IGenericRepository<Checkpoint> _checkpoints;
        public IGenericRepository<Checkpoint> Checkpoints
            => _checkpoints ??= new CheckpointRepository(_context);

        private IGenericRepository<Room> _rooms;
        public IGenericRepository<Room> Rooms
            => _rooms ??= new RoomRepository(_context);

        private IGenericRepository<User> _users;
        public IGenericRepository<User> Users
            => _users ??= new UserRepository(_context);

        private IGenericRepository<MonthUserRoomTimeSpent> _monthUserRoomTimeSpents;
        public IGenericRepository<MonthUserRoomTimeSpent> MonthUserRoomTimeSpents
            => _monthUserRoomTimeSpents ??= new MonthUserRoomTimeSpentRepository(_context);

        private IGenericRepository<PassageDate> _passageDates;
        public IGenericRepository<PassageDate> PassageDates
            => _passageDates ??= new PassageDateRepository(_context);

        private IGenericRepository<RoomTimeSpent> _roomTimeSpent;
        public IGenericRepository<RoomTimeSpent> RoomTimeSpents
            => _roomTimeSpent ??= new RoomTimeSpentRepository(_context);


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public AppDbContext GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void DetachAllEntities()
        {
            _context.ChangeTracker.Clear();
        }

        AppDbContext IUnitOfWork.GetContext()
        {
            return _context;
        }
    }
}
