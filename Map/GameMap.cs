namespace RevGameCore.Map
{
   public class GameMap : IMap
   {
      public Dictionary<IRoom, List<(IRoom target, IDoor ajto)>> ConnectionsOfRooms { get; } = new();

      private GameMap()
      { }

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
            return myMap;
         }

      }
   }
}
