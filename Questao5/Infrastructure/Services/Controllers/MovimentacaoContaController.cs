using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Models;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoContaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Movimento>> GetMovimento(int idContaCorrente)
        {
            return Ok (new List<Movimento>()); 
        }
    }
}
