using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Components;
using BattleOfFaiths.Game.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Screens
{
    public class LoadGameScreen
    {
        private static SpriteFont font;
        private Texture2D gameLoadBackgroundImage;

        private Button back;
        private string backString;
        private Vector2 backPosition;

        private List<Button> buttons;

        private bool anyGames;
        private string noGames;
        private Vector2 noGamesPosition;
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
            noGamesPosition = new Vector2(screenWidth / 4, screenHeight / 4);
            noGames = "There are no currently saved games!";
            backString = "Back";
            backPosition = new Vector2(screenWidth - 100, screenHeight - 60);
            back = new Button(backString, backPosition);

            buttons = new List<Button>();
            List<Models.Game> games = GetStartedGames();
            anyGames = games.Count != 0;
            for (int i = 0; i < games.Count; i++)
            {
                buttons.Add(new Button(GetCustomName(games[i]), new Vector2(50, 50 + 50 * i), games[i]));
            }
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            gameLoadBackgroundImage = Content.Load<Texture2D>("Backgrounds/Load");
            foreach (Button button in buttons)
            {
                button.LoadContent(Content);
            }
            back.LoadContent(Content);
        }

        public void Update()
        {
            if (anyGames)
            {
                foreach (Button button in buttons)
                {
                    button.Update();
                }
            }
            back.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameLoadBackgroundImage, Vector2.Zero, Color.White);
            if (anyGames)
            {
                foreach (Button button in buttons)
                {
                    button.Draw(spriteBatch);
                }
            }
            else
            {
                spriteBatch.DrawString(font, noGames, noGamesPosition, Color.White);
            }
            back.Draw(spriteBatch);
            spriteBatch.End();
        }

        private List<Models.Game> GetStartedGames()
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var games = context.Games.ToList();

                return games;
            }
        }

        private string GetCustomName(Models.Game game)
        {
            return $"{game.Date} || Highscore: {game.HighScore}";
        }
    }
}
