using AutoMapper;
using DemoExam.Blazor.Shared;
using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Models;
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

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        if (orderId <= 0)
            return BadRequest();

        try
        {
            var order = await _ordersService.GetOrder(orderId);
            var userIdFromToken = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "-1");
            if (order.UserId != userIdFromToken)
            {
                if (User.IsInRole(Role.AdminRoleName) is false && User.IsInRole(Role.ManagerRoleName) is false)
                    return Forbid();
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> CreateNewOrder([FromBody] NewOrderDto newOrderDto)
    {
        try
        {
            var userIdStr = User.FindFirst(x => x.Type == "UserId")?.Value;
            if (int.TryParse(userIdStr, out var userId) is false)
                return Unauthorized();
            await _ordersService.CreateNewOrder(userId, newOrderDto.PickupPointId, newOrderDto.Products);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}