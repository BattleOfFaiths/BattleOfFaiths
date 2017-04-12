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
        public int Damage { get; set; }
        public int ReturnValue { get;set; }
        public int TakeValue { get; set; }

        public int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual Character Character { get; set; }
        [ForeignKey("ActionAnimation")]
        public int ActionAnimationId { get; set; }
        public virtual ActionAnimation ActionAnimation { get; set; }
    }
}
