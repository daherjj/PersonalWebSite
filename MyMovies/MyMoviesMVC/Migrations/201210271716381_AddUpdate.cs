namespace MyMoviesMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.CastPersons", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Genres", new[] { "Movie_Id" });
            DropIndex("dbo.CastPersons", new[] { "Movie_Id" });
            CreateTable(
                "dbo.MovieCasts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cast_Id = c.Int(),
                        Movies_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CastPersons", t => t.Cast_Id)
                .ForeignKey("dbo.Movies", t => t.Movies_Id)
                .Index(t => t.Cast_Id)
                .Index(t => t.Movies_Id);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Genre_ID = c.Int(nullable: false),
                        Movies_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Movies_ID = c.Int(nullable: false),
                        path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Genres", "Movie_Id");
            DropColumn("dbo.CastPersons", "Movie_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CastPersons", "Movie_Id", c => c.Int());
            AddColumn("dbo.Genres", "Movie_Id", c => c.Int());
            DropIndex("dbo.MovieCasts", new[] { "Movies_Id" });
            DropIndex("dbo.MovieCasts", new[] { "Cast_Id" });
            DropForeignKey("dbo.MovieCasts", "Movies_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieCasts", "Cast_Id", "dbo.CastPersons");
            DropTable("dbo.MoviePaths");
            DropTable("dbo.MovieGenres");
            DropTable("dbo.MovieCasts");
            CreateIndex("dbo.CastPersons", "Movie_Id");
            CreateIndex("dbo.Genres", "Movie_Id");
            AddForeignKey("dbo.CastPersons", "Movie_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.Genres", "Movie_Id", "dbo.Movies", "Id");
        }
    }
}
