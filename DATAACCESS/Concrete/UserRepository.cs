using Dapper;
using DATAACCESS.Abstract;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAACCESS.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly string _tableName = "Users";
        private SqlConnection SqlConnection()
        {
            return new SqlConnection("Server=.;Database=DapperIdentityDb;uid=yusuf;pwd=123");
        }
        private IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }
        public void DeleteUser(Guid id)
        {
            using (IDbConnection conn = CreateConnection())
            {
                conn.Execute($"delete from {_tableName} where Id=@Id", new { Id = id });
                conn.Close();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (IDbConnection conn = CreateConnection())
            {
                IEnumerable<User> users = conn.Query<User>($"select * from {_tableName}");
                conn.Close();
                return users;
            }
        }

        public User GetById(Guid id)
        {
            using (IDbConnection conn = CreateConnection())
            {
                User user = conn.QueryFirstOrDefault<User>($"select * from {_tableName} where Id=@Id", new { Id = id });
                conn.Close();
                return user;
            }
        }

        public void InsertUser(User user)
        {
            using (IDbConnection conn = CreateConnection())
            {
                conn.Execute($"insert into {_tableName}(Id,Name,Surname,Username,Age,Password) values (@Id,@Name,@Surname,@Username,@Age,@Password)", new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.Username,
                    Age = user.Age,
                    Password = user.Password,
                });
                conn.Close();
            }
        }

        public void UpdateUser(User user)
        {
            using (IDbConnection conn = CreateConnection())
            {
                if (user != null)
                {
                    conn.Execute($"update {_tableName} set Id=@Id,Name=@Name,Surname=@Surname,Username=@Username,Age=@Age,Password=@Password where Id=@Id", new User
                    {
                        Id = user.Id.ToString(),
                        Name = user.Name,
                        Surname = user.Surname,
                        Username = user.Username,
                        Age = user.Age,
                        Password = user.Password,
                    });
                    conn.Close();
                }
                else
                {
                    throw new Exception("Üye bulunamamaktadır");
                }
            }

        }
    }
}
