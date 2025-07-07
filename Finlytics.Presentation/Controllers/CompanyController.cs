using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finlytics.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    private readonly ICompanyService _companyService = companyService;

    [HttpGet]
    public async Task<ActionResult<List<CompanyDto>>> GetAll() => Ok(await _companyService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Add([FromBody] CompanyDto dto)
    {
        var result = await _companyService.AddAsync(dto);
        return Ok(result);
    }
}
