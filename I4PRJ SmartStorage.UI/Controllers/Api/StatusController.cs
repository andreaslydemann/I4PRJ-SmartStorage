﻿using SmartStorage.BLL.ViewModels;
using SmartStorage.DAL.Context;
using SmartStorage.DAL.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartStorage.UI.Controllers.Api
{
  public class StatusController : ApiController
  {
    private ApplicationDbContext db;

    public StatusController()
    {
      db = new ApplicationDbContext();
    }

    public IHttpActionResult GetStatusOfInventory(int id)
    {
      var inventory = db.Inventories.Find(id);

      if (inventory == null)
        return NotFound();

      var status = from i in db.Inventories.Where(i => i.InventoryId == id)//Primary where clause
                   join stk in db.Stocks on i.InventoryId equals stk.InventoryId
                   join p in db.Products on stk.ProductId equals p.ProductId
                   join cat in db.Categories on p.CategoryId equals cat.CategoryId
                   select new //Setup projection
                   {
                     ProductId = p.ProductId,
                     ProductName = p.Name,
                     Quantity = stk.Quantity,
                     CategoryName = cat.Name
                   }; //INNER JOIN

      return Ok(status);
    }

    public IHttpActionResult GetStatus(int id)
    {
      var status = db.Statuses.Find(id);

      if (status == null)
        return NotFound();

      var statuses = from sts in db.Statuses.Where(sts => sts.InventoryId == status.InventoryId
                     && EntityFunctions.DiffSeconds(sts.Updated, status.Updated) == 0) //Primary where clause
                     join cat in db.Categories on sts.Product.CategoryId equals cat.CategoryId
                     join p in db.Products on sts.ProductId equals p.ProductId
                     select new //Setup projection
                     {
                       ProductName = p.Name,
                       CategoryName = cat.Name,
                       ExpQuantity = sts.ExpQuantity,
                       CurQuantity = sts.CurQuantity,
                       Difference = sts.Difference
                     }; //INNER JOIN

      return Ok(statuses);
    }

    [HttpPost]
    public async Task<IHttpActionResult> CreateStatus(StatusEditModel statusDto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      var time = DateTime.Now;
      var user = User.Identity.Name;

      for (int i = 0; i < statusDto.CurQuantities.Count; i++)
      {
        if (statusDto.CurQuantities[i] == null)
          return BadRequest("Quantity value is invalid");

        var status = new Status
        {
          InventoryId = statusDto.InventoryId,
          ExpQuantity = statusDto.ExpQuantities[i],
          CurQuantity = statusDto.CurQuantities[i],
          IsStarted = statusDto.IsStarted,
          ByUser = user,
          Difference = statusDto.Differences[i],
          Updated = time,
          ProductId = statusDto.ProductIds[i]
        };

        db.Statuses.Add(status);

        if (statusDto.Differences[i] != 0)
        {
          var transaction = new Transaction
          {
            ToInventoryId = statusDto.InventoryId,
            ProductId = statusDto.ProductIds[i],
            Quantity = statusDto.Differences[i],
            Updated = time,
            ByUser = user + " [status]"
          };

          db.Transactions.Add(transaction);

          var toStockInDb = db.Stocks.Include(s => s.Inventory)
                    .Include(s => s.Product)
                    .Where(s => s.InventoryId == transaction.ToInventoryId)
                    .SingleOrDefault(s => s.ProductId == transaction.ProductId);

          toStockInDb.Quantity += transaction.Quantity;

          transaction.Updated = time;
          transaction.ByUser = user + " [status]";
          db.Transactions.Add(transaction);
        }

      }
      db.SaveChanges();

      return Ok();
    }
  }
}