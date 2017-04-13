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

namespace BattleOfFaiths.Game.Components
{
    public class Fighter
    {
        private Texture2D fighterImage;
        private Vector2 fighterPosition;
        private Character character;
        
        private Vector2 basicAttackPosition;
        Animation basicAttack = new Animation();
        
        private Vector2 specialAttackPosition;
        Animation specialAttack = new Animation();

        private Vector2 defencePosition;
        Animation defence = new Animation();

        private Vector2 winPosition;
        Animation win = new Animation();

        private Vector2 losePosition;
        Animation lose = new Animation();
        
        public Fighter(Character character)
        {
            this.character = character;
        }

        public Animation BasicAttack => basicAttack;
        public Animation SpecialAttack => specialAttack;
        public Animation Defence => defence;
        public Animation Win => win;
        public Animation Lose => lose;
        public Vector2 Position => fighterPosition;
        public Texture2D Image => fighterImage;
        public Character Character => character;

        public void Initialize()
        {
            fighterPosition = new Vector2(175, 220);
            basicAttackPosition = new Vector2(410, 220);
            basicAttack.Initialize(basicAttackPosition, new Vector2(GetCharacterActionFrames("Basic", character), 1));
            specialAttackPosition = new Vector2(410, 220);
            specialAttack.Initialize(specialAttackPosition, new Vector2(GetCharacterActionFrames("Special", character), 1));
            defencePosition = new Vector2(175, 220);
            defence.Initialize(defencePosition, new Vector2(GetCharacterActionFrames("Defence", character), 1));
            winPosition = new Vector2(175, 220);
            win.Initialize(winPosition, new Vector2(GetCharacterActionFrames("Win", character), 1));
            losePosition = new Vector2(175, 220);
            lose.Initialize(losePosition, new Vector2(GetCharacterActionFrames("Lose", character), 1));
        }

        public void LoadContent(ContentManager Content)
        {
            fighterImage = Content.Load<Texture2D>("Characters/" + character.Sprite);
            basicAttack.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Basic", character));
            specialAttack.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Special", character));
            defence.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Defence", character));
            win.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Win", character));
            lose.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Lose", character));
        }

        public void Update(GameTime gameTime)
        {
            if (basicAttack.Active)
            {
                basicAttack.Update(gameTime);
            }
            else if (specialAttack.Active)
            {
                specialAttack.Update(gameTime);
            }
            else if (defence.Active)
            {
                defence.Update(gameTime);
            }
            else if (win.Active)
            {
                win.Update(gameTime);
            }
            else if (lose.Active)
            {
                lose.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (basicAttack.Active)
                basicAttack.Draw(spriteBatch);
            else if (specialAttack.Active)
                specialAttack.Draw(spriteBatch);
            else if (defence.Active)
                defence.Draw(spriteBatch);
            else if (win.Active)
                win.Draw(spriteBatch);
            else if (lose.Active)
                lose.Draw(spriteBatch);
            else spriteBatch.Draw(fighterImage, fighterPosition, Color.White);
        }

        private string GetCharacterActionSprite(string type, Character character)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentCharacter = context.Characters.FirstOrDefault(c => c.Id == character.Id);
                var currentActionSprite = currentCharacter.CharacterActions.FirstOrDefault(a => a.Name == type).PlayerSprite;

                return currentActionSprite;
            }
        }

        private int GetCharacterActionFrames(string type, Character character)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentCharacter = context.Characters.FirstOrDefault(c => c.Id == character.Id);
                var currentActionFrames = currentCharacter.CharacterActions.FirstOrDefault(a => a.Name == type).Frames;

                return currentActionFrames;
            }
        }
    }
}
