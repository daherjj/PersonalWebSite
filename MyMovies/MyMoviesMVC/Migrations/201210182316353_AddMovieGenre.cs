namespace MyMoviesMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieGenre : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieGenres", "Genres_Id", "dbo.Genres");
            DropForeignKey("dbo.MovieGenres", "Movies_Id", "dbo.Movies");
            DropForeignKey("dbo.MoviePaths", "Movies_Id", "dbo.Movies");
            DropIndex("dbo.MovieGenres", new[] { "Genres_Id" });
            DropIndex("dbo.MovieGenres", new[] { "Movies_Id" });
            DropIndex("dbo.MoviePaths", new[] { "Movies_Id" });
            AddColumn("dbo.MovieGenres", "Genre_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.MovieGenres", "Movies_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.MoviePaths", "Movies_ID", c => c.Int(nullable: false));
            DropColumn("dbo.MovieGenres", "Genres_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieGenres", "Genres_Id", c => c.Int());
            AlterColumn("dbo.MoviePaths", "Movies_Id", c => c.Int());
            AlterColumn("dbo.MovieGenres", "Movies_Id", c => c.Int());
            DropColumn("dbo.MovieGenres", "Genre_ID");
            CreateIndex("dbo.MoviePaths", "Movies_Id");
            CreateIndex("dbo.MovieGenres", "Movies_Id");
            CreateIndex("dbo.MovieGenres", "Genres_Id");
            AddForeignKey("dbo.MoviePaths", "Movies_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.MovieGenres", "Movies_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.MovieGenres", "Genres_Id", "dbo.Genres", "Id");
        }
    }
}
