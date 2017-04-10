using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Components
{
    public class Bar : Microsoft.Xna.Framework.Game
    {
        private Rectangle bar, insideRect, quantityBar;
        private string type;
        private Vector2 startPoint, endPoint;
        private int quantity, isEnemy;
        private GraphicsDeviceManager graphics;
        private Texture2D pixel;
        private Color color;
        private float multiplier;

        public Bar(string type, Vector2 startPoint, Vector2 endPoint, int quantity, int isEnemy)
        {
            this.type = type;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.quantity = quantity;
            this.isEnemy = isEnemy;
            this.graphics = new GraphicsDeviceManager(this);
        }

        public void InitializeBar()
        {
            bar = new Rectangle((int)startPoint.X, (int)startPoint.Y, (int)(endPoint.X - startPoint.X), (int)(endPoint.Y - startPoint.Y));
            insideRect = new Rectangle((int)startPoint.X + 5, (int)startPoint.Y + 5, (int)(endPoint.X - 5 - startPoint.X + 5), (int)(endPoint.Y - 5 - startPoint.Y + 5));
            quantityBar = new Rectangle((int)startPoint.X, (int)startPoint.Y, (int)(endPoint.X - startPoint.X), (int)(endPoint.Y - startPoint.Y));
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            color = this.type == "mana" ? Color.Blue : Color.Red;
            multiplier = (endPoint.X - startPoint.X) / quantity;
        }

        public void Update(int decreaseAmount)
        {
            if (isEnemy == 0)
                startPoint.X += multiplier * decreaseAmount;
            else
                endPoint.X -= multiplier * decreaseAmount;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pixel, bar, Color.Black);
            spriteBatch.Draw(pixel, insideRect, Color.White);
            spriteBatch.Draw(
                pixel, 
                quantityBar, 
                new Rectangle(
                    (int)startPoint.X, 
                    (int)endPoint.X, 
                    (int)(endPoint.X - startPoint.X), 
                    (int)(endPoint.Y - startPoint.Y)), 
                color);
            spriteBatch.End();
        }
    }
}
