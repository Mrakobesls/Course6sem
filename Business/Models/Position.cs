namespace Business.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator Position(Data.Models.Position position)
        {
            return position is null
                ? null
                : new Position
                {
                    Id = position.Id,
                    Name = position.Name
                };
        }


        public static implicit operator Data.Models.Position(Position position)
        {
            return position is null
                ? null
                : new Data.Models.Position
                {
                    Id = position.Id,
                    Name = position.Name
                };
        }
    }
}