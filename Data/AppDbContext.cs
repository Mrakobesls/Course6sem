using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<AccessLevel> AccessLevel { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<PassageDate> PassageDates { get; set; }
        public DbSet<RoomTimeSpent> RoomTimeSpents { get; set; }
        public DbSet<MonthUserRoomTimeSpent> MonthRoomTimeSpents { get; set; }

        public AppDbContext()
        { }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MRAKOBESPC\\SQLEXPRESS01;DataBase=Course6sem;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomTimeSpent>()
                .HasOne(rts => rts.EnterPassageDate)
                .WithOne(pd => pd.StartTimeRoomSpent)
                .HasForeignKey<RoomTimeSpent>(rts => rts.EnterPassageDateId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RoomTimeSpent>()
                .HasOne(rts => rts.ExitPassageDate)
                .WithOne(pd => pd.EndTimeRoomSpent)
                .HasForeignKey<RoomTimeSpent>(rts => rts.ExitPassageDateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MonthUserRoomTimeSpent>()
                .HasOne(mrts => mrts.User)
                .WithMany(u => u.MonthRoomTimeSpents)
                .HasForeignKey(mrts => mrts.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MonthUserRoomTimeSpent>()
                .HasOne(mrts => mrts.Room)
                .WithMany(u => u.MonthUserRoomTimeSpents)
                .HasForeignKey(mrts => mrts.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasData(new Role[] {
                    new Role
                    {
                        Id = (int)Common.Enum.Roles.Admin,
                        Name = Common.Enum.Roles.Admin.ToString()
                    },
                    new Role
                    {
                        Id = (int)Common.Enum.Roles.Worker,
                        Name = Common.Enum.Roles.Worker.ToString()
                    }
                });

            modelBuilder.Entity<Room>()
                .HasData(new Room[] {
                    new Room
                    {
                        Id = 1,
                        Name = "Улица",
                        Description = "Пространство вне объекта"
                    }
                });

            modelBuilder.Entity<User>()
                    .HasData(new User[] {
                        new User
                        {
                            Id = 1,
                            Login = "Admin",
                            Email = "admin@admin.com",
                            Password = "10000.E1oWoDmucer3gKs31Cd1NA==.acfwsZcyNgPBDPw7KDCwtPp6g7lVZCYfVBMJppZtaQQ=",
                            Name = "Admin",
                            Surname = "Admin",
                            Patronymic = "Admin",
                            CurrentRoomId = 1,
                            RoleId = (int)Common.Enum.Roles.Admin
                        }
                    });
        }
    }
}