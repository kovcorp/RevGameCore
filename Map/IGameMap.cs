/* Interfaces for the map */
namespace RevGameCore.Map
{
   public interface IDoor
   {
      string Description { get; }

      IRoom WhereTheDoorOpens(IRoom roomFrom);
   }

   public interface IRoom
   {
      string Description { get; }
      string Name { get; }
   }

   public interface IMap
   {
      Dictionary<IRoom, List<(IRoom target, IDoor ajto)>> ConnectionsOfRooms { get; }
   }
}
