using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Components
{
    public class DrawNames
    {
        private SpriteFont font;

        private Character fighter;
        private Character enemy;

        private string fighterName;
        private Vector2 fighterNamePos;
        private string enemyName;
        private Vector2 enemyNamePos;

        public DrawNames(Character figter, Character enemy)
        {
            this.fighter = figter;
            this.enemy = enemy;
        }

        public int screenWidth
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; }
        }

        public int screenHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }

        public void Initialize()
        {
            fighterName = fighter.Name;
            enemyName = enemy.Name;

            fighterNamePos = new Vector2(50, screenHeight - 50);
            enemyNamePos = new Vector2(screenWidth - 120, screenHeight - 50);
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, fighterName, fighterNamePos, Color.Black);   
            spriteBatch.DrawString(font, enemyName, enemyNamePos, Color.Black);   
        }
    }
}
