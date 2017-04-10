using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class Game
    {
        public Game()
        {
            this.Characters = new HashSet<Character>();
            this.Items = new HashSet<Item>();
        }
        public int Id { get; set; }
        public int HighScore { get; set; }
        public int Money { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
