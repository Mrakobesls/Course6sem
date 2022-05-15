using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public  class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<Role> DbRead()
        {
            return ReadAll()
                    .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new Role
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList();
            DbOverride(entities);
        }
    }
}
