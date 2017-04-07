using System;
using BattleOfFaiths.Game.Data;

namespace BattleOfFaiths.Game
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                context.Database.Initialize(true);
            }
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

