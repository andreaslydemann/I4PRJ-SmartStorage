﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using I4PRJ_SmartStorage.Dtos;
using I4PRJ_SmartStorage.Models;
using I4PRJ_SmartStorage.Models.Domain;
using I4PRJ_SmartStorage.ViewModels;

namespace I4PRJ_SmartStorage.Controllers.Api
{
    public class StatusController : ApiController
    {
        private ApplicationDbContext db;

        public StatusController()
        {
            db = new ApplicationDbContext();
        }

        public IHttpActionResult GetStatus(int id)
        {
            var inventory = db.Inventories.Find(id);

            if (inventory == null)
               return NotFound();
            
            var status = db.Stocks.Include(s => s.Inventory)
                .Include(p => p.Product)
                .Include(c => c.Product.Category).Where(i => i.InventoryId == id);

            return Ok(status);
        }

        [HttpPost]
        public IHttpActionResult CreateNewStatus(NewStatusDto statusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            for (int i = 0; i < statusDto.Quantities.Count; i++)
            {
                if(statusDto.Quantities[i] == null)
                    return BadRequest("Quantity value is invalid");

                var status = new Status
                {
                    InventoryId = statusDto.InventoryId,
                    Quantity = statusDto.Quantities[i],
                    IsStarted = true,
                    ByUser = User.Identity.Name,
                    Difference = statusDto.Differences[i],
                    Updated = DateTime.Now,
                    ProductId = statusDto.ProductIds[i]
                };

                db.Statuses.Add(status);
            }

            db.SaveChanges();

            return Ok();
        }

    }
}
