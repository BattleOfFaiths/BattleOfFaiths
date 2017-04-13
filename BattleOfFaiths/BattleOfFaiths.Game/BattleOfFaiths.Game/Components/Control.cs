using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Data;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;

namespace BattleOfFaiths.Game.Components
{
    public class Control
    {
        private int turn;
        private Fight fight;
        private Fighter fighter;
        private Enemy enemy;
        private float WaitTimeToChangeTurn = 0;

        public Control(Fight fight, Fighter fighter, Enemy enemy)
        {
            this.fight = fight;
            this.fighter = fighter;
            this.enemy = enemy;
            this.turn = 0;
        }

        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        public void Initialize()
        {
            FillFightCharacteristics(fight, fighter, enemy);
        }

        public void Update(GameTime gameTime)
        {
            if (this.Turn == 0)
            {
                if (fighter.BasicAttack.Active)
                {
                    fight.playerMana -= fight.playerAtkDmg;
                    if (DidDefend())
                    {
                        enemy.Defence.Active = true;
                        fight.enemyHealth += fight.playerAtkDmg / 3;
                        fight.playerMana += fight.playerAtkDmg;
                    }
                    else
                    {
                        enemy.Lose.Active = true;
                        fight.playerMana += fight.playerAtkDmg;
                        fight.enemyHealth -= fight.playerAtkDmg;
                        if (fight.enemyHealth - fight.playerAtkDmg <= 0)
                        {
                            //end fight;
                        }
                    }
                    WaitTimeToChangeTurn = 5000;
                    while (WaitTimeToChangeTurn > 0)
                        Wait(gameTime);
                }
                else if (fighter.SpecialAttack.Active)
                {
                    fight.playerMana -= fight.playerSpAtkDmg;
                    if (DidDefend())
                    {
                        enemy.Defence.Active = true;
                        fight.enemyHealth += fight.playerSpAtkDmg / 3;
                    }
                    else
                    {
                        enemy.Lose.Active = true;
                        fight.enemyHealth -= fight.playerSpAtkDmg;
                        if (fight.enemyHealth - fight.playerSpAtkDmg <= 0)
                        {
                            //end fight;
                        }
                    }
                    WaitTimeToChangeTurn = 5000;
                    while (WaitTimeToChangeTurn > 0)
                        Wait(gameTime);
                }
            }
            else
            {
                if (fight.enemyMana >= fight.enemySpAtkDmg)
                {
                    enemy.SpecialAttack.Active = true;
                    fight.enemyMana -= fight.enemySpAtkDmg;
                    if (DidDefend())
                    {
                        fighter.Defence.Active = true;
                        fight.playerHealth += fight.enemySpAtkDmg / 3;
                    }
                    else
                    {
                        fighter.Lose.Active = true;
                        fight.playerHealth -= fight.enemySpAtkDmg;
                        if (fight.playerHealth - fight.enemySpAtkDmg <= 0)
                        {
                            //end fight;
                        }
                    }
                }
                else
                {
                    enemy.BasicAttack.Active = true;
                    fight.enemyMana -= fight.enemyAtkDmg;
                    if (DidDefend())
                    {
                        fighter.Defence.Active = true;
                        fight.playerHealth += fight.enemyAtkDmg / 3;
                        fight.enemyMana += fight.enemyAtkDmg;
                    }
                    else
                    {
                        fighter.Lose.Active = true;
                        fight.playerMana += fight.playerAtkDmg;
                        fight.playerHealth -= fight.enemyAtkDmg;
                        if (fight.playerHealth - fight.enemyAtkDmg <= 0)
                        {
                            //end fight;
                        }
                    }
                }
                WaitTimeToChangeTurn = 5000;
                while (WaitTimeToChangeTurn > 0)
                    Wait(gameTime);
            }
        }

        private void Wait(GameTime gameTime)
        {
            if (this.WaitTimeToChangeTurn > 0)
            {
                this.WaitTimeToChangeTurn -= (int) gameTime.ElapsedGameTime.TotalSeconds;
                if (this.WaitTimeToChangeTurn <= 0)
                {
                    this.WaitTimeToChangeTurn = 0;
                    this.Turn = this.Turn == 0 ? 1 : 0;
                }
            }
        }

        private bool DidDefend()
        {
            //int num;
            //Random rnd = new Random();
            //num = rnd.Next(1, 5);
            //return num != 1 && num != 3 && num != 5;
            return false;
        }

        private void FillFightCharacteristics(Fight fight, Fighter fighter, Enemy enemy)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentFight = context.Fights.FirstOrDefault(f => f.Id == fight.Id);
                var currentFighter = context.Characters.FirstOrDefault(c => c.Name == fighter.Character.Name);
                var currentEnemy = context.Characters.FirstOrDefault(c => c.Name == enemy.Character.Name);

                currentFight.playerHealth = currentFighter.Health;
                currentFight.playerMana = currentFighter.Mana;
                currentFight.playerAtkDmg = currentFighter.Attack;
                currentFight.playerSpAtkDmg = currentFighter.SpecAttack;
                currentFight.enemyHealth = currentEnemy.Health;
                currentFight.enemyMana = currentEnemy.Mana;
                currentFight.enemyAtkDmg = currentEnemy.Attack;
                currentFight.enemySpAtkDmg = currentEnemy.SpecAttack;
                context.SaveChanges();
            }
        }
    }
}
