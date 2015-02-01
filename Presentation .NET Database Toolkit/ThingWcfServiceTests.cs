namespace Presentation.NET_Database_Toolkit
{
    using System.Collections.Generic;

    using Moq;

    using System.Linq;

    using Spritely.Cqrs;

    using Xunit;

    public class ThingWcfServiceTests
    {
        [Fact]
        public static void GetThing_ValidId_ValidResult()
        {
            // arrange
            var expectedThing = new ThingDataAccessObject() { Id = 0, Values = "Monkey" };
            var thingMap = new Dictionary<int, ThingDataAccessObject> { { expectedThing.Id, expectedThing } };
            var mockGetThingByIdQueryHandler = new Mock<IQueryHandler<GetThingByIdQuery, ThingDataAccessObject>>();
            mockGetThingByIdQueryHandler
                .Setup(_ => _.Handle(It.IsAny<GetThingByIdQuery>()))
                .Returns<GetThingByIdQuery>(query => thingMap[query.ThingId]);
            var service = new ThingWcfService(mockGetThingByIdQueryHandler.Object);

            // act
            var actualThing = service.GetThing(expectedThing.Id);

            // assert
            Assert.Equal(expectedThing.ToThingWcfObject().Id, actualThing.Id);
            Assert.Equal(expectedThing.ToThingWcfObject().Values, actualThing.Values);

            // - make sure we're testing all of the properties we assert the count we're testing...
            Assert.Equal(2, typeof(ThingWcfObject).GetProperties().Count());
        }
    }
}
