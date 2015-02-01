namespace Presentation.NET_Database_Toolkit
{
    using System;
    using System.IO;
    using System.Linq;

    using Naos.Utils.Database.Tools;

    using Xunit;

    public class ThingDatabaseTests
    {
        // [Fact]
        public static void TestSchemaCreation()
        {
            var connectionString = "server=(local)\\SQLDEV2012;Trusted_connection=True";
            var dbName = "PresThing";

            var dbs = DatabaseManager.Retrieve(connectionString);
            if (dbs.Any(_ => _.DatabaseName == dbName))
            {
                DatabaseManager.Delete(connectionString, dbName);
            }

            var instanceDefaultDataPath = DatabaseManager.GetInstanceDefaultDataPath(connectionString);
            var instanceDefaultLogPath = DatabaseManager.GetInstanceDefaultLogPath(connectionString);
            var configuration = new DatabaseConfiguration()
                                {
                                    DatabaseName = dbName,
                                    DataFileCurrentSizeInKb = 10000,
                                    DataFileGrowthSizeInKb = 10000,
                                    DataFileLogicalName = dbName + "Dat",
                                    DataFileMaxSizeInKb = 10000,
                                    DataFilePath = Path.Combine(instanceDefaultDataPath, dbName + "Dat.dat"),
                                    LogFileCurrentSizeInKb = 10000,
                                    LogFileGrowthSizeInKb = 10000,
                                    LogFileLogicalName = dbName + "Log",
                                    LogFileMaxSizeInKb = 10000,
                                    LogFilePath = Path.Combine(instanceDefaultLogPath, dbName + "Log.log"),
                                };

            DatabaseManager.Create(connectionString, configuration);

            Naos.Database.Migrator.MigrationExecutor.Up(
                typeof(ThingDatabaseInitialMigration).Assembly,
                connectionString,
                dbName,
                ThingDatabaseInitialMigration.Versions.BaseMigrationVersion,
                Console.WriteLine);
        }
    }
}
