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

        public Vector2 Position
        {
            get { return enemyPosition; }
        }

        public Texture2D Image
        {
            get { return enemyImage; }
        }

        public Character Character
        {
            get { return enemy; }
        }

        public void Initialize()
        {
            enemies = new List<Character>();
            enemies = GetAllCharacters();
            int num;
            Random rnd = new Random();
            num = rnd.Next(1, enemies.Count);
            enemy = enemies[num];
            enemyPosition = new Vector2(570, 250);
        }

        public void LoadContent(ContentManager Content)
        {
            enemyImage = Content.Load<Texture2D>("Characters/" + enemy.Sprite);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyImage, enemyPosition, Color.White);
        }

        private List<Character> GetAllCharacters()
        {
            using (var context = new BattleOfFaithsEntities())
            {
                return context.Characters.ToList();
            }
        }
    }
}
