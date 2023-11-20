using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Requests;

namespace DemoExam.Blazor.Network.Endpoints;

public class NewProductEndpoint : ApiEndpoint
{
    private readonly NewProduct _newProductDto;
    
    public NewProductEndpoint(NewProduct newProductDto) : base(HttpMethod.Post, "/Products")
    {
        _newProductDto = newProductDto;
    }

    public override HttpContent? CreateContent() => ToJson(_newProductDto);
}