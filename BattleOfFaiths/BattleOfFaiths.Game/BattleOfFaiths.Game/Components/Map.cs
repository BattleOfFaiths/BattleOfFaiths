using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Components
{
    public class Map
    {
        private Texture2D map;
        private Vector2 mapPosition;
        private Color color;

        public void Initialize()
        {
            mapPosition = Vector2.Zero;
            color = Color.White;
        }

        public void LoadContent(ContentManager Content)
        {
            int num;
            Random rnd = new Random();
            num = rnd.Next(1, 4);
            map = Content.Load<Texture2D>("Backgrounds/bg" + num);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(map, mapPosition, color);
            spriteBatch.End();
        }
    }
}
