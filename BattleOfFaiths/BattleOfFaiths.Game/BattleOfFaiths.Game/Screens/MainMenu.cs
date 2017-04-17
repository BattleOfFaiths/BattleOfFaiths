using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Screens;
using BattleOfFaiths.Game.Components;
using BattleOfFaiths.Game.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleOfFaiths.Game.Screens
{
    public class MainMenu
    {
        private static SpriteFont font;

        private Texture2D mainBackgroundImage;
        private Vector2 bgPosition;

        GameMenu gameMenu = new GameMenu();
        LoadGameScreen loadGameScreen = new LoadGameScreen();
        StatGameScreen loadStatGameScreen = new StatGameScreen();

        private Button newGame;
        private Button loadGame;
        private Button statistics;
        private List<Button> buttons;

        private string newGameString;
        private Vector2 newGamePosition;

        private string loadGameString;
        private Vector2 loadGamePosition;

        private string statisticsString;
        private Vector2 statisticsPosition;
        
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
            newGamePosition = new Vector2(screenWidth / 10, screenHeight - 60);
            loadGamePosition = new Vector2(newGamePosition.X + screenWidth / 3, screenHeight- 60);
            statisticsPosition = new Vector2(loadGamePosition.X + screenWidth / 3, screenHeight - 60);

            newGameString = "New Game";
            loadGameString = "Load Game";
            statisticsString = "Statistics";

            newGame = new Button(newGameString, newGamePosition);
            loadGame = new Button(loadGameString, loadGamePosition);
            statistics = new Button(statisticsString, statisticsPosition);

            buttons = new List<Button>();
            buttons.Add(newGame);
            buttons.Add(loadGame);
            buttons.Add(statistics);

            loadGameScreen.Initialize();
            loadStatGameScreen.Initialize();
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            foreach (Button button in buttons)
            {
                button.LoadContent(Content);
            }
            mainBackgroundImage = Content.Load<Texture2D>("Backgrounds/main3");

            loadGameScreen.LoadContent(Content);
            loadStatGameScreen.LoadContent(Content);
        }

        public void Update(ContentManager Content, GameTime gameTime)
        {
            if (!StaticBooleans.IsShopOpen)
            {
                foreach (Button button in buttons)
                {
                    button.Update();
                }
            }
            if (StaticBooleans.IsLoadGamesOn && StaticBooleans.HasNewGame)
            {
                loadGameScreen.Initialize();
                loadGameScreen.LoadContent(Content);
                StaticBooleans.SetHasNewGame(false);
            }
            if (StaticBooleans.IsLoadGamesOn && !StaticBooleans.IsShopOpen) loadGameScreen.Update();
            if (StaticBooleans.IsStatGamesOn)
            {
                loadStatGameScreen.Initialize();
                loadStatGameScreen.LoadContent(Content);
                StaticBooleans.SetHasNewGame(false);

            }
            if (StaticBooleans.IsStatGamesOn && !StaticBooleans.IsShopOpen) loadStatGameScreen.Update();
            if (GameAuth.HasStartedGame() && !StaticBooleans.IsGameMenuInitialized)
            {
                gameMenu.Initialize();
                gameMenu.LoadContent(Content);
                StaticBooleans.SetIsGameMenuInitializedBool(true);
            }
            if (GameAuth.HasStartedGame()) { gameMenu.Update(Content, gameTime);}
        }

        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            //spriteBatch.Begin();

            if (GameAuth.HasStartedGame())
            {
                gameMenu.Draw(spriteBatch, Content);
            }
            else if (StaticBooleans.IsLoadGamesOn)
            {
                loadGameScreen.Draw(spriteBatch);
            }
            else if (StaticBooleans.IsStatGamesOn)
            {
                loadStatGameScreen.Draw(spriteBatch);
            }
            else
            {
                LoadMainMenu(spriteBatch);
            }
            
            //spriteBatch.End();
        }

        private void LoadMainMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mainBackgroundImage, bgPosition, Color.White);
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
