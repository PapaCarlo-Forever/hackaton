using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

[ApiController]
[Route("api/[controller]")]
public class GeneralController : ControllerBase
{
    private readonly GeneralDbContext _context;
    public GeneralController(GeneralDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _context.General
            .Include(g => g.SPartner)
            .Include(g => g.SManager)
            .Include(g => g.STypePartner)
            .Include(g => g.SNomenclature)
            .Include(g => g.SRegion)
            .Include(g => g.SReason)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _context.General
            .Include(g => g.SPartner)
            .Include(g => g.SManager)
            .Include(g => g.STypePartner)
            .Include(g => g.SNomenclature)
            .Include(g => g.SRegion)
            .Include(g => g.SReason)
            .FirstOrDefaultAsync(g => g.id == id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

 
    [HttpGet("analyze-advanced")]
    public async Task<IActionResult> AnalyzeAdvanced()
    {
        var random = new Random();
        var randomArr1 = Enumerable.Range(-10, 21).OrderBy(_ => random.Next()).ToArray();
        var randomArr2 = Enumerable.Range(-20, 41).OrderBy(_ => random.Next()).ToArray();

        var items = await _context.General
            .Include(g => g.SPartner)
            .Include(g => g.SManager)
            .Include(g => g.SNomenclature)
            .Include(g => g.SReason)
            .Include(g => g.SRegion)
            .OrderBy(g => g.id)
            .Take(1800)
            .ToListAsync();

        var result = new List<GeneralDto>();

        for (int block = 0; block < 100; block++)
        {
            var blockItems = items.Skip(block * 18).Take(18).ToList();
            if (blockItems.Count < 18) break;

            int rating = 0;
            var last3Sum = blockItems.Skip(15).Take(3).Sum(x => x.weight);
            if (last3Sum == 0)
                rating += 30;

            var sum15_14_12 = blockItems[15].weight + blockItems[14].weight + blockItems[12].weight;
            int usedRandom1 = randomArr1[block % randomArr1.Length];
            if (sum15_14_12 != 0)
            {
                var percent = (last3Sum / sum15_14_12) * 100;
                if (percent < usedRandom1)
                    rating += 30;
            }

            var first3Sum = blockItems.Take(3).Sum(x => x.weight);
            var sumAll = (last3Sum + first3Sum) * 100;
            int usedRandom2 = randomArr2[block % randomArr2.Length];
            if (sumAll < usedRandom2)
                rating += 30;

            var lastRegion = blockItems
                .Where(x => x.SReason != null && x.SReason.reason != "0")
                .GroupBy(x => x.SRegion?.region)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var reasons = blockItems
                .GroupBy(x => x.SReason?.reason)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Where(r => r != null)
                .ToArray();

            result.Add(new GeneralDto
            {
                partnerName = blockItems[0].SPartner?.name,
                managerFio = blockItems[0].SManager?.FIO,
                nomenclatureName = blockItems[0].SNomenclature?.name,
                lastRegion = lastRegion,
                reasons = reasons,
                random1 = usedRandom1,
                random2 = usedRandom2,
                rating = rating,
                totalWeight = blockItems.Sum(x => x.weight)
            });
        }

        var sortedResult = result.OrderByDescending(x => x.rating).ToList();

        // Email рассылка если сегодня 1-е число месяца и есть клиенты с рейтингом 60 или 90
        var now = DateTime.Now;
        if (now.Day == 1)
        {
            var highRiskClients = sortedResult
                .Where(x => x.rating == 60 || x.rating == 90)
                .Select(x =>
                {
                    string reason = x.reasons?.FirstOrDefault(r => r != "Запрос передан в другие управления" && r != "0")
                                    ?? x.reasons?.FirstOrDefault() ?? "Неизвестно";
                    return $"Клиент {x.partnerName} имеет высокий шанс отказа от услуг. Вероятная причина {reason}. Необходимо принять меры";
                })
                .ToList();

            if (highRiskClients.Any())
            {
                var smtpClient = new SmtpClient("smtp.yourserver.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your_email@domain.com", "your_password"),
                    EnableSsl = true,
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your_email@domain.com"),
                    Subject = "Аналитика клиентов",
                    Body = string.Join("\n", highRiskClients),
                    IsBodyHtml = false,
                };
                mailMessage.To.Add("apopugayev4@mail.com"); // директор отдела продаж
                mailMessage.To.Add("address2@example.com"); // ответственный менеджер
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        return Ok(sortedResult);
    }
} 