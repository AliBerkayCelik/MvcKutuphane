﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Siniflarim;
namespace MvcKutuphane.Controllers
{
    public class VitrinController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Vitrin
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBLKITAP.ToList();
            cs.Deger2 = db.TBLHAKKIMIZDA.ToList();
           
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBLMESAJ t)
        {
            db.TBLMESAJ.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}