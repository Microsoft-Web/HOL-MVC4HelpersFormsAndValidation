﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreManagerController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        //
        // GET: /StoreManager/

        public ActionResult Index()
        {
            var albums = this.db.Albums.Include(a => a.Genre).Include(a => a.Artist)
                .OrderBy(a => a.Price);

            return this.View(albums.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id)
        {
            Album album = this.db.Albums.Find(id);

            if (album == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.GenreId = new SelectList(this.db.Genres, "GenreId", "Name", album.GenreId);
            this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name", album.ArtistId);

            return this.View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                this.db.Entry(album).State = EntityState.Modified;
                this.db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            this.ViewBag.GenreId = new SelectList(this.db.Genres, "GenreId", "Name", album.GenreId);
            this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name", album.ArtistId);

            return this.View(album);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
