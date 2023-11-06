using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class CatalogEndpoint : ApiEndpoint<List<Product>>
{
    public CatalogEndpoint() : base(HttpMethod.Get, "catalog")
    {
    }
}