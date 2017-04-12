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

        public Fighter(Character character)
        {
            this.character = character;
        }

        public Animation BasicAttack => basicAttack;
        public Animation SpecialAttack => specialAttack;

        public Vector2 Position
        {
            get { return fighterPosition; }
        }

        public Texture2D Image
        {
            get { return fighterImage; }
        }

        public Character Character
        {
            get { return character; }
        }

        public void Initialize()
        {
            fighterPosition = new Vector2(175, 250);
            basicAttackPosition = new Vector2(175, 250);
            basicAttack.Initialize(basicAttackPosition, new Vector2(6, 1));
            specialAttackPosition = new Vector2(175, 250);
            specialAttack.Initialize(specialAttackPosition, new Vector2(4, 1));
            defencePosition = new Vector2(175, 250);
            defence.Initialize(defencePosition, new Vector2(4, 1));
        }

        public void LoadContent(ContentManager Content)
        {
            fighterImage = Content.Load<Texture2D>("Characters/" + character.Sprite);
            basicAttack.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterBasicAttackSprite("Basic", character));
            specialAttack.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterBasicAttackSprite("Special", character));
            defence.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterBasicAttackSprite("Defence", character));
        }

        public void Update(GameTime gameTime)
        {
            if (basicAttack.Active)
            //basicAttack.Active = true;
                basicAttack.Update(gameTime);
            else if (specialAttack.Active) 
            //specialAttack.Active = true;
                specialAttack.Update(gameTime);
            //defence.Active = true;
            //defence.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (basicAttack.Active)
                basicAttack.Draw(spriteBatch);
            else if (specialAttack.Active)
                specialAttack.Draw(spriteBatch);
            else spriteBatch.Draw(fighterImage, fighterPosition, Color.White);
            //defence.Draw(spriteBatch);
        }

        private string GetCharacterBasicAttackSprite(string type, Character character)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentCharacter = context.Characters.FirstOrDefault(c => c.Id == character.Id);
                var currentAction = currentCharacter.CharacterActions.FirstOrDefault(a => a.Name == type);
                var actionAnimation =
                    context.ActionAnimations.FirstOrDefault(aa => aa.Id == currentAction.ActionAnimationId);
                string spriteName = actionAnimation.Sprite;

                return spriteName;
            }
        }
    }
}
