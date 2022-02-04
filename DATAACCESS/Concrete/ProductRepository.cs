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
    public class ProductRepository : IProductRepository
    {
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
        public void DeleteProduct(Guid id)
        {
            using(IDbConnection conn = CreateConnection())
            {
                conn.Execute("DeleteProduct", new { Id = id }, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using(IDbConnection conn = CreateConnection())
            {
                IEnumerable<Product> products = conn.Query<Product>("SelectProducts", commandType: CommandType.StoredProcedure);
                conn.Close();
                return products;
            }
            
        }

        public Product GetById(Guid id)
        {
            using(IDbConnection conn = CreateConnection())
            {
                Product product = conn.QueryFirstOrDefault<Product>("GetById", new { Id = id }, commandType: CommandType.StoredProcedure);
                conn.Close();
                return product;
            }
        }

        public void InsertProduct(Product product)
        {
            using(IDbConnection conn = CreateConnection())
            {
                conn.Execute("InsertUser",new Product
                {
                    Id = Guid.NewGuid().ToString(),


                })
            }
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
