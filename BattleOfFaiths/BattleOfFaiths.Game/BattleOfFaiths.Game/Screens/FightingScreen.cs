using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Components;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Screens
{
    public class FightingScreen
    {
        private Character character;
        private List<Attack> attacks;

        private List<Item> items;
        private Map map;
        private List<Bar> Bars;
        private Bar playerHealthBar, enemyHealthBar, playerManaBar, enemyManaBar;

        public void Initialize()
        {
            map.Initialize();
        }

        public void LoadContent(ContentManager Content)
        {
            map.LoadContent(Content);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
        }
    }
}
