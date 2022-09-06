using API.DTOs.Enum;
using API.Helpers.Enum;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumValueController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<EnumValueDto>> GetCodeOfTheCountry()
        {
            try
            {
                return Ok(EnumExtensions.GetValues<CodeOfTheCountry>());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
