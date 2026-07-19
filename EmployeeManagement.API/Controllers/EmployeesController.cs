using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> GetAll(CancellationToken ct) => Ok(await service.GetAllAsync(ct));
    [HttpGet("{id:int}")] public async Task<ActionResult<EmployeeDto>> GetById(int id, CancellationToken ct) => await service.GetByIdAsync(id, ct) is { } employee ? Ok(employee) : NotFound();
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeRequest request, IValidator<CreateEmployeeRequest> validator, CancellationToken ct)
    { var result = await validator.ValidateAsync(request, ct); if (!result.IsValid) return ValidationProblem(result.ToDictionary()); var employee = await service.CreateAsync(request, ct); return CreatedAtAction(nameof(GetById), new { employee.Id }, employee); }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeRequest request, IValidator<UpdateEmployeeRequest> validator, CancellationToken ct)
    { var result = await validator.ValidateAsync(request, ct); if (!result.IsValid) return ValidationProblem(result.ToDictionary()); return await service.UpdateAsync(id, request, ct) ? NoContent() : NotFound(); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) => await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
