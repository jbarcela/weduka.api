using MediatR;

namespace Weduka.Application.Queries.Person.GetById;

public class GetByIdPersonQueryRequest : IRequest<GetByIdPersonQueryResponse>
{
    public int Id { get; set; }

    public GetByIdPersonQueryRequest(int id)
    {
        Id = id;
    }
}