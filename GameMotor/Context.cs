using RevGameCore.Map;

namespace RevGameCore.GameMotor
{
   public class Context
   {
      private List<IRoom> _rooms;
      private List<IDoor> _doors;

      public EventHandler<RoomChangeEventArgs> EnterToRoomEvent;

      public IRoom ActualPlace { get; private set; }

      public Context(List<IRoom> rooms, List<IDoor> doors)
      {
         _rooms = rooms;
         _doors = doors;
         ActualPlace = _rooms[0];
      }

      public void StepInToRoom(string roomNameYouWantToStep)
      {
         var roomToStep = GetRoomByName(roomNameYouWantToStep);

         var door = ActualPlace.GetDoors().Where(door => door.DoorConnectRooms.Contains(roomToStep)).ToList();

         if (door.Count > 0)
         {
            ActualPlace = roomToStep;
            EnterToRoomEvent?.Invoke(this, new RoomChangeEventArgs(roomToStep, door[0]));
         }
         else
         {
            throw new ArgumentException(String.Format("Ebbe a szobába: '{0}' innen nem tudsz menni", roomNameYouWantToStep));
         }
      }

      public string ListDoorsInRoom()
      {
         return string.Join(", ", ActualPlace.GetDoors().Select(o => o.WhereTheDoorOpens(ActualPlace).Name).ToList());
      }


      private IRoom? GetRoomByName(string roomName)
      {
         var roomByName = _rooms.Find(e => e.Name.ToLower().Equals(roomName.ToLower()));

         if (roomByName == null)
            throw new ArgumentException(String.Format("Nincs ilyen szoba: {0}", roomName));

         return roomByName;
      }
   }
}
