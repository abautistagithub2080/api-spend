using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MVC.SPEND.API.Repository;
namespace MVC.SPEND.API.Controllers
{
    public class ROTController : ApiController
    {
        private IRotRepository _repository;
        public ROTController()
        {
            _repository = new RotRepository();
        }
        [HttpGet]
        public async Task<IHttpActionResult> FillMenu(string IDUser)
        {
            try
            {
                var oMenu = await _repository.FillMenus(IDUser);
                string WNomCom = await _repository.FillUserID(IDUser);
                return Content(HttpStatusCode.OK, new { mensaje = "OK_FW", Menus = oMenu, NombreUsuario = WNomCom });
            }
            catch (Exception error) {
                return Content(HttpStatusCode.InternalServerError, new { mensaje = "OK_FW", response = "Error" + error.Message });
            }
        }
    }
}