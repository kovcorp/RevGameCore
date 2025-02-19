namespace RevGameCore.Map
{
   public class Room(string name, string description) : IRoom
   {
      public string? Description { get; } = description;

      public string Name { get; } = name;

      // connection door to a room 
      private List<IDoor> _doors = new List<IDoor>
         { Capacity = 0 };

      public void AddDoor(IDoor door)
      {
         _doors.Add(door);
      }

      public List<IDoor> GetDoors()
      {
         return _doors;
      }
   }
}
