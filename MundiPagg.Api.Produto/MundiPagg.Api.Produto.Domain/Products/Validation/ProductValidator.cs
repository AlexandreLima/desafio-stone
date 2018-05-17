using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Domain.Products.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nome não pode estar em branco");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Código do produto não pode estar em branco");
            RuleFor(p => p.Departament).NotNull().WithMessage("Departamento não pode ser nulo");
            RuleFor(p => p.Departament.Name).NotEmpty().WithMessage("O nome do departamento não pode estar em branco");
        }
    }
}
