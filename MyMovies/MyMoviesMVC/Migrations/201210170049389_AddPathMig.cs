namespace MyMoviesMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPathMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoviePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        path = c.String(),
                        Movies_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movies_Id)
                .Index(t => t.Movies_Id);
            
            DropColumn("dbo.Movies", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Path", c => c.String());
            DropIndex("dbo.MoviePaths", new[] { "Movies_Id" });
            DropForeignKey("dbo.MoviePaths", "Movies_Id", "dbo.Movies");
            DropTable("dbo.MoviePaths");
        }
    }
}
