using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion104.Application.Abstractions.Services;
using ProniaOnion104.Application.DTOs.Categories;

namespace ProniaOnion104.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

        //    return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto categoryDto)
        {
            await _service.CreateAsync(categoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
