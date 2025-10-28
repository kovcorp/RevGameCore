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
         ActualPlace = this.map.Connections.Keys.First();
      }
      public void StepInToRoom(string roomNameYouWantToStep)
      {
         var roomToStep = GetRoomByName(roomNameYouWantToStep);
         var doorsOfRoom = GetDoorsOfRoom(roomToStep);

         if (doorsOfRoom.Count > 0)
         {
            ActualPlace = roomToStep;
            EnterToRoomEvent?.Invoke(this, new RoomChangeEventArgs(roomToStep, doorsOfRoom.First()));
         }
         else
         {
            throw new ArgumentException(String.Format("Ebbe a szobába: '{0}' innen nem tudsz menni", roomNameYouWantToStep));
         }
      }

      public string ListDoorsInRoom()
      {
         map.Connections.TryGetValue(ActualPlace, out var connections);

         return string.Join(", ", connections.Select(o => o.ajto.WhereTheDoorOpens(ActualPlace).Name).ToList());
      }

      private List<IDoor> GetDoorsOfRoom(IRoom room)
      {
         if (map.Connections.TryGetValue(room, out var connections))
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
         var roomByName = map.Connections.Keys.FirstOrDefault(room => room.Name.ToLower() == roomName.ToLower());

         if (roomByName == null)
            throw new ArgumentException(String.Format("Nincs ilyen szoba: {0}", roomName));

         return (Room)roomByName;
      }
   }
}
