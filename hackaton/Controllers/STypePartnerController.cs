//Выполнил Арнаутов 22.07.2025
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class STypePartnerController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public STypePartnerController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var types = await _context.STypePartner.ToListAsync();
        return Ok(types);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var type = await _context.STypePartner.FindAsync(id);
        if (type == null)
            return NotFound();
        return Ok(type);
    }
} 