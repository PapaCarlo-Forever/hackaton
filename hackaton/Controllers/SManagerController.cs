//Выполнил Арнаутов 22.07.2025
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SManagerController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public SManagerController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var managers = await _context.SManager.ToListAsync();
        return Ok(managers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var manager = await _context.SManager.FindAsync(id);
        if (manager == null)
            return NotFound();
        return Ok(manager);
    }
} 