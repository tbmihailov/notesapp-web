﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notesapp.Models;

namespace Notesapp.Controllers
{
	[Authorize]
    public class GroupsController : ControllerBase
    {
        private NotesappContext db = new NotesappContext();

        //
        // GET: /Groups/

        public ActionResult Index()
        {
            return View(db.Groups
						.ForUser(CurrentUserName)
						.ToList());
        }

        //
        // GET: /Groups/Details/5

        public ActionResult Details(int id)
        {
            Group group = db.Groups.ForUser(CurrentUserName).Single(g => g.Id == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        //
        // GET: /Groups/Create

        public ActionResult Create()
        {
            return View(new Group());
        }

        //
        // POST: /Groups/Create

        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                //group.Created = DateTime.Now;//TODO: Created property with Create date
			    group.Owner = CurrentUserName;//TODO: Add owner property with current user name/id
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        //
        // GET: /Groups/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Group group = db.Groups.ForUser(CurrentUserName).Single(g => g.Id == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View("Create", group);
        }

        //
        // POST: /Groups/Edit/5

        [HttpPost]
        public ActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
				//user ownership validation
                bool doesExistForCurrentUser = db.Groups
                            .Where(g => g.Id == group.Id)
                            .ForUser(CurrentUserName)
                            .Count() > 0;

                if (!doesExistForCurrentUser)
                {
                    return HttpNotFound();
                }				
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", group);
        }

        //
        // GET: /Groups/Delete/5

        public ActionResult Delete(int id)
        {
            Group group = db.Groups.ForUser(CurrentUserName).Single(g => g.Id == id);
			if (group == null)
            {
                return HttpNotFound();
            }
			
			return View(group);
        }

		//
        // POST: /Groups/Delete/5
		[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
			Group group = db.Groups.ForUser(CurrentUserName).Single(g => g.Id == id);
			if (group == null)
            {
                return HttpNotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}