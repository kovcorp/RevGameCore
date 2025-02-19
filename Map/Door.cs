namespace RevGameCore.Map
{
   /**
    * Door implementation
    */
   internal class Door(string description, IRoom roomA, IRoom roomB) : IDoor
   {
      public string Description { get; } = description;
      public List<IRoom> DoorConnectRooms { get; } = [roomA, roomB];


      public IRoom WhereTheDoorOpens(IRoom roomFrom)
      {
         foreach (var room in DoorConnectRooms)
         {
            if (!roomFrom.Name.Equals(room.Name))
               return room;
         }
         throw new NotImplementedException();
      }
   }
}
