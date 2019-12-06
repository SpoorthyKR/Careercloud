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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string _connStr;
        public CompanyJobDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                            ([Id],
                             [Job],
                             [Job_Name],
                             [Job_Descriptions])
                            VALUES 
                            (@Id,
                             @Job,
                             @Job_Name,
                             @Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT [Id],
                                           [Job],
                                           [Job_Name],
                                           [Job_Descriptions],
                                           [Time_Stamp]
                FROM [JOB_PORTAL_DB].[dbo].[Company_Jobs_Descriptions]";
                connection.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobDescriptionPoco[] appPocos = new CompanyJobDescriptionPoco[2000];
                while (rdr.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.JobName = rdr.GetString(2);
                    poco.JobDescriptions = rdr.GetString(3);
                    poco.TimeStamp = (byte[])rdr[4];

                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
               
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand() { Connection = conn };
                    cmd.CommandText = @"DELETE FROM  [dbo].[Company_Jobs_Descriptions] WHERE  ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }       
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                            SET [Id] = @Id
                                ,[Job] = @Job
                                ,[Job_Name] = @Job_Name
                                ,[Job_Descriptions] = @Job_Descriptions                                
                            WHERE [Id] = @Id
                                ";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }

}
