/* Interfaces for the map */


namespace RevGameCore.Map
{
   public interface IDoor
   {
      string Description { get; }

      List<IRoom> DoorConnectRooms { get; }


      /**
       * <param name="roomFrom"> the room wehre the door is</param>
       * <returns> the room where the door is open</returns>
       */
      IRoom WhereTheDoorOpens(IRoom roomFrom);
   }

   public interface IRoom
   {
      string Description { get; }
      string Name { get; }

      void AddDoor(IDoor door);
      /**
       * <returns> the list of the doors in the room </returns>
       */
      List<IDoor> GetDoors();
   }

      /**
    * Interface to get elements on the map
    */
   public interface IMap
   {
      List<IRoom> Rooms { get; }

      List<IDoor> Doors { get; }
   }
}
