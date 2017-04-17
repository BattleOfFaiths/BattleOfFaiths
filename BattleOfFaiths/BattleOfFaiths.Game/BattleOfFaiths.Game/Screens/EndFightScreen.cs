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
using Microsoft.Xna.Framework.Input;

namespace BattleOfFaiths.Game.Screens
{
    public class EndFightScreen
    {
        private string fightResult, moneyWon, highscoreMade, levelUpString, escape;
        private Vector2 fightResultPos, moneyWonPos, highscoreMadePos, levelUpStringPos, escapePos;
        private SpriteFont font, font1;
        private bool didWin, levelUp;
        private Texture2D bg;
        private Color color;
        private int levelValue;
        private Fighter fighter;
        private KeyboardState keyState;
        private KeyboardState prevKeyState;

        public EndFightScreen(bool didWin, string highscoreMade, Fighter fighter)
        {
            this.didWin = didWin;
            this.highscoreMade = highscoreMade;
            this.fighter = fighter;
        }

        public void Initialize()
        {
            moneyWon = didWin ? "300" : "100";
            fightResult = didWin ? "YOU WIN" : "YOU LOSE";
            color = didWin ? Color.Black : Color.White;
            fightResultPos = didWin ? new Vector2(500, 50) : new Vector2(100, 220);
            moneyWonPos = didWin ? new Vector2(500, 100) : new Vector2(100, 270);
            highscoreMadePos = didWin ? new Vector2(500, 130) : new Vector2(100, 290);
            levelValue = GetTotalHighscore(GameAuth.GetCurrentGame(), fighter);
            levelUpString = "You leveled up - Level: " + levelValue;
            levelUpStringPos = didWin ? new Vector2(500, 150) : new Vector2(100, 310);
            escape = "Press Escape For Menu";
            escapePos = didWin ? new Vector2(500, 170) : new Vector2(100, 330);
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
            font1 = Content.Load<SpriteFont>("Fonts/font1");
            bg = didWin
                ? Content.Load<Texture2D>("Backgrounds/main4")
                : Content.Load<Texture2D>("Backgrounds/main2");
        }

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            {
                StaticBooleans.SetHasNewGame(true);
                StaticBooleans.SetNeedInitializingBool(true);
                FightAuth.EndFight();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font1, fightResult, fightResultPos, color);
            spriteBatch.DrawString(font, "Money: +" + moneyWon, moneyWonPos, color);
            spriteBatch.DrawString(font, "Highscore: +" + highscoreMade, highscoreMadePos, color);
            if (levelUp)
                spriteBatch.DrawString(font, levelUpString, levelUpStringPos, color);
            spriteBatch.DrawString(font, escape, escapePos, color);
        }

        private int GetTotalHighscore(Models.Game game, Fighter fighter)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentGame = context.Games.FirstOrDefault(g => g.Id == game.Id);
                var currentCharacter = currentGame.Characters.FirstOrDefault(c => c.Id == fighter.Character.Id);
                var currentLevel = currentCharacter.Level;
                var level = 0;

                var currentHighscore = currentCharacter.Highscore + int.Parse(this.highscoreMade);
                if (currentHighscore > 4000)
                    level = 5;
                else if (currentHighscore > 3000)
                    level = 4;
                else if (currentHighscore > 2000)
                    level = 3;
                else if (currentHighscore > 1000)
                    level = 2;
                else if (currentHighscore > 500)
                    level = 1;

                if (currentLevel < level)
                {
                    levelUp = true;
                    currentCharacter.Level += 1;
                    moneyWon = (int.Parse(moneyWon) + currentCharacter.Level * 500).ToString();
                }

                currentCharacter.Highscore += int.Parse(this.highscoreMade);

                currentGame.Money += int.Parse(moneyWon);
                currentGame.HighScore += int.Parse(this.highscoreMade);
                context.SaveChanges();

                return currentCharacter.Level;
            }
        }
    }
}
