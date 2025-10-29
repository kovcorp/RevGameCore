using RevGameCore.Map;

namespace RevGameCore.GameMotor
{
   public class Context
   {
      private readonly IMap map;

      public EventHandler<RoomChangeEventArgs> EnterToRoomEvent;

      public IRoom ActualPlace
      {
         get;
         private set;
      }

      public Context(IMap map)
      {
         this.map = map;
         ActualPlace = this.map.ConnectionsOfRooms.Keys.First();
      }

      public void StepInToRoom(string roomInto)
      {
         var roomToStep = GetRoomByName(roomInto);
         var doorsOfRoom =  GetDoorsOfRoomToStep(roomToStep);

         if (doorsOfRoom.Count > 0)
         {
            ActualPlace = roomToStep;
            EnterToRoomEvent?.Invoke(this, new RoomChangeEventArgs(roomToStep, doorsOfRoom.First()));
         }
         else
         {
            throw new ArgumentException($"Ebbe a szobába: '{roomInto}' innen nem tudsz menni");
         }
      }

      public string ListDoorsInRoom()
      {
         var listDoorsInTheRoom = map.ListDoorsInTheRoom(ActualPlace);
         return string.Join(", ", listDoorsInTheRoom.Select(o => o.WhereTheDoorOpens(ActualPlace).Name).ToList());
      }

      private List<IDoor> GetDoorsOfRoomToStep(IRoom room)
      {
         if (map.ConnectionsOfRooms.TryGetValue(room, out var connections))
         {
            return connections
                .Where(conn => conn.target == ActualPlace)
                .Select(conn => conn.ajto)
                .ToList();
         }
         else
         {
            throw new ArgumentException($"Nincs ilyen szoba vagy nincs hozzá kapcsolódó ajtó: {room}");
         }
      }

      private Room GetRoomByName(string roomName)
      {
         var roomByName = map.ConnectionsOfRooms.Keys.FirstOrDefault(room => room.Name.Equals(roomName, StringComparison.CurrentCultureIgnoreCase));

         if (roomByName == null)
            throw new ArgumentException($"Nincs ilyen szoba: {roomName}");

         return (Room)roomByName;
      }
   }
}
