﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        DbKutuphaneEntities db=new DbKutuphaneEntities();
		// GET: Kitap
        public ActionResult Index(string p)
        {
			var values = from x in db.TBLKITAP select x;
			if (!string.IsNullOrEmpty(p))
			{
				values = values.Where(y => y.AD.Contains(p));
			}
			return View(values.ToList());
		}
        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();

			List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
										   select new SelectListItem
										   {
											   Text = i.AD +" "+i.SOYAD,
											   Value = i.ID.ToString()
										   }).ToList();
			ViewBag.dgr1= deger1;
			ViewBag.dgr2 = deger2;
			return View();
        }
		[HttpPost]
		public ActionResult KitapEkle(TBLKITAP p)
		{
            var ktg=db.TBLKATEGORI.Where(k=>k.ID==p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);
            db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult KitapSil(int id)
		{
			var kitap = db.TBLKITAP.Find(id);
			db.TBLKITAP.Remove(kitap);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);
			List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
										   select new SelectListItem
										   {
											   Text = i.AD,
											   Value = i.ID.ToString()
										   }).ToList();
			List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
										   select new SelectListItem
										   {
											   Text = i.AD + " " + i.SOYAD,
											   Value = i.ID.ToString()
										   }).ToList();
			ViewBag.dgr1 = deger1;
			ViewBag.dgr2 = deger2;
			return View("KitapGetir", ktp);
        }
		public ActionResult KitapGuncelle(TBLKITAP p)
		{
			var kitap = db.TBLKITAP.Find(p.ID);
			kitap.AD = p.AD;
			kitap.BASIMYIL = p.BASIMYIL;
			kitap.YAYINEVI = p.YAYINEVI;
			kitap.SAYFA = p.SAYFA;
			kitap.DURUM =true;
			var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
			var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
			kitap.KATEGORI = ktg.ID;
			kitap.YAZAR = yzr.ID;

			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult OduncIade(int id)
		{
			var odn = db.TBLHAREKET.Find(id);
			return View("Odunciade", odn);
		}
		public ActionResult OduncGuncelle(TBLHAREKET p)
		{
			var hrk = db.TBLHAREKET.Find(p.ID);
			hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
			hrk.ISLEMDURUM = true;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}