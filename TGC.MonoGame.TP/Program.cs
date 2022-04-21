using System;

namespace TGC.MonoGame.TP
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameShip())
                game.Run();
        }
    }
}
