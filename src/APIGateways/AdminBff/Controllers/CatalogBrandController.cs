using AdminBff.Dtos;
using AdminBff.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminBff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogBrandController : ControllerBase
    {
        private readonly ICatalogBrandService brandService;

        public CatalogBrandController(ICatalogBrandService brandService)
        {
            this.brandService = brandService;
        }
        [HttpPost("Add", Name = "AddBrand")]
        public async Task<ActionResult<bool>> AddBrand(CatalogBrandCreate brand)
        {
            var status = await brandService.AddCatalogBrandAsync(brand);
            return Ok(status);
        }

        [HttpPut("Update", Name = "UpdateBrand")]
        public async Task<ActionResult<bool>> UpdateBrand(CatalogBrandUpdate brand)
        {
            var status = await brandService.UpdateCatalogBrandAsync(brand);
            return Ok(status);
        }

        [HttpDelete("Delete/{brandId}", Name = "DeleteBrand")]
        public async Task<ActionResult<bool>> DeleteBrand(int brandId)
        {
            var status = await brandService.DeleteCatalogBrandAsync(brandId);
            return Ok(status);
        }

        [HttpGet("GetAll", Name = "GetAllBrands")]
        public async Task<ActionResult<IEnumerable<CatalogBrandRead>>> GetAllBrands()
        {
            var brands = await brandService.GetCatalogBrandsAsync();

            return Ok(brands);
        }

        [HttpGet("ById/{brandId}", Name = "GetBrandById")]
        public async Task<ActionResult<CatalogBrandRead>> GetBrandById(int brandId)
        {
            var brand = await brandService.GetCatalogBrandByIdAsync(brandId);
            return Ok(brand);
        }
    }
}
