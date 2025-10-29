#region Init

using RevGameCore.GameMotor;
using RevGameCore.Map;

var map = InitMap();
var gameContext = new Context(map);

var storyTeller = new StoryTeller(gameContext);
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

   if (roomNameToGo is "xx")
   {
      Console.WriteLine("===== Good By! ======");
      storyTeller.Stop();
      return;
   }

   try
   {
      if (roomNameToGo != null)
         gameContext.StepInToRoom(roomNameToGo);
   }
   catch (ArgumentException e)
   {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(e.Message);
      Console.ForegroundColor = ConsoleColor.Gray;
      ShowActualPlaceDescription();
   }

   ListDoors();

}

#endregion

static IMap InitMap()
{
   var roomA = new Room("A", "Az A szobában vagy, nincs itt semmi");
   var roomB = new Room("B", "A B szobában vagy csak egy virág van itt");
   var roomC = new Room("C", "A C szoba egy átjáró");
   var roomD = new Room("D", "A D szobában vagy, üres csak tovább vagy vissza mehetsz");
   var roomE = new Room("E", "Az E szobában vagy egy törött boros üveg van a padlón");
   var roomF = new Room("F", "Az F szobában vagy ez zsákutca");

   var mapBuilder = GameMap.GetBuilder();


   mapBuilder.BuildConnections(roomA, [
      (roomB, new Door("A-B", roomA, roomB)),
      (roomD, new Door("A-D", roomA, roomD))
   ]);
   mapBuilder.BuildConnections(roomB, [
      (roomA, new Door("B-A", roomB, roomA)),
      (roomF, new Door("B-F", roomB, roomF)),
      (roomC, new Door("B-C", roomB, roomC))
   ]);
   mapBuilder.BuildConnections(roomC, [
      (roomB, new Door("C-B", roomC, roomB)),
      (roomE, new Door("C-E", roomC, roomE))
   ]);
   mapBuilder.BuildConnections(roomD, [
      (roomA, new Door("D-A", roomD, roomA)),
      (roomE, new Door("D-E", roomD, roomE))
   ]);
   mapBuilder.BuildConnections(roomE, [
      (roomC, new Door("E-C", roomE, roomC)),
      (roomD, new Door("E-D", roomE, roomD))
   ]);
   mapBuilder.BuildConnections(roomF, [
      (roomB, new Door("F-B", roomF, roomB))
   ]);

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

