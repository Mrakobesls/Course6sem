namespace Business.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator Room(Data.Models.Room room)
        {
            return room is null
                ? null
                : new Room
                {
                    Id = room.Id,
                    Name = room.Name,
                    Description = room.Description
                };
        }

        public static implicit operator Data.Models.Room(Room room)
        {
            return room is null
                ? null
                : new Data.Models.Room
                {
                    Id = room.Id,
                    Name = room.Name,
                    Description = room.Description
                };
        }
    }
}