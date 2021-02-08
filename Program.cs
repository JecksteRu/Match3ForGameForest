using System;

namespace Match3Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MatchGame())
                game.Run();
        }
    }

}
