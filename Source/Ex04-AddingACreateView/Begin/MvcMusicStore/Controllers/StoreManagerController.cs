﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class StoreManagerController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();

        //
        // GET: /StoreManager/

        public ActionResult Index()
        {
            var albums = storeDB.Albums
                .Include("Genre").Include("Artist")
                .ToList();

            return View(albums);
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
            Album album = storeDB.Albums.Single(a => a.AlbumId == id);

            var viewModel = new StoreManagerViewModel()
            {
                Album = album,
                Genres = new SelectList(storeDB.Genres.ToList(), "GenreId", "Name", album.GenreId),
                Artists = new SelectList(storeDB.Artists.ToList(), "ArtistId", "Name", album.ArtistId)
            };

            return View(viewModel);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var album = storeDB.Albums.Single(a => a.AlbumId == id);

            try
            {
                // Save Album

                UpdateModel(album, "Album");
                storeDB.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                // Error occurred - so redisplay the form

                var viewModel = new StoreManagerViewModel()
                {
                    Album = album,
                    Genres = new SelectList(storeDB.Genres.ToList(), "GenreId", "Name", album.GenreId),
                    Artists = new SelectList(storeDB.Artists.ToList(), "ArtistId", "Name", album.ArtistId)
                };

                return View(viewModel);
            }
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
