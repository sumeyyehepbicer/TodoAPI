using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Implementation.Services.CategoryServices;
using Todo.Shared.DTOs.CategoryDtos;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Categories.Read")]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.GetAll(cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Categories.Read")]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.GetById(id, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin,User", Policy = "Permissions.Categories.Read")]
        [HttpGet("get-by-name")]
        public async Task<ActionResult<CategoryDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.GetByName(name, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Categories.Create")]
        [HttpPost("create-category")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.CreateCategory(request, cancellationToken));
        }

        [Authorize(Roles = "SuperAdmin,Admin", Policy = "Permissions.Categories.Update")]
        [HttpPost("update-category")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.UpdateCategory(request, cancellationToken));
        }
    }
}
