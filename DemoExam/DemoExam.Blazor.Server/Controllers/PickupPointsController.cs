using AutoMapper;
using DemoExam.Blazor.Shared;
using DemoExam.Domain.Models;
using DemoExam.Domain.Services.PickupPoints;
using Microsoft.AspNetCore.Mvc;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PickupPointsController : ControllerBase
{
    private readonly IPickupPointService _pickupPointService;
    private readonly IMapper _mapper;

    public PickupPointsController(IPickupPointService pickupPointService, IMapper mapper)
    {
        _pickupPointService = pickupPointService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetPoints()
    {
        try
        {
            List<PickupPoint> points = await _pickupPointService.GetAll();
            var pointsDto = _mapper.Map<List<PickupPointDto>>(points);
            return Ok(pointsDto);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}