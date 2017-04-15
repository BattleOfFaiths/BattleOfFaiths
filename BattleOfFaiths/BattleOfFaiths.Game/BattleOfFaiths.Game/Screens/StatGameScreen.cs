using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Components;
using BattleOfFaiths.Game.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BattleOfFaiths.Game.Models;

namespace BattleOfFaiths.Game.Screens
{
    public class StatGameScreen
    {
        private static SpriteFont font;
        private Texture2D gameLoadBackgroundImage;        

        private Button back;
        private string backString;
        private Vector2 backPosition;       
        
        private string bestGameHighScore;
        private string bestCharHighScore;
        private Vector2 bestGamePosition;
        private Vector2 bestCharPosition;
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
            bestGameHighScore = TakeBestGames();
            bestGamePosition = new Vector2(20, 60);
            bestCharPosition = new Vector2(340, 60);
            bestCharHighScore = TakeBestChar();
            backString = "Back";
            backPosition = new Vector2(screenWidth - 100, screenHeight - 60);
            back = new Button(backString, backPosition);
           
        }

        private string TakeBestChar()
        {
            using(var context = new BattleOfFaithsEntities())
            {
                var result = context.Characters
                    .OrderByDescending(c => c.Highscore)
                    .Select(c=>new  { Name = c.Name , Highscore=c.Highscore}).ToList();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Best Characters Highscore :");
                int count = 0;
                foreach (var ch in result)
                {                    
                    sb.AppendLine($"{++count}. {ch.Name} : {ch.Highscore}");
                }
                return sb.ToString();
            }
            
        }

        private string TakeBestGames()
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var result = context.Games
                    .OrderByDescending(g =>g.HighScore)
                    .Select(g => new { Date=g.Date,HighScore=g.HighScore}).Take(3).ToList();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Best Games Highscore :");
                int count = 0;
                foreach (var ch in result)
                {
                    sb.AppendLine($"{++count}. {ch.Date} : {ch.HighScore}");
                }
                return sb.ToString();
            }
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            gameLoadBackgroundImage = Content.Load<Texture2D>("Backgrounds/Load");
         
            back.LoadContent(Content);
        }

        public void Update()
        {
           
            back.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameLoadBackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, bestGameHighScore, bestGamePosition, Color.White);
            spriteBatch.DrawString(font, bestCharHighScore, bestCharPosition, Color.White);
            back.Draw(spriteBatch);
            spriteBatch.End();
        }

      
    }
}
