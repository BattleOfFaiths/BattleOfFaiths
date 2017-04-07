using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Components;
using BattleOfFaiths.Game.Data;
using BattleOfFaiths.Game.Helpers;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Screens
{
    public class ShopScreen
    {
        private SpriteFont font;

        private Button close;
        private string closeString;
        private Vector2 closePosition;

        private List<ShopItem> items;
        
        private string moneyString;
        private Vector2 moneyPosition;

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
            moneyString = "Money: " + GameAuth.GetCurrentGame().Money;
            moneyPosition = new Vector2(30, screenHeight - 60);
            
            closeString = "Close";
            closePosition = new Vector2(screenWidth - 100, screenHeight - 60);
            close = new Button(closeString, closePosition);

            items = new List<ShopItem>();
            List<Item> shopItems = GetAllShopItems();
            for (int i = 0, col = 0, row = 0; i < shopItems.Count; col++, i++)
            {
                if (col == 4)
                {
                    row++;
                    col = 0;
                }
                Vector2 position = new Vector2(30 + col * 200, 20 + row * 200);
                items.Add(new ShopItem(shopItems[i], position));
            }
            foreach (ShopItem si in items)
            {
                si.Initialize();
            }
        }

        public void LoadContent(ContentManager Content)
        {
            foreach (ShopItem si in items)
            {
                si.LoadContent(Content);
            }
            font = Content.Load<SpriteFont>("Fonts/font");
            close.LoadContent(Content);
        }

        public void Update()
        {
            moneyString = "Money: " + GetAmountOfMoney(GameAuth.GetCurrentGame());
            foreach (ShopItem si in items)
            {
                si.Update();
            }
            close.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (ShopItem si in items)
            {
                si.Draw(spriteBatch);
            }
            spriteBatch.DrawString(font, moneyString, moneyPosition, Color.White);
            close.Draw(spriteBatch);
            spriteBatch.End();
        }

        private List<Item> GetAllShopItems()
        {
            using (var context = new BattleOfFaithsEntities())
            {
                return context.Items.ToList();
            }
        }

        private int GetAmountOfMoney(Models.Game game)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentGame = context.Games.FirstOrDefault(g => g.Id == game.Id);
                return currentGame.Money;
            }
        }
    }
}
