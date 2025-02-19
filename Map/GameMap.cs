namespace RevGameCore.Map
{
   public class GameMap : IMap
   {
      public List<IRoom> Rooms { get; set; } = [];
      public List<IDoor> Doors { get; set; } = [];

      private GameMap()
      { }

      public static MapBuilder GetBuilder()
      {
         return new MapBuilder();
      }

      public class MapBuilder
      {
         private readonly IMap myMap = new GameMap();

         public MapBuilder AddRooms(List<IRoom> rooms)
         {
            myMap.Rooms.AddRange(rooms);
            return this;
         }

         public MapBuilder ConnectRoomsByName(string roomName, string otherRoomNAme)
         {
            var room = GetRoomByName(roomName);
            var otherRoom = GetRoomByName(otherRoomNAme);

            if (room != null && otherRoom != null)
            {
               IDoor d = new Door(room.Name + " - " + otherRoom.Name, room, otherRoom);
               myMap.Doors.Add(d);
               room.AddDoor(d);
               otherRoom.AddDoor(d);
            }
            else
               throw new ArgumentException("oops, no room ... S: ");

            return this;
         }

         public IMap Build()
         {
            return myMap;
         }

         private IRoom? GetRoomByName(string roomName)
         {
            return myMap.Rooms.Find(e => e.Name.ToLower().Equals(roomName.ToLower()));
         }
      }
   }
}
