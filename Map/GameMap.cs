namespace RevGameCore.Map
{
   using System.Text.Json;

   public class GameMap : IMap
   {
      public Dictionary<IRoom, List<(IRoom target, IDoor ajto)>> ConnectionsOfRooms { get; } = new();

      private GameMap()
      { }

      public List<IDoor> ListDoorsInTheRoom(IRoom room)
      {
         if (!ConnectionsOfRooms.TryGetValue(room, out var connections))
            return [];

         return connections.Select(c => c.ajto).ToList();
      }

      public static MapBuilder GetBuilder()
      {
         return new MapBuilder();
      }

      public class MapBuilder
      {
         private readonly IMap myMap = new GameMap();

         public void BuildConnections(Room room, List<(IRoom targetRoom, IDoor ajto)> cons)
         {
            if (!myMap.ConnectionsOfRooms.TryAdd(room, cons))
            {
               myMap.ConnectionsOfRooms[room].AddRange(cons);
            }
         }

         public IMap Build()
         {
            loadJ();
            return myMap;
         }

         private void loadJ()
         {
            string path = @"..\..\..\Map\MapData.json";
            var jsonString = File.ReadAllText(path);
            var mapData = JsonSerializer.Deserialize<RoomsData>(jsonString);

         }

      }
   }

   class RoomData
   { 
      public string Name { get; set; }
      public string Description { get; set; }
   }

   class RoomsData
   {
      public List<RoomData> Rooms { get; set; }
   }

}
