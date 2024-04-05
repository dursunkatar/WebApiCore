using Application.Commands;
using Application.DTOs;
using Application.Query;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand createCategoryDto)
        {
            var response = await _mediator.Send(createCategoryDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand { Id = id });

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var query = new GetCategoriesQuery();
            var response = await _mediator.Send(query);

            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
