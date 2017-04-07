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
    public class ShopItem
    {
        private Item item;
        private Texture2D itemPic;
        private Button buy;
        
        private Vector2 pricePosition;
        private Vector2 namePosition;
        private Vector2 buttonPosition;
        private Vector2 pos;

        private SpriteFont font;

        public ShopItem(Item item, Vector2 pos)
        {
            this.item = item;
            this.pos = pos;
        }

        public void Initialize()
        {
            namePosition = new Vector2(pos.X, pos.Y + 80);
            pricePosition = new Vector2(namePosition.X, namePosition.Y + 20);
            buttonPosition = new Vector2(pricePosition.X + 30, pricePosition.Y + 30);
            buy = new Button("Buy", buttonPosition, this.item);
        }

        public void LoadContent(ContentManager Content)
        {
            buy.LoadContent(Content);
            font = Content.Load<SpriteFont>("Fonts/font");
            itemPic = Content.Load<Texture2D>("Sprites/Items/" + this.item.Sprite);
        }

        public void Update()
        {
            buy.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemPic, pos, Color.White);
            spriteBatch.DrawString(font, item.Name, namePosition, Color.White);
            spriteBatch.DrawString(font, "Price: " + item.Price, pricePosition, Color.White);
            buy.Draw(spriteBatch); 
        }
    }
}
