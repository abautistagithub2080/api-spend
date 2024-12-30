using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC.SPEND.API.Tools
{
    public class CodDec
    {
        private async Task<int> Asc(string WLet)
        {
            if (WLet.Length == 0) return 0;
            int nAsc = System.Convert.ToChar(WLet);
            return nAsc;
        }
        private async Task<String> Hex(string WHnd)
        {
            int nWord = Convert.ToInt32(WHnd); string WHex = String.Format("{0:X}", nWord);
            return WHex;
        }
        public async Task<string> FNCdHx(string WHnd)
        {
            String Wrd = ""; String WCar = ""; int nKey = 31; char CPad = '0';
            for (int J = 0; J <= WHnd.Length - 1; J++)
            {
                WCar = WHnd.Substring(J, 1); int nAsc = await Asc(WCar) * nKey; string WHex = await Hex(nAsc.ToString());
                Wrd = Wrd + WHex.PadLeft(4, CPad);
            }
            return Wrd;
        }
        public async Task<string> FNDcHx(string WHnd)
        {
            String Wrd = ""; String WCar = ""; int nKey = 31; int J = 0;
            while (J < WHnd.Length)
            {
                WCar = WHnd.Substring(J, 4); string XCar = WCar; int nHNum = Int32.Parse(XCar, System.Globalization.NumberStyles.HexNumber); int nChr = nHNum / nKey; Wrd = Wrd + Convert.ToChar(nChr);
                J = J + 4;
            }
            return Wrd;
        }
    }
}