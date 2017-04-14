using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Data;
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
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private Animation animation;
        private Control control;

        public Attack(string type, Vector2 pos, Character character, Animation animation, Control control)
        {
            this.type = type;
            this.pos = pos;
            this.character = character;
            this.animation = animation;
            this.control = control;
        }

        public void LoadContent(ContentManager Content)
        {
            atkImage = Content.Load<Texture2D>("Sprites/Items/" + GetActionIconSpite(character, type));
        }

        public void Update()
        {
            if (control.Turn == 1)
                color = Color.Black;
            else
            {
                if (type == "Basic")
                    color = Color.White;
                else if (control.PlayerMana < control.PlayerSpAtk)
                    color = Color.Black;
                else color = Color.White; 
            }

            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.A) && prevKeyState.IsKeyUp(Keys.A))
            {
                if (type == "Basic" && color == Color.White)
                    animation.Active = true;
            }
            else if (keyState.IsKeyUp(Keys.A) && prevKeyState.IsKeyDown(Keys.A))
            {
                //false
            }

            if (keyState.IsKeyDown(Keys.S) && prevKeyState.IsKeyUp(Keys.S))
            {
                if (type == "Special" && color == Color.White)
                    animation.Active = true;
            }
            else if (keyState.IsKeyUp(Keys.S) && prevKeyState.IsKeyDown(Keys.S))
            {
                //false
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(atkImage, pos, color);
        }

        private string GetActionIconSpite(Character character, string type)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentCharacter = context.Characters.FirstOrDefault(c => c.Id == character.Id);
                var currentActionSprite = currentCharacter.CharacterActions.FirstOrDefault(c => c.Name == type).Sprite;

                return currentActionSprite;
            }
        }
    }
}
