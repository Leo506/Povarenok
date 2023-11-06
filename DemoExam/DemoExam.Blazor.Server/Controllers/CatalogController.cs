using AutoMapper;
using DemoExam.Blazor.Shared.Dto.Responses;
using DemoExam.Domain.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly IMapper _mapper;
    
    public CatalogController(IProductsService productsService, IMapper mapper)
    {
        _productsService = productsService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCatalog()
    {
        await Task.Delay(2_000).ConfigureAwait(false);

        var products = await _productsService.GetAll().ConfigureAwait(false);
        var productDtos = products.Select(x => _mapper.Map<Product>(x));
        return Ok(productDtos);
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> FindProducts([FromQuery, ] string searchString = "", [FromQuery] string category = "all")
    {
        var products = await _productsService.FindProduct(searchString, category);
        var productDtos = products.Select(x => _mapper.Map<Product>(x));
        return Ok(productDtos);
    }
    
    [HttpGet("{article}")]
    public async Task<IActionResult> FindProducts(string article)
    {
        var product = await _productsService.GetByArticleNumber(article);
        return Ok(_mapper.Map<Product>(product));
    }
}