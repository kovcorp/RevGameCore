#region Init

using RevGameCore.GameMotor;
using RevGameCore.Map;

var map = InitMap();
var gameContext = new Context(map.Rooms, map.Doors);

StoryTeller storyTeller = new StoryTeller(gameContext);
//ShowStartDemo();
#endregion

#region GameCycle

Console.WriteLine("A játék elkezdődött!\n");
ShowActualPlaceDescription();
ListDoors();
storyTeller.Start();

while (true)
{
   Console.ForegroundColor = ConsoleColor.DarkRed;
   Console.WriteLine("\nHova akarsz menni?");
   Console.ForegroundColor = ConsoleColor.Gray;

   var roomNameToGo = Console.ReadLine();

   Console.Clear();

   if (roomNameToGo != null && roomNameToGo.Equals("xx"))
   {
      Console.WriteLine("===== Good By! ======");
      storyTeller.Stop();
      return;
   }

   try
   {
      gameContext.StepInToRoom(roomNameToGo);
   }
   catch (ArgumentException e)
   {
      Console.ForegroundColor = ConsoleColor.Red;
      //Console.WriteLine("Nincs ilyen hely: " + roomNameToGo);
      Console.WriteLine(e.Message);
      Console.ForegroundColor = ConsoleColor.Gray;
      ShowActualPlaceDescription();
   }

   ListDoors();

}

#endregion
IMap InitMap()
{
   var mapBuilder = GameMap.GetBuilder();

   mapBuilder.AddRooms([
      new Room("A", "Az A szobában vagy, nincs itt semmi"),
      new Room("B", "A B szobában vagy csak egy virág van itt"),
      new Room("C", "A C szoba egy átjáró"),
      new Room("D", "A D szobában vagy, üres csak tovább vagy vissza mehetsz"),
      new Room("E", "Az E szobában vagy egy törött boros üveg van a padlón"),
      new Room("F", "Az F szobában vagy ez zsákutca")]);

   mapBuilder.ConnectRoomsByName("A", "B")
      .ConnectRoomsByName("A", "D")
      .ConnectRoomsByName("B", "F")
      .ConnectRoomsByName("B", "C")
      .ConnectRoomsByName("C", "E")
      .ConnectRoomsByName("E", "D");

   return mapBuilder.Build();
}

void ShowStartDemo()
{
   const string title = "REVENGE";
   Console.ForegroundColor = ConsoleColor.Red;

   for (var i = 0; i < 7; i++)
   {
      Thread.Sleep(600);
      Console.Write("=[{0}]=", title[i]);
   }
   Thread.Sleep(800);

   Console.ForegroundColor = ConsoleColor.Gray;
   Console.Clear();
}

void ListDoors()
{
   Console.ForegroundColor = ConsoleColor.Cyan;
   Console.WriteLine("\t|> Ajtók a következő helyiségekbe: " + gameContext.ListDoorsInRoom());
   Console.ForegroundColor = ConsoleColor.Gray;
}

void ShowActualPlaceDescription()
{
   Console.ForegroundColor = ConsoleColor.Yellow;
   Console.WriteLine(gameContext.ActualPlace.Description);
   Console.ForegroundColor = ConsoleColor.Gray;
}

