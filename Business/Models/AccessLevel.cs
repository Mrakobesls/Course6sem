namespace Business.Models
{
    public class AccessLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator AccessLevel(Data.Models.AccessLevel accessLevel)
        {
            return accessLevel is null
                ? null
                : new AccessLevel
                {
                    Id = accessLevel.Id,
                    Name = accessLevel.Name
                };
        }


        public static implicit operator Data.Models.AccessLevel(AccessLevel accessLevel)
        {
            return accessLevel is null
                ? null
                : new Data.Models.AccessLevel
                {
                    Id = accessLevel.Id,
                    Name = accessLevel.Name
                };
        }
    }
}