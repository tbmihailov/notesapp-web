using System;
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
    public class NotesController : ControllerBase
    {
        private NotesappContext db = new NotesappContext();

        //
        // GET: /Notes/

        public ActionResult Index()
        {
            var notes = db.Notes.Include(n => n.Group);
            return View(notes
						.ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Note>
						.ToList());
        }

        //
        // GET: /Notes/Details/5

        public ActionResult Details(int id)
        {
            Note note = db.Notes.ForUser(CurrentUserName).Single(n => n.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        //
        // GET: /Notes/Create

        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            return View(new Note());
        }

        //
        // POST: /Notes/Create

        [HttpPost]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
			    //note.Owner = CurrentUserName;//TODO: Add owner property with current user name/id
                note.Created = DateTime.Now;
                note.Owner = CurrentUserName;

                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", note.GroupId);
            return View(note);
        }

        //
        // GET: /Notes/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Note note = db.Notes.ForUser(CurrentUserName).Single(n => n.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", note.GroupId);
            return View("Create", note);
        }

        //
        // POST: /Notes/Edit/5

        [HttpPost]
        public ActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {

                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", note.GroupId);
            return View("Create", note);
        }

        //
        // GET: /Notes/Delete/5

        public ActionResult Delete(int id)
        {
            Note note = db.Notes.ForUser(CurrentUserName).Single(n => n.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }

            return View(note);
        }

        //
        // Post: /Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.ForUser(CurrentUserName).Single(n => n.Id == id);
            db.Notes.Remove(note);
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