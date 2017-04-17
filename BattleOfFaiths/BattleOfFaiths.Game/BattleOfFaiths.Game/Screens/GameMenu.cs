using System;
using System.Collections.Generic;
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
    public class GameMenu
    {
        private static SpriteFont font;

        private ShopScreen shopScreen = new ShopScreen();
        private FightingScreen fightScreen = new FightingScreen();

        private Texture2D gameMenuBackgroundImage;
        private Vector2 bgPosition;
        
        private Button shop;
        private Button back;
        private List<Button> buttons;

        private List<CharacterBlock> characters;

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
            var charactersList = GetAllGameCharacters(GameAuth.GetCurrentGame());
            characters = new List<CharacterBlock>();
            for (int i = 0; i < charactersList.Count; i++)
            {
                characters.Add(new CharacterBlock(charactersList[i], new Vector2(15, 15 + i * 150)));
            }
            foreach (CharacterBlock cb in characters)
            {
                cb.Initialize();
            }

            bgPosition = Vector2.Zero;
            shopPosition = new Vector2(screenWidth / 3, screenHeight - 60);
            backPosition = new Vector2(shopPosition.X + screenWidth / 3, screenHeight - 60);
            
            shopString = "Shop";
            backString = "Back";
            
            shop = new Button(shopString, shopPosition);
            back = new Button(backString, backPosition);

            buttons = new List<Button>();
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
            gameMenuBackgroundImage = Content.Load<Texture2D>("Backgrounds/skul");

            shopScreen.LoadContent(Content);
            foreach (CharacterBlock cb in characters)
            {
                cb.LoadContent(Content);
            }
        }

        public void Update(ContentManager Content, GameTime gameTime)
        {
            if (StaticBooleans.IsShopOpen)
            {
                shopScreen.Update();
            }
            else if (FightAuth.HasStartedFight())
            {
                fightScreen.Update(gameTime, Content);
            }
            else 
            {
                foreach (CharacterBlock cb in characters)
                {
                    if (StaticBooleans.NeedInitializing)
                    {
                        cb.Initialize();
                        cb.LoadContent(Content);
                    }
                }
                StaticBooleans.SetNeedInitializingBool(false);
                foreach (CharacterBlock cb in characters)
                {
                    cb.Update();
                }
                foreach (Button button in buttons)
                {
                    button.Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            if (StaticBooleans.IsShopOpen)
            {
                shopScreen.Draw(spriteBatch);
            }
            else if (FightAuth.HasStartedFight())
            {
                if (!StaticBooleans.HasFightBeenInitialized)
                {
                    fightScreen.Initialize();
                    fightScreen.LoadContent(Content);
                    StaticBooleans.SetHasFightBeenInitializedBool(true);
                }
                fightScreen.Draw(spriteBatch);
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
            foreach (CharacterBlock cb in characters)
            {
                cb.Draw(spriteBatch);
            }
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private List<Character> GetAllGameCharacters(Models.Game game)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentGame = context.Games.FirstOrDefault(g => g.Id == game.Id);
                return currentGame.Characters.ToList();
            }
        }
    }
}
