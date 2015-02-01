namespace Presentation.NET_Database_Toolkit
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Dapper;

    using Xunit;

    public interface IThingDataAccess
    {
        ThingDataAccessObject GetThingById(int id);
    }

    public class ThingDataAccess : IThingDataAccess
    {
        private string connectionString;

        public ThingDataAccess(string connectionstring)
        {
            this.connectionString = connectionstring;
        }

        public ThingDataAccessObject GetThingById(int id)
        {
            ThingDataAccessObject ret = null;
            var sqlText = @"SELECT ThingId as [Id], 
                                  [Values] as [Values] 
                            FROM Thing 
                            WHERE ThingId = @Id";
            var sqlParams = new { Id = id };
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                // ADO
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlText;
                command.Parameters.Add(new SqlParameter("Id", sqlParams.Id));
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
                        .Query<ThingDataAccessObject>(sqlText, sqlParams)
                        .SingleOrDefault();

                Assert.Equal(adoValues, ret.Values);
            }

            return ret;
        }
    }
}
