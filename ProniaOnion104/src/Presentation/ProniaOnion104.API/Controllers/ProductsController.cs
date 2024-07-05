using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion104.Application.Abstractions.Services;
using ProniaOnion104.Application.DTOs.Products;
using System.Security.Claims;

namespace ProniaOnion104.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ITokenHandler _handler;

        public ProductsController(IProductService service, ITokenHandler handler)
        {
            _service = service;
            _handler = handler;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> Test()
        {

            return Ok(_handler.CreateRefreshToken());
            //return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllPaginated(page, take));
        }
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Get(int id)
        {
            
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ProductCreateDto dto)
        {
            await _service.CreateAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,ProductUpdateDto dto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
