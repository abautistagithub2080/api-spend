using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.SPEND.API.Repository
{
    public interface IValidateRepository
    {
        Task<int> FNEsValidateUser(string User, string Pass);
    }
}
