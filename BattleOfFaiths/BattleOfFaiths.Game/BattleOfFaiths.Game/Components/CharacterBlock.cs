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
    public class CharacterBlock
    {
        private SpriteFont font;

        private Button fightButton;
        private Vector2 fightButtonPos;

        private string fighterData;
        private Vector2 position;

        private Texture2D fighterProfile;
        private Character character;
        private Vector2 characterPos;

        public CharacterBlock(Character character, Vector2 characterPos)
        {
            this.character = character;
            this.characterPos = characterPos;
        }

        public void Initialize()
        {
            fighterData = GetFighterData(character);
            position = new Vector2(characterPos.X + 100, characterPos.Y);
            fightButtonPos = new Vector2(position.X + 200, position.Y);
            fightButton = new Button("Fight", fightButtonPos, this.character);
        }

        public void LoadContent(ContentManager Content)
        {
            fightButton.LoadContent(Content);
            font = Content.Load<SpriteFont>("Fonts/font");
            fighterProfile = Content.Load<Texture2D>("Characters/" + character.Sprite);
        }

        public void Update()
        {
            fightButton.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fighterProfile, characterPos, Color.White);
            spriteBatch.DrawString(font, fighterData, position, Color.Black);
            fightButton.Draw(spriteBatch);
        }

        private string GetFighterData(Character character)
        {
            //using (var context = new BattleOfFaithsEntities())
            //{
                //var ch = context.Characters.FirstOrDefault(c => c.Name == character.Name);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(character.Name);
                sb.AppendLine($"Level: {character.Level}");
                sb.AppendLine($"Highscore: {character.Highscore}");

                return sb.ToString();
            //}
        }
    }
}
