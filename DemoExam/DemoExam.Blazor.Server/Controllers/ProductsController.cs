using AutoMapper;
using DemoExam.Blazor.Shared.Dto.Requests;
using DemoExam.Domain.Models;
using DemoExam.Domain.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product = DemoExam.Blazor.Shared.Dto.Responses.Product;

namespace DemoExam.Blazor.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Role.AdminRoleName)]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly IMapper _mapper;

    public ProductsController(IProductsService productsService, IMapper mapper)
    {
        _productsService = productsService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productsService.GetAll();
        var productsDto = _mapper.Map<List<Product>>(products);
        return Ok(productsDto);
    }

    [HttpPost("edit/{productArticle}")]
    public async Task<IActionResult> EditProduct(string productArticle, [FromBody] ProductEdit productEdit)
    {
        await _productsService.UpdateProduct(productArticle, x =>
        {
            x.Name = productEdit.Name ?? x.Name;
            x.Description = productEdit.Description ?? x.Description;
            x.Category = productEdit.Category ?? x.Category;
            x.Discount = productEdit.Discount ?? x.Discount;
            x.Photo = productEdit.Photo is null ? x.Photo : Convert.FromBase64String(productEdit.Photo);
            x.QuantityInStock = productEdit.QuantityInStock ?? x.QuantityInStock;
            x.Price = productEdit.Price ?? x.Price;
            x.ManufacturerName = productEdit.ManufacturerName ?? x.ManufacturerName;
            x.SupplierName = productEdit.SupplierName ?? x.SupplierName;
        });
        return Ok();
    }

    [HttpDelete("{productArticle}")]
    public async Task<IActionResult> DeleteProduct(string productArticle)
    {
        await _productsService.DeleteProduct(productArticle);
        return Ok();
    }

    [HttpPost("/new/")]
    public async Task<IActionResult> CreateNew([FromBody] NewProduct newProduct)
    {
        var product = _mapper.Map<DemoExam.Domain.Models.Product>(newProduct);
        await _productsService.AddProduct(product);
        return Ok();
    }
}