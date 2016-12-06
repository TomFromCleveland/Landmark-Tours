using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DALs
{
    public interface IUserDAL
    {
        bool CreateNewUser(string username, string password);
    }
}
