using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAACCESS.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
    }
}
