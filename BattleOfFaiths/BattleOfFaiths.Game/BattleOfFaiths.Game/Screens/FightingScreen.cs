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

        //test
        private SpriteFont font;
        private string playerHealth;
        private Vector2 playerHealthPos;
        private string playerMana;
        private Vector2 playerManaPos;
        private string playerAtk;
        private Vector2 playerAtkPos;
        private string playerSpAtk;
        private Vector2 playerSpAtkPos;
        private string enemyHealth;
        private Vector2 enemyHealthPos;
        private string enemyMana;
        private Vector2 enemyManaPos;
        private string enemyAtk;
        private Vector2 enemyAtkPos;
        private string enemySpAtk;
        private Vector2 enemySpAtkPos;
        //test

        private List<Item> itemsInBag;
        private List<GameItem> gameItems;

        private Map map = new Map();

        private List<Bar> bars;
        private Bar playerHealthBar, enemyHealthBar, playerManaBar, enemyManaBar;

        public int screenHeight => GraphicsDeviceManager.DefaultBackBufferHeight;

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

            itemsInBag = new List<Item>();
            itemsInBag = GetGameItems(GameAuth.GetCurrentGame());
            gameItems = new List<GameItem>();
            int offset = 0;
            foreach (Item i in itemsInBag)
            {
                gameItems.Add(new GameItem(i, new Vector2(210 + offset, screenHeight - 80)));
                offset += 110;
            }
            foreach (GameItem gi in gameItems)
            {
                gi.Initialize();
            }

            control = new Control(fight, fighter, enemy, gameItems);
            control.Initialize();

            InitializeBars();

            attacks = new List<Components.Attack>();
            basicAttack = new Components.Attack("Basic", new Vector2(340, 90), fighter.Character, fighter.BasicAttack, control);
            specialAttack = new Components.Attack("Special", new Vector2(410, 90), fighter.Character, fighter.SpecialAttack, control);
            attacks.Add(basicAttack);
            attacks.Add(specialAttack);

            //test

                playerHealth = control.PlayerHealth.ToString();
                playerHealthPos = new Vector2(10, 100);
                playerMana = control.PlayerMana.ToString();
                playerManaPos = new Vector2(10, 120); 
                playerAtk = control.PlayerAtk.ToString(); 
                playerAtkPos = new Vector2(10, 140);
                playerSpAtk = control.PlayerSpAtk.ToString(); 
                playerSpAtkPos = new Vector2(10, 160);
                enemyHealth = control.EnemyHealth.ToString(); 
                enemyHealthPos = new Vector2(10, 220);
                enemyMana = control.EnemyMana.ToString(); 
                enemyManaPos = new Vector2(10, 240);
                enemyAtk = control.EnemyAtk.ToString(); 
                enemyAtkPos = new Vector2(10, 260);
                enemySpAtk = control.EnemySpAtk.ToString(); 
                enemySpAtkPos = new Vector2(10, 280);

            //test
        }

        public void LoadContent(ContentManager Content)
        {

            foreach (GameItem gi in gameItems)
            {
                gi.LoadContent(Content);
            }

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
            //test
            font = Content.Load<SpriteFont>("Fonts/font");
            //test
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {

            foreach (GameItem gi in gameItems)
            {
                gi.Update();
            }

            foreach (var atk in attacks)
            {
                atk.Update();
            }
            foreach (Bar b in bars)
            {
                b.Update();
            }
            fighter.Update(gameTime);
            enemy.Update(gameTime);
            control.Update(gameTime, Content);
            //test

            playerHealth = control.PlayerHealth.ToString();
            playerMana = control.PlayerMana.ToString();
            playerAtk = control.PlayerAtk.ToString();
            playerSpAtk = control.PlayerSpAtk.ToString();
            enemyHealth = control.EnemyHealth.ToString();
            enemyMana = control.EnemyMana.ToString();
            enemyAtk = control.EnemyAtk.ToString();
            enemySpAtk = control.EnemySpAtk.ToString();

            //test
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (!control.HasFightEnded)
            {
                map.Draw(spriteBatch);
                foreach (Bar bar in bars)
                {
                    bar.Draw(spriteBatch);
                }
                fighter.Draw(spriteBatch);
                enemy.Draw(spriteBatch);
                drawNames.Draw(spriteBatch);

                foreach (GameItem gi in gameItems)
                {
                    gi.Draw(spriteBatch);
                }

                foreach (var atk in attacks)
                {
                    atk.Draw(spriteBatch);
                }
                //test
                spriteBatch.DrawString(font, "ph: " + playerHealth, playerHealthPos, Color.White);
                spriteBatch.DrawString(font, "pm: " + playerMana, playerManaPos, Color.White);
                spriteBatch.DrawString(font, "pa: " + playerAtk, playerAtkPos, Color.White);
                spriteBatch.DrawString(font, "ps: " + playerSpAtk, playerSpAtkPos, Color.White);
                spriteBatch.DrawString(font, "eh: " + enemyHealth, enemyHealthPos, Color.White);
                spriteBatch.DrawString(font, "em: " + enemyMana, enemyManaPos, Color.White);
                spriteBatch.DrawString(font, "ea: " + enemyAtk, enemyAtkPos, Color.White);
                spriteBatch.DrawString(font, "es: " + enemySpAtk, enemySpAtkPos, Color.White);
                //test
            }
            else
                control.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void InitializeBars()
        {
            playerHealthBar = new Bar("health", new Vector2(20, 20), control.PlayerHealth, 0, control);
            enemyHealthBar = new Bar("health", new Vector2(420, 20), control.EnemyHealth, 1, control);
            playerManaBar = new Bar("mana", new Vector2(20, 60), control.PlayerMana, 0, control);
            enemyManaBar = new Bar("mana", new Vector2(420, 60), control.EnemyMana, 1, control);
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

        private List<Item> GetGameItems(Models.Game game)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentGame = context.Games.FirstOrDefault(g => g.Id == game.Id);
                var items = currentGame.Items.Take(4).ToList();

                return items;
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
