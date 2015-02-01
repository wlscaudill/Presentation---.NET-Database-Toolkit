namespace Presentation.NET_Database_Toolkit
{
    using AutoMapper;

    using Spritely.Cqrs;

    using Xunit;

    public class ThingWcfService
    {
        private readonly IQueryHandler<GetThingByIdQuery, ThingDataAccessObject> getThingByIdQueryHandler;

        public ThingWcfService(IQueryHandler<GetThingByIdQuery, ThingDataAccessObject> getThingByIdQueryHandler)
        {
            this.getThingByIdQueryHandler = getThingByIdQueryHandler;
            RunDiagnostics();
        }

        public static void RunDiagnostics()
        {
            Mapper.CreateMap<ThingDataAccessObject, ThingWcfObject>();
            Mapper.AssertConfigurationIsValid();
        }

        public ThingWcfObject GetThing(int id)
        {
            var dao = this.getThingByIdQueryHandler.Handle(new GetThingByIdQuery() { ThingId = id });

            var extMethodRet = dao.ToThingWcfObject();
            var autoMapperRet = Mapper.Map<ThingWcfObject>(dao);

            return autoMapperRet;
        }

    }
}