using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAACCESS.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);

    }
}
