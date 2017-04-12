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
            this.CharacterActions = new HashSet<CharacterAction>();          
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }
        public string EnemySprite { get; set; }
        public int Level { get; set; }
        public int Highscore { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int SpecAttack { get; set; }

        public virtual ICollection<CharacterAction> CharacterActions { get; set; }        
        public virtual ICollection<Game> Games { get; set; }
    }
}
