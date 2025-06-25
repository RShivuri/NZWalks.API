using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //https://localhost:7004/api/Regions
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext context;
        public RegionsController(NZWalksDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await context.Regions.ToListAsync();
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                var regionDto = new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                };
                regionsDto.Add(regionDto);
            }
            return Ok(regionsDto);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var regions = await context.Regions.FindAsync(Id);
            if (regions == null)
            {
                return NotFound();
            }
            return Ok(regions);
        }
        //POST To Create New Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionDto addRegionDto)
        {
            //map Or Convert DTo to Domain Model 
            var regions = new Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                RegionImageUrl = addRegionDto.RegionImageUrl
            };
            await context.Regions.AddAsync(regions);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = regions.Id }, regions);
        }
        //Update Region
        //api/Regions/{Id:Guid}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Get Region From Database
            var region = context.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            //Map DTo to Domain Model
            region.Code = updateRegionDto.Code;
            region.Name = updateRegionDto.Name;
            region.RegionImageUrl = updateRegionDto.RegionImageUrl;
            await context.SaveChangesAsync();
            return Ok(region);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Get Region From Database
            var region =  await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            //Delete Region
            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return Ok(region);

        }
    }
}
