using System;

namespace InteractivePiano
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new InteractivePianoGame())
                game.Run();
        }
    }
}
