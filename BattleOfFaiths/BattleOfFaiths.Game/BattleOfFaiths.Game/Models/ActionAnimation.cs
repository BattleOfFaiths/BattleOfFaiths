using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class ActionAnimation
    {
        public int Id { get; set; }
        public string Sprite { get; set; }
        public virtual CharacterAction CharacterAction { get; set; }
    }
}
