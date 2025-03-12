using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
		DbKutuphaneEntities db = new DbKutuphaneEntities();
		// GET: Yazar
		public ActionResult Index()
        {
			var degerler = db.TBLYAZAR.ToList();
			return View(degerler);
		}
		[HttpGet]
		public ActionResult YazarEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult YazarEkle(TBLYAZAR p)
		{
			db.TBLYAZAR.Add(p);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult YazarSil(int id)
		{
			var yazar = db.TBLYAZAR.Find(id);
			db.TBLYAZAR.Remove(yazar);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult YazarGetir(int id)
		{
			var yazar = db.TBLYAZAR.Find(id);
			return View("YazarGetir", yazar);
		}
		public ActionResult YazarGuncelle(TBLYAZAR p)
		{
			var yazar = db.TBLYAZAR.Find(p.ID);
			yazar.AD = p.AD;
			yazar.SOYAD = p.SOYAD;
			yazar.DETAY = p.DETAY;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}