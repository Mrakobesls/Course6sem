namespace Business.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public bool IsDisabled { get; set; }

        public int RoleId { get; set; }
        public int CurrentRoomId { get; set; }
        public int PositionId { get; set; }
        public List<int> AccessLevelsId { get; set; } = new List<int>();


        public static implicit operator User(Data.Models.User user)
        {
            return user is null
                ? null
                : new User
                {
                    Id = user.Id,
                    Login = user.Login,
                    Email = user.Email,
                    Password = user.Password,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    IsDisabled = user.IsDisabled,
                    RoleId = user.RoleId,
                    CurrentRoomId = user.CurrentRoomId,
                    PositionId = user.PositionId,
                    AccessLevelsId = user.AccessLevels.Select(al => al.Id)
                                            .ToList()
                };
        }


        public static implicit operator Data.Models.User(User user)
        {
            return user is null
                ? null
                : new Data.Models.User
                {
                    Id = user.Id,
                    Login = user.Login,
                    Email = user.Email,
                    Password = user.Password,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    IsDisabled = user.IsDisabled,
                    RoleId = user.RoleId,
                    CurrentRoomId = user.CurrentRoomId,
                    PositionId = user.PositionId
                };
        }
    }
}
