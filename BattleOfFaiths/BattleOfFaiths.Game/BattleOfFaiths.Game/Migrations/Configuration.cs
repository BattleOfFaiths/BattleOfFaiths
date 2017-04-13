namespace BattleOfFaiths.Game.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BattleOfFaiths.Game.Data.BattleOfFaithsEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BattleOfFaiths.Game.Data.BattleOfFaithsEntities";
        }

        protected override void Seed(BattleOfFaiths.Game.Data.BattleOfFaithsEntities context)
        {
            //-------------- Items ------------------
            var health50 = new Item
            {
                Name = "Health +50",
                Price = 100,
                Sprite = "heart50"
            };
            var healtFull = new Item
            {
                Name = "Health-Full",
                Price = 800,
                Sprite = "heartFull"
            };
            var mana50 = new Item
            {
                Name = "Mana +50",
                Price = 80,
                Sprite = "mana50"
            };
            var mana100 = new Item
            {
                Name = "Mana +100",
                Price = 130,
                Sprite = "mana100"
            };
            var maxDef = new Item
            {
                Name = "Max Defence",
                Price = 750,
                Sprite = "def"
            };
            var attackBoost = new Item
            {
                Name = "Attack +100",
                Price = 600,
                Sprite = "Attack100"
            };
            context.Items.AddOrUpdate(i => i.Name, health50, healtFull, mana50, mana100, maxDef, attackBoost);
            context.SaveChanges();

            //--------------- Characters ----------------//
            var Alucard = new Character
            {
                Name = "Alucard",
                Level = 0,
                Sprite = "Al-Position",
                EnemySprite = "Al-Position1",
                Highscore = 0,
                Health = 100,
                Mana = 50,
                Attack = 30,
                SpecAttack = 60
            };
            var Belmont = new Character
            {
                Name = "Belmont",
                Level = 0,
                Sprite = "BelPosition",
                EnemySprite = "BelPosition1",
                Highscore = 0,
                Health = 100,
                Mana = 80,
                Attack = 30,
                SpecAttack = 40
            };
            var Rebel = new Character
            {
                Name = "Rebel",
                Level = 0,
                Sprite = "RabPosition",
                EnemySprite = "RabPosition1",
                Highscore = 0,
                Health=100,
                Mana = 10,
                Attack = 20,
                SpecAttack = 80

            };
            context.Characters.AddOrUpdate(c => c.Name, Alucard, Belmont, Rebel);
            context.SaveChanges();

            //-------------------- Character Actions ------------------------//
            // ------------------- Alucard --------------------
            var AlAttack = new CharacterAction
            {
                Name = "Basic",
                Sprite = "BasicAttack",
                PlayerSprite = "Al-Attack",
                EnemySprite = "Al-Attack1",
                CharacterId = Alucard.Id,
                Frames = 6
            };
            var AlAttackSpec = new CharacterAction
            {
                Name = "Special",
                Sprite = "SpecAttack",
                PlayerSprite = "Al-Specattack",
                EnemySprite = "Al-Specattack1",
                CharacterId = Alucard.Id,
                Frames = 4
            };
            var AlWin= new CharacterAction
            {
                Name = "Win",
                Sprite = "manaSprite",
                PlayerSprite = "Al-Win",
                EnemySprite = "Al-Win1",
                CharacterId = Alucard.Id,
                Frames = 4
            };
            var AlLose = new CharacterAction
            {
                Name = "Lose",
                Sprite = "manaSprite",
                PlayerSprite = "Al-Lose",
                EnemySprite = "Al-Lose1",
                CharacterId = Alucard.Id,
                Frames = 4
            };
            var AlDef = new CharacterAction
            {
                Name = "Defence",
                Sprite = "manaSprite",
                PlayerSprite = "Al-Defence",
                EnemySprite = "Al-Defence1",
                CharacterId = Alucard.Id,
                Frames = 3
            };
            var AlHit = new CharacterAction
            {
                Name = "Hit",
                Sprite = "manaSprite",
                PlayerSprite = "Al-Hit",
                EnemySprite = "Al-Hit1",
                CharacterId = Alucard.Id,
                Frames = 3
            };
            //---------------- Belmont -----------------------//
            var BelAtt = new CharacterAction
            {
                Name = "Basic",
                Sprite = "BasicAttack",
                PlayerSprite = "BelAttack",
                EnemySprite = "BelAttack1",
                CharacterId = Belmont.Id,
                Frames = 3
            };
            var BelAttackSpec = new CharacterAction
            {
                Name = "Special",
                Sprite = "SpecAttack",
                PlayerSprite = "BelSpecAttack",
                EnemySprite = "BelSpecAttack1",
                CharacterId = Belmont.Id,
                Frames = 6
            };
            var BelWin = new CharacterAction
            {
                Name = "Win",
                Sprite = "manaSprite",
                PlayerSprite = "BelWin",
                EnemySprite = "BelWin1",
                CharacterId = Belmont.Id,
                Frames = 3
            };
            var BelLose = new CharacterAction
            {
                Name = "Lose",
                Sprite = "manaSprite",
                PlayerSprite = "BelLose",
                EnemySprite = "BelLose1",
                CharacterId = Belmont.Id,
                Frames = 4
            };
            var BelDef = new CharacterAction
            {
                Name = "Defence",
                Sprite = "manaSprite",
                PlayerSprite = "BelDefence",
                EnemySprite = "BelDefence1",
                CharacterId = Belmont.Id,
                Frames = 2
            };
            var BelHit = new CharacterAction
            {
                Name = "Hit",
                Sprite = "manaSprite",
                PlayerSprite = "BelHit",
                EnemySprite = "BelHit1",
                CharacterId = Belmont.Id,
                Frames = 3
            };
            //-------------------- Rebel -------------------//
            var RebAtt = new CharacterAction
            {
                Name = "Basic",
                Sprite = "BasicAttack",
                PlayerSprite = "Attack",
                EnemySprite = "Attack1",
                CharacterId = Rebel.Id,
                Frames = 4
            };
            var RebAttackSpec = new CharacterAction
            {
                Name = "Special",
                Sprite = "SpecAttack",
                PlayerSprite = "SpecAttack",
                EnemySprite = "SpecAttack1",
                CharacterId = Rebel.Id,
                Frames = 5
            };
            var RebWin = new CharacterAction
            {
                Name = "Win",
                Sprite = "manaSprite",
                PlayerSprite = "Win",
                EnemySprite = "Win1",
                CharacterId = Rebel.Id,
                Frames = 4
            };
            var RebLose = new CharacterAction
            {
                Name = "Lose",
                Sprite = "manaSprite",
                PlayerSprite = "Lose",
                EnemySprite = "Lose1",
                CharacterId = Rebel.Id,
                Frames = 4
            };
            var RebDef = new CharacterAction
            {
                Name = "Defence",
                Sprite = "manaSprite",
                PlayerSprite = "Defence",
                EnemySprite = "Defence1",
                CharacterId = Rebel.Id,
                Frames = 4
            };
            var RebHit = new CharacterAction
            {
                Name = "Hit",
                Sprite = "manaSprite",
                PlayerSprite = "RabHit",
                EnemySprite = "RabHit",
                CharacterId = Belmont.Id,
                Frames = 3
            };
            context.CharacterActions.AddOrUpdate(a => a.PlayerSprite, AlAttack, AlAttackSpec, AlWin, AlLose, AlDef, AlHit, BelAtt, BelAttackSpec, BelWin, BelLose, BelDef, BelHit, RebAtt, RebAttackSpec, RebWin, RebLose, RebDef, RebHit);
            context.SaveChanges();

        }
    }
}
