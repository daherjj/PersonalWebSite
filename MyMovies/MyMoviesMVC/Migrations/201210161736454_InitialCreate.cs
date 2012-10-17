namespace MyMoviesMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImdbId = c.String(),
                        Name = c.String(),
                        Runtime = c.String(),
                        Released = c.String(),
                        MovieCast_Id = c.Int(),
                        MovieGenre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieCasts", t => t.MovieCast_Id)
                .ForeignKey("dbo.MovieGenres", t => t.MovieGenre_Id)
                .Index(t => t.MovieCast_Id)
                .Index(t => t.MovieGenre_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        MovieGenre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieGenres", t => t.MovieGenre_Id)
                .Index(t => t.MovieGenre_Id);
            
            CreateTable(
                "dbo.CastPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Character = c.String(),
                        MovieCast_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieCasts", t => t.MovieCast_Id)
                .Index(t => t.MovieCast_Id);
            
            CreateTable(
                "dbo.MovieCasts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CastPersons", new[] { "MovieCast_Id" });
            DropIndex("dbo.Genres", new[] { "MovieGenre_Id" });
            DropIndex("dbo.Movies", new[] { "MovieGenre_Id" });
            DropIndex("dbo.Movies", new[] { "MovieCast_Id" });
            DropForeignKey("dbo.CastPersons", "MovieCast_Id", "dbo.MovieCasts");
            DropForeignKey("dbo.Genres", "MovieGenre_Id", "dbo.MovieGenres");
            DropForeignKey("dbo.Movies", "MovieGenre_Id", "dbo.MovieGenres");
            DropForeignKey("dbo.Movies", "MovieCast_Id", "dbo.MovieCasts");
            DropTable("dbo.MovieGenres");
            DropTable("dbo.MovieCasts");
            DropTable("dbo.CastPersons");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
        }
    }
}
