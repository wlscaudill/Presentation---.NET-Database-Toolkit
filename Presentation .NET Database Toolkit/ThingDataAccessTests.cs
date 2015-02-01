namespace Presentation.NET_Database_Toolkit
{
    using System.Linq;

    using Xunit;

    public class ThingDataAccessTests
    {
        [Fact]
        public static void GetThingById_ValidId_ReturnsValidResut()
        {
            // arrange

            var dataAccess = new ThingDataAccess("server=(local)\\SQLDEV2012;database=PresThing;Trusted_connection=True");

            // act
            var result = dataAccess.GetThingById(ThingDatabaseInitialMigration.SeedData.ThingId);

            // assert
            Assert.Equal(ThingDatabaseInitialMigration.SeedData.ThingId, result.Id);
            Assert.Equal(ThingDatabaseInitialMigration.SeedData.Values, result.Values);

            // - make sure we're testing all of the properties we assert the count we're testing...
            Assert.Equal(2, typeof(ThingDatabaseObject).GetProperties().Count());
        }
    }
}
