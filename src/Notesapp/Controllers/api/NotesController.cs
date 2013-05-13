using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Notesapp.Models;

namespace Notesapp.Controllers.Api
{
    [Authorize]
    public class NotesController : ApiControllerBase
    {
        private NotesappContext db = new NotesappContext();

        // GET api/Notes
        public IEnumerable<Note> GetNotes()
        {
            var notes = db.Notes
                        .Include(n => n.Group)
                        .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Note>
                        .ToList();
            return notes;
        }

        // GET api/Notes/5
        public Note GetNote(int id)
        {
            Note note = db.Notes
                          .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Note>
                          .Single(n => n.Id == id);
            if (note == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return note;
        }

        // PUT api/Notes/5
        public HttpResponseMessage PutNote(int id, Note note)
        {
            if (ModelState.IsValid && id == note.Id)
            {
                Note noteToCheck = db.Notes
                          .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Note>
                          .Single(n => n.Id == id);
                if (noteToCheck == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                db.Entry(note).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Notes
        public HttpResponseMessage PostNote(Note note)
        {
            if (ModelState.IsValid)
            {
                //note.Owner = CurrentUserName;//TODO: Add owner property with current user name/id
                note.Created = DateTime.Now;
                note.Owner = CurrentUserName;

                db.Notes.Add(note);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, note);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = note.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Notes/5
        public HttpResponseMessage DeleteNote(int id)
        {
            Note note = db.Notes
                          .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Note>
                          .Single(n => n.Id == id);
            if (note == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Notes.Remove(note);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, note);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}