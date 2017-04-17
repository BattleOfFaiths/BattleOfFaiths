using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleOfFaiths.Game.Components
{
    public class GameItem
    {
        private Texture2D itemPic;
        private Vector2 pos;
        private Item item;
        private Color color;
        private bool isUsed;

        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }

        public Item Item => item;

        public GameItem(Item item, Vector2 pos)
        {
            this.item = item;
            this.pos = pos;
        }

        public void Initialize()
        {
            color = Color.White;
            isUsed = false;
        }

        public void LoadContent(ContentManager Content)
        {
            itemPic = Content.Load<Texture2D>("Sprites/SmallItems/" + item.Sprite);
        }

        public void Update()
        {
            if (isUsed)
            {
                color = Color.Black;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemPic, pos, color);
        }
    }
}
