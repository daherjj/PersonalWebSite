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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.CastPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Character = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CastPersons", new[] { "Movie_Id" });
            DropIndex("dbo.Genres", new[] { "Movie_Id" });
            DropForeignKey("dbo.CastPersons", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Genres", "Movie_Id", "dbo.Movies");
            DropTable("dbo.CastPersons");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
        }
    }
}
