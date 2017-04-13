using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class Fight
    {
        public int Id { get; set; }
        public int playerHealth { get; set; }
        public int playerMana { get; set; }
        public int playerAtkDmg { get; set; }
        public int playerSpAtkDmg { get; set; }
        public int enemyHealth { get; set; }
        public int enemyMana { get; set; }
        public int enemyAtkDmg { get; set; }
        public int enemySpAtkDmg { get; set; }

        public int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual Character Character { get; set; }
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
