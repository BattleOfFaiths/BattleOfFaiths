using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class Item
    {
        public Item()
        {
            this.Games = new HashSet<Game>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
