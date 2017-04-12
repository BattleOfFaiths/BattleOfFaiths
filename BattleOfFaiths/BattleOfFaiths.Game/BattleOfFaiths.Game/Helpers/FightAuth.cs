using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Models;

namespace BattleOfFaiths.Game.Helpers
{
    public class FightAuth
    {
        private static Fight fight;

        public static void SetCurrentFight(Fight currentFight)
        {
            fight = currentFight;
        }

        public static bool HasStartedFight()
        {
            return fight != null;
        }

        public static Fight GetCurrentFight()
        {
            return fight;
        }

        public static void EndFight()
        {
            fight = null;
        }
    }
}
