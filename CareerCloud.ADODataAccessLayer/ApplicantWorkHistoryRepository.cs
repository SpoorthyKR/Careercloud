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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connStr;
        public ApplicantWorkHistoryRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                            ([Id],
                             [Applicant],
                             [Company_Name],
                             [Country_Code],
                             [Location],
                             [Job_Title],
                             [Job_Description],
                             [Start_Month],
                             [Start_Year],
                             [End_Month],
                             [End_Year])
                            VALUES 
                            (@Id,
                             @Applicant,
                             @Company_Name,
                             @Country_Code,
                             @Location,
                             @Job_Title,
                             @Job_Description,
                             @Start_Month,
                             @Start_Year,
                             @End_Month,
                             @End_Year)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT [Id],[Applicant],[Company_Name],[Country_Code],[Location],[Job_Title],[Job_Description],[Start_Month],[Start_Year],[End_Month],[End_Year],[Time_Stamp]
                FROM [JOB_PORTAL_DB].[dbo].[Applicant_Work_History]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantWorkHistoryPoco[] appPocos = new ApplicantWorkHistoryPoco[1000];
                while (rdr.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.CompanyName = rdr.GetString(2);
                    poco.CountryCode = rdr.GetString(3);
                    poco.Location = rdr.GetString(4);
                    poco.JobTitle = rdr.GetString(5);
                    poco.JobDescription = rdr.GetString(6);
                    poco.StartMonth = rdr.GetInt16(7);
                    poco.StartYear = rdr.GetInt32(8);
                    poco.EndMonth = rdr.GetInt16(9);
                    poco.EndYear = rdr.GetInt32(10);
                    poco.TimeStamp = (byte[])rdr[11];

                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand() { Connection = conn };
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM  [dbo].[Applicant_Work_History] WHERE  ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
                
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
               
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                            SET [Id] = @Id
                                ,[Applicant] = @Applicant
                                ,[Company_Name] = @Company_Name   
                                ,[Country_Code] = @Country_Code
                                ,[Location] = @Location
                                ,[Job_Title] = @Job_Title
                                ,[Job_Description] = @Job_Description
                                ,[Start_Month] = @Start_Month
                                ,[Start_Year] = @Start_Year
                                ,[End_Month] = @End_Month
                                ,[End_Year] = @End_Year
                            WHERE [Id] = @Id
                                ";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
