using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //https://localhost:7004/api/Regions
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _repository.GetAllAsync();
            return Ok(_mapper.Map<List<RegionDto>>(regions));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _repository.GetByIdAsync(id);
            if (region == null) return NotFound();
            return Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionDto request)
        {
            var region = _mapper.Map<Region>(request);
            region = await _repository.CreateAsync(region);
            return CreatedAtAction(nameof(GetById),
                new { id = region.Id },
                _mapper.Map<RegionDto>(region));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            var region = _mapper.Map<Region>(request);
            region = await _repository.UpdateAsync(id, region);
            if (region == null) return NotFound();
            return Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _repository.DeleteAsync(id);
            if (region == null) return NotFound();
            return Ok(_mapper.Map<RegionDto>(region));
        }
    }
}
