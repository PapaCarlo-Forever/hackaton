using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SReasonController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public SReasonController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var reasons = await _context.SReason.ToListAsync();
        return Ok(reasons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var reason = await _context.SReason.FindAsync(id);
        if (reason == null)
            return NotFound();
        return Ok(reason);
    }
} 