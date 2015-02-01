namespace Presentation.NET_Database_Toolkit
{
    public static class ThingConversionExtensionMethods
    {
        public static ThingDataAccessObject ToDataAccessObject(this ThingDatabaseObject source)
        {
            return new ThingDataAccessObject { Id = source.ThingId, Values = source.Values };
        }

        public static ThingWcfObject ToThingWcfObject(this ThingDataAccessObject source)
        {
            return new ThingWcfObject { Id = source.Id, Values = source.Values };
        }


        public static ThingRestObject ToThingRestObject(this ThingDataAccessObject source)
        {
            return new ThingRestObject { Id = source.Id, Values = source.Values };
        }
    }
}