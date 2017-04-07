using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Data;
using BattleOfFaiths.Game.Helpers;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleOfFaiths.Game.Components
{
    public class Button
    {
        private SpriteFont font;

        private string name;
        private Vector2 pos;
        private Color color;

        private Models.Game game;
        private Item item;

        private MouseState prevMouseState;
        private MouseState MouseState;

        public Button(string name, Vector2 pos)
        {
            this.name = name;
            this.pos = pos;
        }

        public Button(string name, Vector2 pos, Models.Game game)
        {
            this.name = name;
            this.pos = pos;
            this.game = game;
        }

        public Button(string name, Vector2 pos, Item item)
        {
            this.name = name;
            this.pos = pos;
            this.item = item;
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/font");
        }
        public void Update()
        {
            prevMouseState = MouseState;
            MouseState = Mouse.GetState();

            if (MouseState.X < pos.X || MouseState.Y < pos.Y
                || MouseState.X > pos.X + font.MeasureString(name).X
                || MouseState.Y > pos.Y + font.MeasureString(name).Y)
            {
                //The mouse is not hovering over the button
                color = new Color(255, 255, 255);
                if (StaticBooleans.IsShopOpen)
                {
                    if (item != null && this.item.Price > GameAuth.GetCurrentGame().Money)
                            color = new Color(0, 0, 0);
                }
            }
            else
            {
                color = new Color(0, 0, 0);
                
                if (MouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    switch (name)
                    {
                        case "New Game":
                            NewGame();
                            break;
                        case "Load Game":
                            StaticBooleans.SetLoadGamesBool(true);
                            break;
                        case "Statistics":
                            break;
                        case "Fight":
                            break;
                        case "Shop":
                            StaticBooleans.SetOpenShopBool(true);
                            break;
                        case "Buy":
                            game = GameAuth.GetCurrentGame();
                            BuyIfPossible(this.item, game);
                            break;
                        case "Close":
                            StaticBooleans.SetOpenShopBool(false);
                            break;
                        case "Back":
                            if (!StaticBooleans.IsShopOpen)
                            {
                                GameAuth.EndGame();
                                StaticBooleans.SetLoadGamesBool(false);
                            }
                            break;
                        default:
                            LoadGame(this.game);
                            break;
                    }
                }
            }
        }

        private void BuyIfPossible(Item item, Models.Game game)
        {
            if (item.Price <= game.Money)
            {
                BuyIt(item.Id, game.Id);
                DecreaseMoney(game.Id, game.Money, item.Price);
            }
        }

        private void BuyIt(int itemId, int gameId)
        {
            using (BattleOfFaithsEntities context = new BattleOfFaithsEntities())
            {
                Item item = context.Items.FirstOrDefault(i => i.Id == itemId);
                Models.Game game = context.Games.FirstOrDefault(g => g.Id == gameId);

                game.Items.Add(item);
                context.Games.Attach(game);

                context.SaveChanges();
            }
        }

        private void DecreaseMoney(int gameId, int money, int price)
        {
            using (BattleOfFaithsEntities context = new BattleOfFaithsEntities())
            {
                Models.Game game = context.Games.FirstOrDefault(g => g.Id == gameId);
                var leftMoney = money - price;
                game.Money = leftMoney;

                context.SaveChanges();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, name, pos, color);
        }

        private void NewGame()
        {
            Models.Game currentGame = CreateNewGame();
            GameAuth.SetCurrentGame(currentGame);
            StaticBooleans.SetHasNewGame(true);
            StaticBooleans.SetIsGameMenuInitializedBool(false);
        }

        private void LoadGame(Models.Game game)
        {
            Models.Game currentGame = game;
            GameAuth.SetCurrentGame(currentGame);
            StaticBooleans.SetIsGameMenuInitializedBool(false);
        }

        private Models.Game CreateNewGame()
        {
            using (var context = new BattleOfFaithsEntities())
            {
                Models.Game game = new Models.Game()
                { 
                    Date = DateTime.Now
                };

                context.Games.Add(game);
                context.SaveChanges();

                return game;
            }
        }
    }
}
