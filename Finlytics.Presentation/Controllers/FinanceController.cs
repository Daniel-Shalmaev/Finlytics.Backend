using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finlytics.Presentation.Controllers;

[Route("api/finance")]
[ApiController]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService _financeService;

    public FinanceController(IFinanceService financeService) =>
        _financeService = financeService;

    // Endpoint to retrieve financial data based on a date range
    [HttpGet("get-data")]
    public async Task<IActionResult> GetFinanceData([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        if (from > to)
            return BadRequest(new { error = "Invalid date range: 'from' cannot be later than 'to'." });

        if (to > DateTime.UtcNow)
            return BadRequest(new { error = "Invalid date range: 'to' cannot be in the future." });

        return Ok(await _financeService.GetFinanceData(from, to));
    }

    [HttpPost]
    public async Task<ActionResult<DailyFinanceDto>> Add([FromBody] AddDailyFinanceDto dto)
    {
        var result = await _financeService.AddAsync(dto);
        return Ok(result);
    }
}