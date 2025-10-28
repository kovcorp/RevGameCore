namespace RevGameCore.Map
{
   public class GameMap : IMap
   {
      public Dictionary<IRoom, List<(IRoom target, IDoor ajto)>> Connections { get; set; } = new();

      private GameMap()
      { }

      public static MapBuilder GetBuilder()
      {
         return new MapBuilder();
      }

      public class MapBuilder
      {
         private readonly IMap myMap = new GameMap();

 

         public  void BuildConnections(Room room, List<(IRoom targetRoom, IDoor ajto)> cons)
         {
            if (!((GameMap)myMap).Connections.ContainsKey(room))
            {
               ((GameMap)myMap).Connections.Add(room, cons);
            }
            else
            {
               ((GameMap)myMap).Connections[room].AddRange(cons);
            }
         }

 
         public IMap Build()
         {
            return myMap;
         }

      }
   }
}
