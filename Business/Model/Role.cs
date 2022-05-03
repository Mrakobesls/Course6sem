namespace Business.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(Data.Model.Role role)
        {
            return role is null
                ? null
                : new Role
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }


        public static implicit operator Data.Model.Role(Role roleDto)
        {
            return roleDto is null
                ? null
                : new Data.Model.Role
                {
                    Id = roleDto.Id,
                    Name = roleDto.Name
                };
        }
    }
}
