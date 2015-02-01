namespace Presentation.NET_Database_Toolkit
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Dapper;

    using Spritely.Cqrs;

    using Xunit;

    public class GetThingByIdQuery : IQuery<ThingDataAccessObject>
    {
        public int ThingId { get; set; }
    }

    public class GetThingByIdQueryHandler : IQueryHandler<GetThingByIdQuery, ThingDataAccessObject>
    {
        private readonly string connectionString;

        public GetThingByIdQueryHandler(string connectionstring)
        {
            this.connectionString = connectionstring;
        }

        public ThingDataAccessObject Handle(GetThingByIdQuery query)
        {
            ThingDataAccessObject ret = null;
            var sqlText = @"SELECT ThingId as [Id], 
                                  [Values] as [Values] 
                            FROM Thing 
                            WHERE ThingId = @ThingId";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                // ADO
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlText;
                command.Parameters.Add(new SqlParameter("ThingId", query.ThingId));
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    ret = new ThingDataAccessObject
                              {
                                  Id = reader.GetInt32(0), 
                                  Values = reader.GetString(1)
                              };
                }

                var adoValues = ret.Values;

                // Dapper
                ret = connection
                        .Query<ThingDataAccessObject>(sqlText, query)
                        .SingleOrDefault();

                Assert.Equal(adoValues, ret.Values);
            }

            return ret;
        }
    }
}
