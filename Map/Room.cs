namespace RevGameCore.Map
{
   public class Room(string name, string description) : IRoom
   {
      public string Description { get; } = description;

      public string Name { get; } = name;

   }
}
