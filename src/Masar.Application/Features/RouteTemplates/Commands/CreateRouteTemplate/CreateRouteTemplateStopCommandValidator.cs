using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate
{
    public sealed class CreateRouteTemplateStopCommandValidator : AbstractValidator<CreateRouteTemplateStopCommand>
    {
        public CreateRouteTemplateStopCommandValidator()
        {
            RuleFor(x => x.StationId).NotEmpty().WithMessage("StationId is required.");
            RuleFor(x => x.StopOrder).GreaterThan(0).WithMessage("StopOrder must be a positive integer.");
        }
    }
}
