namespace Business.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(Data.Models.Role role)
        {
            return role is null
                ? null
                : new Role
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }


        public static implicit operator Data.Models.Role(Role roleDto)
        {
            return roleDto is null
                ? null
                : new Data.Models.Role
                {
                    Id = roleDto.Id,
                    Name = roleDto.Name
                };
        }
    }
}
