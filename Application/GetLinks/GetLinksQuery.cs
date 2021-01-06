using MediatR;
using MyHN.Application;

namespace MyHn.Application
{
    public class GetLinksQuery : IRequest<LinkDto[]>
    {

    }
}
