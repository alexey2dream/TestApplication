using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.Queries.GetTotalAmountMessagesByChannel
{
    public class GetTotalAmountMessagesByChannelValidator : AbstractValidator<GetTotalAmountMessagesByChannel>
    {
        public GetTotalAmountMessagesByChannelValidator()
        {
            RuleFor(c => c.ChannelId)
                .GreaterThan(0).WithMessage("ChannelId is less than 1!");
        }
    }
}
