using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MVC.SPEND.API.Models;
using MVC.SPEND.API.Repository;

namespace MVC.SPEND.API.Controllers
{
    public class CatGastosController : ApiController
    {
        private IGastoRepository _repository;
        public CatGastosController()
        {
            _repository = new GastoRepository();
        }         
        [HttpGet]
        [Route("api/CatGastos/GetCatSpend/{IDUser:int}/{IDGastos:int}")]
        public async Task<IHttpActionResult> GetCatSpend(int IDUser, int IDGastos)
        {
            try
            {
                string WIDGas = ""; string WGastos = "";
                var Spendlist = await _repository.FillCombosGastos(IDUser);
                if (IDGastos == 0)
                {
                    WIDGas = "0";
                }
                else
                {
                    var oCSpend = await _repository.GETGastoID(IDGastos, IDUser);
                    WIDGas = oCSpend[0].IDGastos.ToString(); WGastos = oCSpend[0].Gastos;
                    oCSpend.Clear(); oCSpend = null;
                }
                return Content(HttpStatusCode.OK, new { mensaje = "OK_FW", LCBXGastos = Spendlist, IDGastos = WIDGas, Gastos = WGastos });
            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.InternalServerError, new { mensaje = "OK_FW", response = "Error" + error.Message });
            }
        }
        [HttpPost]
        [Route("api/CatGastos/Guardar")]
        public async Task<IHttpActionResult> Guardar([FromBody] CatGastos oSpend)
        {
            try
            {
                var IsSave = await _repository.SaveCatGasto(oSpend);
                var Spendlist = await _repository.FillCombosGastos(oSpend.IDUsr);
                return Content(HttpStatusCode.OK, new { mensaje = "OK INSERT-UPDATE", LCBXGastos = Spendlist });
            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.InternalServerError, new { mensaje = "OK_FW", response = "Error" + error.Message });
            }
        }
        [HttpPost]
        [Route("api/CatGastos/Borrar")]
        public async Task<IHttpActionResult> Borrar([FromBody] CatGastos oSpend)
        {
            try
            {
                var IsSave = await _repository.DeleteCatGasto(oSpend);
                var Spendlist = await _repository.FillCombosGastos(oSpend.IDUsr);
                return Content(HttpStatusCode.OK, new { mensaje = "OK DELETE", LCBXGastos = Spendlist });
            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.InternalServerError, new { mensaje = "OK_FW", response = "Error" + error.Message });
            }
        }
    }
}