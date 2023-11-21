using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Blazor.Shared.Dto.Responses;
using DemoExam.Domain.Exceptions;
using DemoExam.Domain.Models;
using DemoExam.Domain.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order = DemoExam.Blazor.Shared.Dto.Responses.Order;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrdersService ordersService, IMapper mapper, ILogger<OrdersController> logger)
    {
        _ordersService = ordersService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("User/{userId:int}")]
    public async Task<IActionResult> GetOrdersForUser([Range(1, int.MaxValue)] int userId)
    {
        try
        {
            var userIdFromToken = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "-1");
            if (userId != userIdFromToken)
                return Forbid();

            var orders = await _ordersService.GetOrdersForUser(userId).ConfigureAwait(false);
            var ordersDto = _mapper.Map<List<OrderShort>>(orders);
            return Ok(ordersDto);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get user orders");
            return BadRequest();
        }
    }

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetOrder([Range(1, int.MaxValue)] int orderId)
    {
        try
        {
            var order = await _ordersService.GetOrder(orderId);
            var userIdFromToken = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "-1");
            if (order.UserId != userIdFromToken)
            {
                if (User.IsInRole(Role.AdminRoleName) is false && User.IsInRole(Role.ManagerRoleName) is false)
                    return Forbid();
            }

            var orderDto = _mapper.Map<Order>(order);
            return Ok(orderDto);
        }
        catch (EntityNotFoundException)
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
    public async Task<IActionResult> CreateNewOrder([FromBody] NewOrder newOrder)
    {
        try
        {
            var userIdStr = User.FindFirst(x => x.Type == "UserId")?.Value;
            if (int.TryParse(userIdStr, out var userId) is false)
                return Unauthorized();
            await _ordersService.CreateNewOrder(userId, newOrder.PickupPointId, newOrder.Products);
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("")]
    [Authorize(Policy = "AdministratorAndManager")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _ordersService.GetAllOrders();
            var ordersDto = _mapper.Map<List<OrderShort>>(orders);
            return Ok(ordersDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch all orders");
            return BadRequest(e);
        }
    }

    [HttpDelete("{orderId:int}")]
    [Authorize(Policy = "AdministratorAndManager")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        try
        {
            await _ordersService.CancelOrder(orderId);
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to cancel order");
            return BadRequest(e);
        }
    }

    [HttpPut("{orderId:int}")]
    [Authorize(Policy = "AdministratorAndManager")]
    public async Task<IActionResult> EditOrder(int orderId, [FromBody] OrderEdit orderEdit)
    {
        try
        {
            await _ordersService.EditOrder(orderId, orderEdit.PickupPointId, orderEdit.ItemsToDelete);
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to edit order");
            return BadRequest(e);
        }
    }
}