using RevGameCore.Map;

namespace RevGameCore.GameMotor
{
   public class StoryTeller
   {
      private Context _context;
      private Thread _storyThread;
      private bool _running;

      public StoryTeller(Context context)
      {
         _context = context;
         _context.EnterToRoomEvent += EnterToRoomHandler;
      }


      public void Start()
      {
         _storyThread = new Thread(Run);
         _storyThread.Start();
      }

      public void Stop()
      {
         _running = false;
         _storyThread.Join();
      }

      private void Run()
      {
         while (_running)
         {
            Thread.Sleep(100);
         }
      }

      private void EnterToRoomHandler(object? sender, RoomChangeEventArgs e)
      {
         var eViaDoor = e.ViaDoor;
         // present actual palce 
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine("Beléptél az ajtón: " + eViaDoor.Description);
         Console.WriteLine(_context.ActualPlace.Description);



         Console.ForegroundColor = ConsoleColor.Gray;
      }
   }

   public class RoomChangeEventArgs(IRoom roomInto, IDoor viaDoor) : EventArgs
   {
      public IRoom RoomInto { get; } = roomInto;
      public IDoor ViaDoor { get; } = viaDoor;

   }
}
