using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SPartnerController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public SPartnerController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var partners = await _context.SPartner.ToListAsync();
        return Ok(partners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var partner = await _context.SPartner.FindAsync(id);
        if (partner == null)
            return NotFound();
        return Ok(partner);
    }
} 