using BattleOfFaiths.Game.Migrations;
using BattleOfFaiths.Game.Models;

namespace BattleOfFaiths.Game.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BattleOfFaithsEntities : DbContext
    {
        public BattleOfFaithsEntities()
            : base("name=BattleOfFaithsEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BattleOfFaithsEntities, Configuration>());
        }

        public virtual DbSet<Character> Characters { get; set; }        
        public virtual DbSet<CharacterAction> CharacterActions { get; set; }
        public virtual DbSet<Models.Game> Games { get; set; }
        public virtual DbSet<Bonus> Bonuses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Fight> Fights { get; set; }
   

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Game>()
                .HasMany(g => g.Characters)
                .WithMany(c => c.Games)
                .Map(gc =>
                {
                    gc.MapLeftKey("GameId");
                    gc.MapRightKey("CharacterId");
                    gc.ToTable("GamesCharacters");
                });

            modelBuilder.Entity<Models.Game>()
                .HasMany(g => g.Items)
                .WithMany(i => i.Games)
                .Map(gi =>
                {
                    gi.MapLeftKey("GameId");
                    gi.MapRightKey("ItemId");
                    gi.ToTable("GamesItems");
                });

            modelBuilder.Entity<Bonus>().ToTable("Bonuses");        

            base.OnModelCreating(modelBuilder);
        }
    }
}