using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public  class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<User> DbRead()
        {
            return ReadAll()
                    .Include(u => u.AccessLevels)
                    .Include(u => u.Position)
                    .Include(u => u.CurrentRoom)
                    .Include(u => u.Role)
                    .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new User
                {
                    Id = e.Id,
                    Login = e.Login,
                    Email = e.Email,
                    Password = e.Password,
                    Name = e.Name,
                    Surname = e.Surname,
                    Patronymic = e.Patronymic,
                    IsDisabled = e.IsDisabled,
                    Role = AppDbContext.Roles.Find(e.Role.Id),
                    Position = AppDbContext.Positions.Find(e.Position.Id),
                    CurrentRoom = AppDbContext.Rooms.Find(e.CurrentRoom.Id),
                    AccessLevels = e.AccessLevels.Select(al
                                    => AppDbContext.AccessLevels.Find(al.Id))
                                        .ToList(),
                }).ToList();
            DbOverride(entities);
        }
    }
}
