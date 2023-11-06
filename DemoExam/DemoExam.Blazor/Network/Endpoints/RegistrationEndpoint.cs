using System.Net;
using Arbus.Network;
using DemoExam.Blazor.Exceptions;
using DemoExam.Blazor.Shared.Dto.Requests;

namespace DemoExam.Blazor.Network.Endpoints;

public class RegistrationEndpoint : ApiEndpoint
{
    private readonly User _userRequest;
    
    public RegistrationEndpoint(User userRequest) : base(HttpMethod.Post, "Auth/registration")
    {
        _userRequest = userRequest;
    }

    public override HttpContent CreateContent() => ToJson(_userRequest);

    public override Task HandleNotSuccessStatusCode(HttpResponseMessage responseMessage)
    {
        if (responseMessage.StatusCode == HttpStatusCode.Conflict)
            throw new DuplicateLoginException();
        return base.HandleNotSuccessStatusCode(responseMessage);
    }
}