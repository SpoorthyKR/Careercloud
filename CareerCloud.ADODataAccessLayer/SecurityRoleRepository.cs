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
     public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string _connStr;

        public SecurityRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                            ([Id],
                             [Role],
                             [Is_Inactive])
                            VALUES 
                            (@Id,
                             @Role, 
                             @Is_Inactive)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);


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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT [Id],[Role],[Is_Inactive]
                FROM [JOB_PORTAL_DB].[dbo].[Security_Roles]";
                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SecurityRolePoco[] appPocos = new SecurityRolePoco[2000];
                while (reader.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Role = reader.GetString(1);
                    poco.IsInactive = reader.GetBoolean(2);


                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand() { Connection = conn };
                foreach (SecurityRolePoco poco in items)
                {


                    cmd.CommandText = @"DELETE FROM  [dbo].[Security_Roles] WHERE  ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
                
            foreach (SecurityRolePoco poco in items)
            {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                            SET [Id] = @Id
                                ,[Role] = @Role
                                ,[Is_Inactive] = @Is_Inactive                              
                            WHERE [Id] = @Id
                                ";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Role", poco.Role);
                cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}

