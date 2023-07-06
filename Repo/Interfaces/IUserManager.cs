using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface IUserManager
    {
        public Task<int> AddIdentityRoles();
    }
}
