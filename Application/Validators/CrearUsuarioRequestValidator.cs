using Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Domain.Entidades;

namespace Application.Validators
{
    public class CrearUsuarioRequestValidator : AbstractValidator<CrearUsuarioRequest>
    {
        public CrearUsuarioRequestValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El primer nombre es obligatorio.");

            RuleFor(x => x.TipoDocumento)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El número de documento es obligatorio.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("El correo electrónico no tiene un formato válido.");

        }
    }
}
