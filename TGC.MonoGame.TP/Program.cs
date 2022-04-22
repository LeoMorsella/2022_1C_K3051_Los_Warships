using System;
using TGC.MonoGame.Samples.Samples.Heightmaps;


namespace TGC.MonoGame.TP
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameShips())
                game.Run();
        }
    }
}
