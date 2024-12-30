using MVC.SPEND.API.Data;
using MVC.SPEND.API.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC.SPEND.API.Repository
{
    public class ValidateRepository: IValidateRepository
    {
        public async Task<int> FNEsValidateUser(string User, string Pass)
        {
            int IsUser = 0;
            CodDec CdHex = new CodDec();
            DataLayer DL = new DataLayer();
            string WUser = await CdHex.FNCdHx(User); string WPAss = await CdHex.FNCdHx(Pass);
            var oUser = await DL.FNReader("spGetUsuario", 0, "@Usuario|@Contra", WUser, WPAss);
            if (oUser.Read()) IsUser = int.Parse(oUser["IDUsr"].ToString());
            oUser.Close(); oUser.Dispose(); oUser = null;
            DL.Dispose(); DL = null;
            return IsUser;
        }
    }
}