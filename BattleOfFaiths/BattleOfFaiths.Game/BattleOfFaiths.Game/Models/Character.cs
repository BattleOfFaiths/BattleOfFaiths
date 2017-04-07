using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class Character
    {
        public Character()
        {
            this.Attacks = new HashSet<Attack>();
            this.Characteristics = new HashSet<Characteristics>();
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }

        public virtual ICollection<Attack> Attacks { get; set; }
        public virtual ICollection<Characteristics> Characteristics { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
