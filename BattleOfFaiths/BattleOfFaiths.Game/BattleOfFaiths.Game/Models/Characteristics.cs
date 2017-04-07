using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BattleOfFaiths.Game.Models
{
    public class Characteristics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual Character Character { get; set; }
    }
}
