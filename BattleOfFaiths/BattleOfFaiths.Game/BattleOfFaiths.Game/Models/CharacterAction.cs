using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class CharacterAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; } 
        public string PlayerSprite { get; set; }
        public string EnemySprite { get; set; }
        public int Frames { get; set; }

        public int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual Character Character { get; set; }
      
    }
}
