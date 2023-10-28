using AutoMapper;
using DemoExam.Blazor.Shared;
using DemoExam.Domain.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;
    private readonly IMapper _mapper;

    public OrdersController(IOrdersService ordersService, IMapper mapper)
    {
        _ordersService = ordersService;
        _mapper = mapper;
    }

    [HttpGet("User/{userId:int}")]
    public async Task<IActionResult> GetOrdersForUser(int userId)
    {
        if (userId <= 0)
            return BadRequest();
        
        var userIdFromToken = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "-1");
        if (userId != userIdFromToken)
            return Forbid();

        var orders = await _ordersService.GetOrdersForUser(userId).ConfigureAwait(false);
        var ordersDto = _mapper.Map<List<OrderShortDto>>(orders);
        return Ok(ordersDto);
    }
}