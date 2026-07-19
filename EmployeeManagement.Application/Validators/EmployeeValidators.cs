using EmployeeManagement.Application.DTOs;
using FluentValidation;

namespace EmployeeManagement.Application.Validators;

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator() { RuleFor(x => x.Name).NotEmpty().MaximumLength(100); RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256); RuleFor(x => x.Department).NotEmpty().MaximumLength(100); }
}
public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator() { RuleFor(x => x.Name).NotEmpty().MaximumLength(100); RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256); RuleFor(x => x.Department).NotEmpty().MaximumLength(100); }
}
