// --------------------------------------------------------------------------------------------------------------------
// Context of the Game 
// --------------------------------------------------------------------------------------------------------------------

namespace RevGameCore.GameMotor
{
   using RevGameCore.Map;

   public class StoryTeller
   {
      #region Constants and Fields

      private bool running;

      private Thread storyThread;

      #endregion

      #region Constructors and Destructors

      public StoryTeller(Context context)
      {
         context.EnterToRoomEvent += EnterToRoomHandler;
      }

      #endregion

      #region Public Methods and Operators

      public void Start()
      {
         storyThread = new Thread(Run);
         storyThread.Start();
      }

      public void Stop()
      {
         running = false;
         storyThread.Join();
      }

      #endregion

      #region Methods

      private void EnterToRoomHandler(object? sender, RoomChangeEventArgs e)
      {
         var eViaDoor = e.ViaDoor;
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine("Beléptél az ajtón: " + eViaDoor.Description);
         Console.WriteLine(e.RoomInto.Description);

         Console.ForegroundColor = ConsoleColor.Gray;
      }

      private void Run()
      {
         while (running)
         {
            Thread.Sleep(100);
         }
      }

      #endregion
   }

   public class RoomChangeEventArgs(IRoom roomInto, IDoor viaDoor) : EventArgs
   {
      #region Public Properties

      public IRoom RoomInto { get; } = roomInto;

      public IDoor ViaDoor { get; } = viaDoor;

      #endregion
   }
}