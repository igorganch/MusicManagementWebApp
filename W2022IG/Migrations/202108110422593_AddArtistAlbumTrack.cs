namespace W2022IG.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArtistAlbumTrack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "MediaContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Tracks", "Media", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "Media");
            DropColumn("dbo.Tracks", "MediaContentType");
        }
    }
}
