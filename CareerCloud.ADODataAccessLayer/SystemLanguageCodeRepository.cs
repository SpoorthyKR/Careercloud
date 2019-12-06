using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
     public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connStr;

        public SystemLanguageCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

        }
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SystemLanguageCodePoco item in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                            ([LanguageID],
                             [Name],
                             [Native_Name])
                            VALUES 
                            (@LanguageID,
                             @Name,
                             @Native_Name)";

                    comm.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    comm.Parameters.AddWithValue("@Name", item.Name);
                    comm.Parameters.AddWithValue("@Native_Name", item.NativeName);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT [LanguageID],
                                           [Name], 
                                           [Native_Name]
                FROM [JOB_PORTAL_DB].[dbo].[System_Language_Codes]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemLanguageCodePoco[] appPocos = new SystemLanguageCodePoco[2000];
                while (rdr.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();

                    poco.LanguageID = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);
                    poco.NativeName = rdr.GetString(2);
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand() { Connection = conn };
                foreach (SystemLanguageCodePoco poco in items)
                {


                    cmd.CommandText = @"DELETE FROM  [dbo].[System_Language_Codes] WHERE  LanguageID = @LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
                
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                            SET  [Name] = @Name                                                                
                                ,[Native_Name] = @Native_Name                                                                
                                
                            WHERE [LanguageID] = @LanguageID
                                ";


                    comm.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    comm.Parameters.AddWithValue("@Name", poco.Name);
                    comm.Parameters.AddWithValue("@Native_Name", poco.NativeName);


                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
