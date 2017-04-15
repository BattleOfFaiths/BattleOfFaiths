using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using BattleOfFaiths.Game.Data;
using BattleOfFaiths.Game.Helpers;
using BattleOfFaiths.Game.Models;
using BattleOfFaiths.Game.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Components
{
    public class Control
    {
        private int turn;
        private Fight fight;
        private Fighter fighter;
        private Enemy enemy;
        private bool changeTurn = false;
        private bool endFight = false;
        private bool waitingDone = false;
        private bool winLoseDone = false;
        private double counter = 0d;
        private double endFightCounter = 0d;
        private double winLoseCounter = 0d;
        private int playerHealth, enemyHealth, playerMana, enemyMana;
        private int playerMaxHealth, enemyMaxHealth, playerMaxMana, enemyMaxMana;
        private int playerAtk, enemyAtk, playerSpAtk, enemySpAtk;
        private EndFightScreen endFightScreen;
        private int highscoreMade;
        private bool hasFightEnded, hasEndBeenInitialized, didWin;

        public Control(Fight fight, Fighter fighter, Enemy enemy)
        {
            this.fight = fight;
            this.fighter = fighter;
            this.enemy = enemy;
            this.turn = 0;
        }

        public int PlayerHealth => playerHealth;
        public int PlayerMana => playerMana;
        public int PlayerAtk => playerAtk;
        public int PlayerSpAtk => playerSpAtk;
        public int EnemyHealth => enemyHealth;
        public int EnemyMana => enemyMana;
        public int EnemyAtk => enemyAtk;
        public int EnemySpAtk => enemySpAtk;

        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        public bool HasFightEnded
        {
            get { return hasFightEnded; }
        }

        public void Initialize()
        {
            FillFightCharacteristics(fight, fighter, enemy);
            playerMaxHealth = playerHealth = GetStats(fight, "playerHealth");
            playerMaxMana = playerMana = GetStats(fight, "playerMana");
            playerAtk = GetStats(fight, "playerAtk");
            playerSpAtk = GetStats(fight, "playerSpAtk");
            enemyMaxHealth = enemyHealth = GetStats(fight, "enemyHealth");
            enemyMaxMana = enemyMana = GetStats(fight, "enemyMana");
            enemyAtk = GetStats(fight, "enemyAtk");
            enemySpAtk = GetStats(fight, "enemySpAtk");
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            if (hasFightEnded)
            {
                if (!hasEndBeenInitialized)
                {
                    endFightScreen = new EndFightScreen(didWin, highscoreMade.ToString(), fighter);
                    endFightScreen.Initialize();
                    endFightScreen.LoadContent(Content);
                    hasEndBeenInitialized = true;
                }
                endFightScreen.Update();
            }
            else
            {
                if (!endFight)
                {
                    if (!changeTurn)
                    {
                        if (this.Turn == 0)
                        {
                            if (fighter.BasicAttack.Active)
                            {
                                FighterBasicAttack(gameTime);
                                changeTurn = true;
                            }
                            else if (fighter.SpecialAttack.Active)
                            {
                                FighterSpecialAttack(gameTime);
                                changeTurn = true;
                            }
                        }
                        else
                        {
                            if (enemyMana >= enemySpAtk)
                            {
                                EnemySpecialAttack(gameTime);
                            }
                            else
                            {
                                EnemyBasicAttack(gameTime);
                            }
                            changeTurn = true;
                        }
                    }

                    if (changeTurn)
                    {
                        Wait(gameTime);
                    }
                }
                else
                {
                    EndFightPause(gameTime);
                    if (waitingDone)
                    {
                        if (didWin) YouWin(gameTime);
                        else YouLose(gameTime);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (hasEndBeenInitialized)
                endFightScreen.Draw(spriteBatch);
        }

        private void EnemyBasicAttack(GameTime gameTime)
        {
            enemy.BasicAttack.Active = true;
            if (DidDefend())
            {
                fighter.Defence.Active = true;
                if (playerHealth + enemyAtk / 3 > playerMaxHealth)
                    playerHealth = playerMaxHealth;
                else
                    playerHealth += enemyAtk / 3;
                if (enemyMana + enemyAtk > enemyMaxMana)
                    enemyMana = enemyMaxMana;
                else
                    enemyMana += enemyAtk;
            }
            else
            {
                fighter.Hit.Active = true;

                if (enemyMana + enemyAtk > enemyMaxMana)
                    enemyMana = enemyMaxMana;
                else
                    enemyMana += enemyAtk;

                if (playerHealth - enemyAtk <= 0)
                {
                    playerHealth = 0;
                    this.didWin = false;
                    endFight = true;
                    //YouLose();
                }
                else
                    playerHealth -= enemyAtk;
            }
        }

        private void EnemySpecialAttack(GameTime gameTime)
        {
            enemy.SpecialAttack.Active = true;
            enemyMana -= enemySpAtk;
            if (DidDefend())
            {
                fighter.Defence.Active = true;
                if (playerHealth + enemySpAtk / 3 > playerMaxHealth)
                    playerHealth = playerMaxHealth;
                else
                    playerHealth += enemySpAtk / 3;
            }
            else
            {
                fighter.Hit.Active = true;
                if (playerHealth - enemySpAtk <= 0)
                {
                    playerHealth = 0;
                    this.didWin = false;
                    endFight = true;
                    //YouLose();
                }
                else
                    playerHealth -= enemySpAtk;
            }
        }

        private void YouLose(GameTime gameTime)
        {
            fighter.Lose.Active = true;
            enemy.Win.Active = true;
            WaitWinLoseReaction(gameTime);
            if (winLoseDone)
            {
                SaveFightStatsToDatabase(fight, fighter, enemy);
                hasFightEnded = true;
            }
        }

        private void FighterSpecialAttack(GameTime gameTime)
        {
            highscoreMade += playerSpAtk;
            playerMana -= playerSpAtk;
            if (DidDefend())
            {
                enemy.Defence.Active = true;
                if (enemyHealth + playerSpAtk / 3 > enemyMaxHealth)
                    enemyHealth = enemyMaxHealth;
                else
                    enemyHealth += playerSpAtk / 3;
            }
            else
            {
                enemy.Hit.Active = true;
                
                if (enemyHealth - playerSpAtk <= 0)
                {
                    enemyHealth = 0;
                    this.didWin = true;
                    endFight = true;
                    //YouWin();
                }
                else
                    enemyHealth -= playerSpAtk;
            }
        }

        private void FighterBasicAttack(GameTime gameTime)
        {
            highscoreMade += playerAtk;
            if (DidDefend())
            {
                enemy.Defence.Active = true;
                if (enemyHealth + playerAtk / 3 > enemyMaxHealth)
                    enemyHealth = enemyMaxHealth;
                else
                    enemyHealth += playerAtk / 3;
                if (playerMana + playerAtk > playerMaxMana)
                    playerMana = playerMaxMana;
                else
                    playerMana += playerAtk;
            }
            else
            {
                enemy.Hit.Active = true;
                if (playerMana + playerAtk > playerMaxMana)
                    playerMana = playerMaxMana;
                else
                    playerMana += playerAtk;

                if (enemyHealth - playerAtk <= 0)
                {
                    enemyHealth = 0;
                    this.didWin = true;
                    endFight = true;
                    //YouWin();
                }
                else
                    enemyHealth -= playerAtk;
            }
        }

        private void YouWin(GameTime gameTime)
        {
            enemy.Lose.Active = true;
            fighter.Win.Active = true;
            WaitWinLoseReaction(gameTime);
            if (winLoseDone)
            {
                SaveFightStatsToDatabase(fight, fighter, enemy);
                hasFightEnded = true;
            }
        }

        private void Wait(GameTime gameTime)
        {
            this.counter += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.counter > 2)
            {
                this.Turn = this.Turn == 0 ? 1 : 0;
                this.counter = 0;
                changeTurn = false;
            }
        }

        private void EndFightPause(GameTime gameTime)
        {
            this.endFightCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.endFightCounter > 2)
            {
                waitingDone = true;
            }
        }

        private void WaitWinLoseReaction(GameTime gameTime)
        {
            this.winLoseCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.winLoseCounter > 2)
            {
                winLoseDone = true;
            }
        }

        private bool DidDefend()
        {
            int num;
            Random rnd = new Random();
            num = rnd.Next(1, 6);
            return num != 1 && num != 3 && num != 5;
        }

        private int GetStats(Fight fight, string type)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentFight = context.Fights.FirstOrDefault(f => f.Id == fight.Id);
                switch (type)
                {
                    case "playerHealth":
                        return currentFight.playerHealth;
                    case "playerMana":
                        return currentFight.playerMana;
                    case "playerAtk":
                        return currentFight.playerAtkDmg;
                    case "playerSpAtk":
                        return currentFight.playerSpAtkDmg;
                    case "enemyHealth":
                        return currentFight.enemyHealth;
                    case "enemyMana":
                        return currentFight.enemyMana;
                    case "enemyAtk":
                        return currentFight.enemyAtkDmg;
                    case "enemySpAtk":
                        return currentFight.enemySpAtkDmg;
                    default:
                        return 0;
                }
            }
        }

        private void FillFightCharacteristics(Fight fight, Fighter fighter, Enemy enemy)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentFight = context.Fights.FirstOrDefault(f => f.Id == fight.Id);
                var currentFighter = context.Characters.FirstOrDefault(c => c.Name == fighter.Character.Name);
                var currentEnemy = context.Characters.FirstOrDefault(c => c.Name == enemy.Character.Name);

                currentFight.playerHealth = currentFighter.Health * (currentFighter.Level + 2);
                currentFight.playerMana = currentFighter.Mana * (currentFighter.Level + 2);
                currentFight.playerAtkDmg = currentFighter.Attack * (currentFighter.Level + 1);
                currentFight.playerSpAtkDmg = currentFighter.SpecAttack * (currentFighter.Level + 1);
                currentFight.enemyHealth = currentEnemy.Health * (currentFighter.Level + 2);
                currentFight.enemyMana = currentEnemy.Mana * (currentFighter.Level + 2);
                currentFight.enemyAtkDmg = currentEnemy.Attack * (currentFighter.Level + 1);
                currentFight.enemySpAtkDmg = currentEnemy.SpecAttack * (currentFighter.Level + 1);
                context.SaveChanges();
            }
        }

        private void SaveFightStatsToDatabase(Fight fight, Fighter fighter, Enemy enemy)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentFight = context.Fights.FirstOrDefault(f => f.Id == fight.Id);
                var currentFighter = context.Characters.FirstOrDefault(c => c.Name == fighter.Character.Name);
                var currentEnemy = context.Characters.FirstOrDefault(c => c.Name == enemy.Character.Name);

                currentFight.playerHealth = playerHealth;
                currentFight.playerMana = playerMana;
                currentFight.enemyHealth = enemyHealth;
                currentFight.enemyMana = enemyMana;
                context.SaveChanges();
            }
        }
    }
}
