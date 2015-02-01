namespace Presentation.NET_Database_Toolkit
{
    using System;

    using FluentMigrator;

    [Migration(Versions.BaseMigrationVersion)]
    public class ThingDatabaseInitialMigration : Migration
    {
        public class Versions
        {
            public const long BaseMigrationVersion = 0;
        }

        public static ThingDatabaseObject SeedData = new ThingDatabaseObject() { ThingId = 0, Values = "Monkey" };

        public override void Up()
        {
            Create.Table("Thing")
                .WithColumn("ThingId")
                .AsInt32()
                .PrimaryKey()
                .WithColumn("Values")
                .AsString();
            Insert.IntoTable("Thing").Row(SeedData);
        }

        public override void Down()
        {
            Delete.Table("WorkingTranslate_v1");
        }
    }
}