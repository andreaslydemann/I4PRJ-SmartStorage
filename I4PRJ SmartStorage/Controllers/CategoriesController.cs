﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using I4PRJ_SmartStorage.Models;
using I4PRJ_SmartStorage.Models.Domain;

namespace I4PRJ_SmartStorage.Controllers
{
  public class CategoriesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
      return View(db.Categories.Where(c => c.IsDeleted != true).ToList());
    }

    public ActionResult Details(int? id)
    {
      if(id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Category category = db.Categories.Find(id);
      if(category == null)
      {
        return HttpNotFound();
      }
      return View(category);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "CategoryId,Name,IsActiv,LastUpdated,ByUser,Version")] Category category)
    {
      if(ModelState.IsValid)
      {
        category.Updated = DateTime.Now;
        category.ByUser = User.Identity.Name;

        db.Categories.Add(category);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(category);
    }

    // GET: /Categories/Edit/5
    public ActionResult Edit(int? id)
    {
      if(id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Category category = db.Categories.Find(id);
      if(category == null)
      {
        return HttpNotFound();
      }
      return View(category);
    }

    // POST: /Categories/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "CategoryId,Name,LastUpdated,ByUser,Version")] Category category)
    {
      if(ModelState.IsValid)
      {
        category.Updated = DateTime.Now;
        category.ByUser = User.Identity.Name;

        db.Entry(category).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(category);
    }

    // GET: /Categories/Delete/5
    public ActionResult Delete(int? id)
    {
      if(id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Category category = db.Categories.Find(id);
      if(category == null)
      {
        return HttpNotFound();
      }
      return View(category);
    }

    // POST: /Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Category category = db.Categories.Find(id);
      category.IsDeleted = true;
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if(disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}