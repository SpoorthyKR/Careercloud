using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
   public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string _connStr;

        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                            ([Id],[Login],[Current_Salary],[Current_Rate],[Currency],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])
                            VALUES 
                            (@Id,@Login,@Current_Salary,@Current_Rate,@Currency,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT [Id],[Login],[Current_Salary],[Current_Rate],[Currency],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code],[Time_Stamp]
                FROM [JOB_PORTAL_DB].[dbo].[Applicant_Profiles]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantProfilePoco[] appPocos = new ApplicantProfilePoco[1000];
                while (rdr.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.CurrentSalary = rdr.GetDecimal(2);
                    poco.CurrentRate = rdr.GetDecimal(3);
                    poco.Currency = rdr.GetString(4);
                    poco.Country = rdr.GetString(5);
                    poco.Province = rdr.GetString(6);
                    poco.Street = rdr.GetString(7);
                    poco.City = rdr.GetString(8);
                    poco.PostalCode = rdr.GetString(9);
                    poco.TimeStamp = (byte[])rdr[10];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantProfilePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand() { Connection = conn };
                    cmd.CommandText = @"DELETE FROM  [dbo].[Applicant_Profiles] WHERE  ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                            SET [Id] = @Id
                                ,[Login] = @Login
                                ,[Current_Salary] = @Current_Salary
                                ,[Current_Rate] = @Current_Rate
                                ,[Currency] = @Currency
                                ,[Country_Code] = @Country_Code
                                ,[State_Province_Code] = @State_Province_Code
                                ,[Street_Address] = @Street_Address
                                ,[City_Town] = @City_Town
                                ,[Zip_Postal_Code] = @Zip_Postal_Code
                            WHERE [Id] = @Id
                                ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
