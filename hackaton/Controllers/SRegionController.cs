//Выполнил Арнаутов 22.07.2025
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SRegionController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public SRegionController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var regions = await _context.SRegion.ToListAsync();
        return Ok(regions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var region = await _context.SRegion.FindAsync(id);
        if (region == null)
            return NotFound();
        return Ok(region);
    }
} 