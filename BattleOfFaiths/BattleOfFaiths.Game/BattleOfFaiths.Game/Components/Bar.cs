using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Components
{
    public class Bar : Microsoft.Xna.Framework.Game
    {
        private Texture2D bar;
        private string type;
        private Vector2 position;
        private int quantity, isEnemy;
        private Color color;
        private float multiplier;
        private Vector2 startPoint;
        private Vector2 endPoint;
        //private Rectangle sourceRect;

        public Bar(string type, Vector2 position, int quantity, int isEnemy)
        {
            this.type = type;
            this.position = position;
            this.quantity = quantity;
            this.isEnemy = isEnemy;
        }

        public void InitializeBar()
        {
            color = this.type == "mana" ? Color.Blue : Color.Red;
            multiplier = 350 / quantity;
            startPoint = position;
            endPoint = new Vector2(position.X + 350, position.Y);
        }

        public void LoadContent(ContentManager Content)
        {
            if (this.type == "mana") { 
                bar = Content.Load<Texture2D>("Components/mbar");
            }
            else
            {
                bar = Content.Load<Texture2D>("Components/hbar");
            }
        }

        public void Update(int decreaseAmount)
        {
            if (isEnemy == 0)
                startPoint.X += multiplier * decreaseAmount;
            else
                endPoint.X -= multiplier * decreaseAmount;

            //sourceRect = new Rectangle(
            //    (int) startPoint.X,
            //    (int) endPoint.Y,
            //    (int) (endPoint.X - startPoint.X),
            //    (int) (endPoint.Y - startPoint.Y));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                bar, 
                position,
                //sourceRect,
                color);
        }
    }
}
