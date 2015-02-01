namespace Presentation.NET_Database_Toolkit
{
    using System;
    using System.Linq;

    using Xunit;

    public class ThingConversionExtensionMethodsTests
    {
        [Fact]
        public static void ToDataAccessObject_InputAllPropsSet_OutputAllPropsMatch()
        {
            // arrange
            var objectIn = new ThingDatabaseObject() { ThingId = 4, Values = "SomeValues" };

            // act
            var objectOut = objectIn.ToDataAccessObject();

            // assert
            Assert.Equal(objectIn.ThingId, objectOut.Id);
            Assert.Equal(objectIn.Values, objectOut.Values);

            // - make sure we know all the properties to test
            Assert.Equal(2, typeof(ThingDatabaseObject).GetProperties().Count());
            Assert.Equal(2, typeof(ThingWcfObject).GetProperties().Count());
        }
    }
}
