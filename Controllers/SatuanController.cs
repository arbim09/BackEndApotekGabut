using Apotek.DTO;
using Apotek.Services.Satuan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apotek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SatuanController : ControllerBase
    {
        private readonly ISatuanService _service;

        public SatuanController(ISatuanService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] SatuanDto request)
        {
            var response = await _service.Create(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] SatuanDto request)
        {
            var response = await _service.Update(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete([FromBody] SatuanDto request)
        {
            var response = await _service.Delete(request.Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("getById")]
        [Authorize]
        public async Task<IActionResult> GetById([FromBody] SatuanDto request)
        {
            var response = await _service.GetById(request.Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("all")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromBody] SearchDto request)
        {
            var response = await _service.GetAll(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
