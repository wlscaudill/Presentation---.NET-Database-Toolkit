namespace Presentation.NET_Database_Toolkit
{
    using AutoMapper;

    public class ThingWcfService
    {
        private string connectionString;

        public ThingWcfService(string connectionString)
        {
            this.connectionString = connectionString;
            RunDiagnostics();
        }

        public static void RunDiagnostics()
        {
            Mapper.CreateMap<ThingDataAccessObject, ThingWcfObject>();
            Mapper.AssertConfigurationIsValid();
        }

        public ThingWcfObject GetThing(int id)
        {
            var dao = new ThingDataAccess(this.connectionString).GetThingById(id);

            var extMethodRet = dao.ToThingWcfObject();
            var autoMapperRet = Mapper.Map<ThingWcfObject>(dao);

            return null;
        }

    }
}