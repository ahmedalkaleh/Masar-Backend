using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate
{
    public class CreateRouteTemplateCommandValidator : AbstractValidator<CreateRouteTemplateCommand>
    {
        public CreateRouteTemplateCommandValidator()
        {
            RuleFor(x => x.TemplateName).NotEmpty().WithMessage("TemplateName is required.");
            RuleFor(x => x.StartStationId).NotEmpty().WithMessage("StartStationId is required.");
            RuleFor(x => x.EndStationId).NotEmpty().WithMessage("EndStationId is required.");
            RuleFor(x => x.RouteTemplateStops).NotEmpty().WithMessage("RouteTemplateStops are required.");
            RuleForEach(x => x.RouteTemplateStops).SetValidator(new CreateRouteTemplateStopCommandValidator());

        }
    }
}
