using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private Texture2D container;
        private string type;
        private Vector2 position;
        private int isEnemy;
        private float quantity;
        private Color color;
        private Rectangle sourceRect;
        private Control control;
        private float total;

        public Bar(string type, Vector2 position, float quantity, int isEnemy, Control control)
        {
            this.type = type;
            this.position = position;
            this.quantity = quantity;
            this.isEnemy = isEnemy;
            this.control = control;
        }

        public void InitializeBar()
        {
            color = this.type == "mana" ? Color.Blue : Color.Red;
            total = quantity;
            sourceRect = type == "mana"
                ? new Rectangle((int) position.X, (int) position.Y, 350, 17)
                : new Rectangle((int) position.X, (int) position.Y, 350, 34);
        }

        public void LoadContent(ContentManager Content)
        {
            if (this.type == "mana") { 
                bar = Content.Load<Texture2D>("Components/mbar");
                container = Content.Load<Texture2D>("Components/mContainer");
            }
            else
            {
                bar = Content.Load<Texture2D>("Components/hbar");
                container = Content.Load<Texture2D>("Components/hContainer");
            }
        }

        public void Update()//int decreaseAmount)
        {
            float percentage = 0f;

            if (type == "mana" && isEnemy == 0)
            {
                quantity = control.PlayerMana;
                percentage = (quantity / total) * 100f;
            }
            else if (type == "mana" && isEnemy == 1)
            {
                quantity = control.EnemyMana;
                percentage = (quantity / total) * 100f;
            }
            else if (type == "health" && isEnemy == 0)
            {
                quantity = control.PlayerHealth;
                percentage = (quantity / total) * 100f;
            }
            else if (type == "health" && isEnemy == 1)
            {
                quantity = control.EnemyHealth;
                percentage = (quantity / total) * 100f;
            }
            
            float value = (percentage * 350f) / 100f;

            sourceRect = new Rectangle((int)position.X, (int)position.Y, (int)value, bar.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(container, position, Color.White);
            spriteBatch.Draw(bar, sourceRect, color);
        }
    }
}
