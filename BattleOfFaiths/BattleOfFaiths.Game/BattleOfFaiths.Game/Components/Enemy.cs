using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleOfFaiths.Game.Data;
using BattleOfFaiths.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game.Components
{
    public class Enemy
    {
        private List<Character> enemies;
        private Character enemy;
        private Texture2D enemyImage;
        private Vector2 enemyPosition;

        public Vector2 Position => enemyPosition;
        public Texture2D Image => enemyImage;
        public Character Character => enemy;

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

        private Vector2 hitPosition;
        Animation hit = new Animation();

        public Animation BasicAttack => basicAttack;
        public Animation SpecialAttack => specialAttack;
        public Animation Defence => defence;
        public Animation Win => win;
        public Animation Lose => lose;
        public Animation Hit => hit;

        public void Initialize()
        {
            enemies = new List<Character>();
            enemies = GetAllCharacters();
            int num;
            Random rnd = new Random();
            num = rnd.Next(1, enemies.Count);
            enemy = enemies[num];
            enemyPosition = new Vector2(500, 220);
            basicAttackPosition = new Vector2(230, 220);
            basicAttack.Initialize(basicAttackPosition, new Vector2(GetCharacterActionFrames("Basic", enemy), 1));
            specialAttackPosition = new Vector2(230, 220);
            specialAttack.Initialize(specialAttackPosition, new Vector2(GetCharacterActionFrames("Special", enemy), 1));
            defencePosition = new Vector2(500, 220);
            defence.Initialize(defencePosition, new Vector2(GetCharacterActionFrames("Defence", enemy), 1));
            winPosition = new Vector2(500, 220);
            win.Initialize(winPosition, new Vector2(GetCharacterActionFrames("Win", enemy), 1));
            losePosition = new Vector2(500, 220);
            lose.Initialize(losePosition, new Vector2(GetCharacterActionFrames("Lose", enemy), 1));
            hitPosition = new Vector2(500, 220);
            hit.Initialize(hitPosition, new Vector2(GetCharacterActionFrames("Hit", enemy), 1));
        }

        public void LoadContent(ContentManager Content)
        {
            enemyImage = Content.Load<Texture2D>("Characters/" + enemy.EnemySprite);
            basicAttack.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Basic", enemy));
            specialAttack.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Special", enemy));
            defence.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Defence", enemy));
            win.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Win", enemy));
            lose.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Lose", enemy));
            hit.AnimationImage = Content.Load<Texture2D>("Animations/" + GetCharacterActionSprite("Hit", enemy));
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
            else if (hit.Active)
            {
                hit.Update(gameTime);
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
            else if (hit.Active)
                hit.Draw(spriteBatch);
            else
                spriteBatch.Draw(enemyImage, enemyPosition, Color.White);
        }

        private List<Character> GetAllCharacters()
        {
            using (var context = new BattleOfFaithsEntities())
            {
                return context.Characters.ToList();
            }
        }

        private string GetCharacterActionSprite(string type, Character character)
        {
            using (var context = new BattleOfFaithsEntities())
            {
                var currentCharacter = context.Characters.FirstOrDefault(c => c.Id == character.Id);
                var currentActionSprite = currentCharacter.CharacterActions.FirstOrDefault(a => a.Name == type).EnemySprite;

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
