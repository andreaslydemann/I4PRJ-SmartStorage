﻿using SmartStorage.DAL.Context;
using SmartStorage.DAL.Interfaces.Repositories;
using SmartStorage.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartStorage.DAL.Repositories
{
  public class StatusesRepository : Repository<Status>, IStatusesRepository
  {
    public StatusesRepository(ApplicationDbContext context) : base(context)
    {
    }

    public ApplicationDbContext ApplicationDbContext
    {
      get { return Context as ApplicationDbContext; }
    }

    public new List<Status> GetAll()
    {
      return base.Context.Set<Status>().Include("Inventory").Include("Product").ToList();
    }
  }
}
