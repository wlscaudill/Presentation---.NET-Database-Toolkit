namespace Presentation.NET_Database_Toolkit
{
    using System.Linq;

    using AutoMapper;

    using Xunit;

    public class ThingDataAccessTests
    {
        [Fact]
        public static void GetThingById_ValidId_ReturnsValidResut()
        {
            // arrange
            Mapper.CreateMap<ThingDatabaseObject, GetThingByIdQuery>();
            Mapper.AssertConfigurationIsValid();
            var getThingByIdQueryHandler = new GetThingByIdQueryHandler("server=(local)\\SQLDEV2012;database=PresThing;Trusted_connection=True");

            // act
            var result = getThingByIdQueryHandler.Handle(Mapper.Map<GetThingByIdQuery>(ThingDatabaseInitialMigration.SeedData));

            // assert
            Assert.Equal(ThingDatabaseInitialMigration.SeedData.ThingId, result.Id);
            Assert.Equal(ThingDatabaseInitialMigration.SeedData.Values, result.Values);

            // - make sure we're testing all of the properties we assert the count we're testing...
            Assert.Equal(2, typeof(ThingDatabaseObject).GetProperties().Count());
        }
    }
}
