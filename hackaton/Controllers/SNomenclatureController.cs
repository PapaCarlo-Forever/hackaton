//Выполнил Арнаутов 22.07.2025
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SNomenclatureController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public SNomenclatureController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var nomenclatures = await _context.SNomenclature.ToListAsync();
        return Ok(nomenclatures);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var nomenclature = await _context.SNomenclature.FindAsync(id);
        if (nomenclature == null)
            return NotFound();
        return Ok(nomenclature);
    }
} 