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
   public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string _connStr;
        public ApplicantResumeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                            ([Id],[Applicant],[Resume],[Last_Updated])
                            VALUES 
                            (@Id,@Applicant,@Resume,@Last_Updated)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);


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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT [Id],[Applicant],[Resume],[Last_Updated]
                FROM [JOB_PORTAL_DB].[dbo].[Applicant_Resumes]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantResumePoco[] appPocos = new ApplicantResumePoco[1000];
                while (rdr.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Resume = rdr.GetString(2);
                    if (!rdr.IsDBNull(3)) poco.LastUpdated = rdr.GetDateTime(3);

                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand() { Connection = conn };
                foreach (ApplicantResumePoco poco in items)
                {

                    cmd.CommandText = @"DELETE FROM  [dbo].[Applicant_Resumes] WHERE  ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                            SET [Id] = @Id
                                ,[Applicant] = @Applicant
                                ,[Resume] = @Resume   
                                ,[Last_Updated] = @Last_Updated
                            WHERE [Id] = @Id
                                ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
