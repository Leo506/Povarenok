using AutoMapper;
using DemoExam.Blazor.Shared.Dto.Responses;
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
            List<Domain.Models.PickupPoint> points = await _pickupPointService.GetAll();
            var pointsDto = _mapper.Map<List<PickupPoint>>(points);
            return Ok(pointsDto);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}