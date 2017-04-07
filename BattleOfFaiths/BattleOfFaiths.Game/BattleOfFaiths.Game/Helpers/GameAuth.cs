using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Helpers
{
    public class GameAuth
    {
        private static Models.Game game;

        public static void SetCurrentGame(Models.Game currentGame)
        {
            game = currentGame;
        }

        public static bool HasStartedGame()
        {
            return game != null;
        }

        public static Models.Game GetCurrentGame()
        {
            return game;
        }

        public static void EndGame()
        {
            game = null;
        }
    }
}
