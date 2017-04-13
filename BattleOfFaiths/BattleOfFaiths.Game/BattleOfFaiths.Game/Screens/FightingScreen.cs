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
    public class FightingScreen
    {
        private Character character;
        private Fighter fighter;
        private Enemy enemy;
        private Fight fight;

        private DrawNames drawNames;
        private Control control;

        private List<Components.Attack> attacks;
        private Components.Attack basicAttack;
        private Components.Attack specialAttack;

        //private List<Item> items;

        private Map map = new Map();

        private List<Bar> bars;
        private Bar playerHealthBar, enemyHealthBar, playerManaBar, enemyManaBar;

        public void Initialize()
        {
            fight = FightAuth.GetCurrentFight();

            character = new Character();
            character = GetCharacterFromFight(fight);

            map.Initialize();

            fighter = new Fighter(character);
            fighter.Initialize();

            enemy = new Enemy();
            enemy.Initialize();

            drawNames = new DrawNames(fighter.Character, enemy.Character);
            drawNames.Initialize();

            attacks = new List<Components.Attack>();
            basicAttack = new Components.Attack("Basic", new Vector2(340, 90), fighter.Character, fighter.BasicAttack);
            specialAttack = new Components.Attack("Special", new Vector2(410, 90), fighter.Character, fighter.SpecialAttack);
            attacks.Add(basicAttack);
            attacks.Add(specialAttack);

            control = new Control(fight, fighter, enemy);
            control.Initialize();

            InitializeBars();
        }

        public void LoadContent(ContentManager Content)
        {
            map.LoadContent(Content);
            fighter.LoadContent(Content);
            enemy.LoadContent(Content);
            drawNames.LoadContent(Content);
            foreach (Bar b in bars)
            {
                b.LoadContent(Content);
            }
            foreach (var atk in attacks)
            {
                atk.LoadContent(Content);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var atk in attacks)
            {
                atk.Update();
            }
            fighter.Update(gameTime);
            enemy.Update(gameTime);
            control.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            foreach (Bar bar in bars)
            {
                bar.Draw(spriteBatch);
            }
            fighter.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            drawNames.Draw(spriteBatch);
            foreach (var atk in attacks)
            {
                atk.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private void InitializeBars()
        {
            playerHealthBar = new Bar("health", new Vector2(20, 20), GetCharacterCharacteristicsByName("fighter", fight, "health"), 0);
            enemyHealthBar = new Bar("health", new Vector2(420, 20), GetCharacterCharacteristicsByName("enemy", fight, "health"), 1);
            playerManaBar = new Bar("mana", new Vector2(20, 60), GetCharacterCharacteristicsByName("fighter", fight, "mana"), 0);
            enemyManaBar = new Bar("mana", new Vector2(420, 60), GetCharacterCharacteristicsByName("enemy", fight, "mana"), 1);
            bars = new List<Bar>();
            bars.Add(playerHealthBar);
            bars.Add(enemyHealthBar);
            bars.Add(playerManaBar);
            bars.Add(enemyManaBar);
            foreach (Bar b in bars)
            {
                b.InitializeBar();
            }
        }

        private Character GetCharacterFromFight(Fight fight)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentFight = context.Fights.FirstOrDefault(f => f.Id == fight.Id);

                return currentFight.Character;
            }
        }

        private int GetCharacterCharacteristicsByName(string type, Fight fight, string name)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentFight = context.Fights.FirstOrDefault(f => f.Id == fight.Id);
                return type == "fighter"
                    ? (name == "health" ? currentFight.playerHealth : currentFight.playerMana)
                    : (name == "health" ? currentFight.enemyHealth : currentFight.enemyMana);
            }
        }
    }
}
