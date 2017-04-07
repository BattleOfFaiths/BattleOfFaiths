using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Components;
using BattleOfFaiths.Game.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Screens
{
    public class GameMenu
    {
        private static SpriteFont font;

        private ShopScreen shopScreen = new ShopScreen();

        private Texture2D gameMenuBackgroundImage;
        private Vector2 bgPosition;

        private Button fight;
        private Button shop;
        private Button back;
        private List<Button> buttons;

        private string fightString;
        private Vector2 fightPosition;

        private string shopString;
        private Vector2 shopPosition;

        private string backString;
        private Vector2 backPosition;
        
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
            bgPosition = Vector2.Zero;
            fightPosition = new Vector2(screenWidth / 10, screenHeight - 60);
            shopPosition = new Vector2(fightPosition.X + screenWidth / 3, screenHeight - 60);
            backPosition = new Vector2(shopPosition.X + screenWidth / 3, screenHeight - 60);

            fightString = "Fight";
            shopString = "Shop";
            backString = "Back";

            fight = new Button(fightString, fightPosition);
            shop = new Button(shopString, shopPosition);
            back = new Button(backString, backPosition);

            buttons = new List<Button>();
            buttons.Add(fight);
            buttons.Add(shop);
            buttons.Add(back);

            shopScreen.Initialize();
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            foreach (Button button in buttons)
            {
                button.LoadContent(Content);
            }
            gameMenuBackgroundImage = Content.Load<Texture2D>("Backgrounds/main3");

            shopScreen.LoadContent(Content);
        }

        public void Update(ContentManager Content)
        {
            if (!StaticBooleans.IsShopOpen)
            {
                foreach (Button button in buttons)
                {
                    button.Update();
                }
            }
            else
            {
                shopScreen.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (StaticBooleans.IsShopOpen)
            {
                shopScreen.Draw(spriteBatch);
            }
            else
            {
                LoadGameMenu(spriteBatch);
            }
        }

        private void LoadGameMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameMenuBackgroundImage, bgPosition, Color.White);
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
