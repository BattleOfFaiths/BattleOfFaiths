using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleOfFaiths.Game.Components
{
    public class Attack
    {
        private Texture2D atkImage;
        private Vector2 pos;
        private string type;
        private Color color;
        private Character character;
        private MouseState prevMouseState;
        private MouseState MouseState;
        private Animation animation;

        public Attack(string type, Vector2 position, Character character, Animation animation)
        {
            this.type = type;
            this.pos = position;
            this.character = character;
            this.animation = animation;
        }

        public void LoadContent(ContentManager Content)
        {
            atkImage = Content.Load<Texture2D>("Sprites/Items/manaSprite");
        }

        public void Update()
        {
            prevMouseState = MouseState;
            MouseState = Mouse.GetState();

            if (MouseState.X < pos.X || MouseState.Y < pos.Y
                || MouseState.X > pos.X + atkImage.Width
                || MouseState.Y > pos.Y + atkImage.Height)
            {
                //The mouse is not hovering over the button
                color = new Color(255, 255, 255);    
            }
            else
            {
                color = new Color(0, 0, 0);

                if (MouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    switch (type)
                    {
                        case "basic":
                            color = new Color(0, 0, 255);
                            animation.Active = true;
                            break;
                        case "special":
                            animation.Active = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(atkImage, pos, color);
        }
    }
}
