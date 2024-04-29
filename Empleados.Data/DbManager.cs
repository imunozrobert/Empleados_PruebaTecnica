using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Empleados.Data
{
    public class DbManager : IDisposable
    {
        public SqlConnection sqlConn;
        public DbManager()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            string connectionString = string.Empty;

            var server = string.Empty;
            var user = string.Empty;
            var password = string.Empty;
            var dataBase = string.Empty;

            
            server = config.GetValue<string>("DbConnection:Server");
            user = config.GetValue<string>("DbConnection:User");
            password = config.GetValue<string>("DbConnection:Password");
            dataBase = config.GetValue<string>("DbConnection:DataBase");
            

            connectionString = $"Data Source={server};Initial Catalog ={dataBase};User ID ={user};Password={password};TrustServerCertificate=True;";
            sqlConn = new SqlConnection(connectionString);
        }

        public void Dispose()
        {

        }

        public IEnumerable<T> ProcedimientoAlmacenado<T>(string SP, object parametros = null)
        {
            try
            {
                return sqlConn.Query<T>(SP, parametros, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcedimientoAlmacenado(string SP, object parametros = null)
        {
            try
            {
                sqlConn.Execute(SP, parametros, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ProcedimientoAlmacenado(string SP, ref DataSet ds, string[] parametros, object[] resultados)
        {
            SqlCommand functionReturnValue = default(SqlCommand);
            try
            {
                functionReturnValue = new SqlCommand(SP, sqlConn);
                functionReturnValue.CommandType = CommandType.StoredProcedure;

                //sqlcnx.Open();

                functionReturnValue.CommandTimeout = 0;
                if ((parametros != null))
                {
                    for (int l = 0; l <= parametros.Length - 1; l++)
                    {
                        functionReturnValue.Parameters.AddWithValue(parametros[l], resultados[l]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(functionReturnValue);
                ds = new DataSet();
                da.Fill(ds);

                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
