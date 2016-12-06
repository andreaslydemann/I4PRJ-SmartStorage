﻿using System.Web.Http;
using SmartStorage.BLL.Interfaces.Services;
using SmartStorage.BLL.Services;
using SmartStorage.DAL.Context;
using SmartStorage.DAL.UnitOfWork;

namespace SmartStorage.UI.Controllers.Api
{
  public class ProductsController : ApiController
  {
    private readonly IProductService _service;

    public ProductsController()
      : this(new ProductService(new UnitOfWork(new ApplicationDbContext())))
    {
    }
    public ProductsController(IProductService service)
    {
      _service = service ?? new ProductService(new UnitOfWork(new ApplicationDbContext()));
    }

    [ActionName("DefaultAction")]
    public IHttpActionResult GetProducts()
    {
      var entityDto = _service.GetAllActive();

      if (entityDto == null) return NotFound();

      return Ok(entityDto);
    }

    public IHttpActionResult GetProductsOfCategory(int id)
    {
      var entityDto = _service.GetAllActiveOfCategory(id);

      if (entityDto == null) return NotFound();

      return Ok(entityDto);
    }

    [HttpDelete]
    public IHttpActionResult DeleteProduct(int id)
    {
      // TODO null kontrol

      _service.Delete(id);

      return Ok();
    }
  }
}