using UserBff.Dtos;
using UserBff.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace UserBff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogTypeController : ControllerBase
    {
        private readonly ICatalogTypeService typeService;
        private readonly IMapper mapper;

        public CatalogTypeController(ICatalogTypeService typeService, IMapper mapper)
        {
            this.typeService = typeService;
            this.mapper = mapper;
        }

        [HttpPost("Add", Name = "AddType")]
        public async Task<IActionResult> AddType(CatalogTypeCreate type)
        {

            await typeService.AddCatalogTypeAsync(type);
            return Ok();
        }

        [HttpPut("Update", Name = "UpdateType")]
        public async Task<ActionResult<bool>> UpdateType(CatalogTypeUpdate type)
        {
            var status = await typeService.UpdateCatalogTypeAsync(type);
            return Ok(status);
        }

        [HttpDelete("Delete/{typeId}", Name = "DeleteType")]
        public async Task<ActionResult<bool>> DeleteType(int typeId)
        {
            var status = await typeService.DeleteCatalogTypeAsync(typeId);
            return Ok(status);
        }

        [HttpGet("GetAll", Name = "GetAllTypes")]
        public async Task<ActionResult<IEnumerable<CatalogTypeRead>>> GetAllTypes()
        {
            var types = await typeService.GetCatalogTypesAsync();

            return Ok(types);
        }

        [HttpGet("ById/{typeId}", Name = "GetTypeById")]
        public async Task<ActionResult<CatalogTypeRead>> GetTypeById(int typeId)
        {
            var type = await typeService.GetCatalogTypeByIdAsync(typeId);
            return Ok(type);
        }
    }
}
