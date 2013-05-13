using Notesapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Notesapp.Controllers.Api
{
    [Authorize]
    public class GroupsController : ApiControllerBase
    {
        private NotesappContext db = new NotesappContext();

        // GET api/Groups
        public IEnumerable<Group> GetGroups()
        {
            var groups = db.Groups
                        .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Group>
                        .ToList();
            return groups;
        }

        // GET api/Groups/5
        public Group GetGroup(int id)
        {
            Group group = db.Groups
                          .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Group>
                          .Single(n => n.Id == id);
            if (group == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return group;
        }

        // PUT api/Groups/5
        public HttpResponseMessage PutGroup(int id, Group group)
        {
            if (ModelState.IsValid && id == group.Id)
            {
                Group groupToCheck = db.Groups
                          .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Group>
                          .Single(n => n.Id == id);
                if (groupToCheck == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                db.Entry(group).State = EntityState.Modified;

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

        // POST api/Groups
        public HttpResponseMessage PostGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                //group.Owner = CurrentUserName;//TODO: Add owner property with current user name/id
                //group.Created = DateTime.Now;
                group.Owner = CurrentUserName;

                db.Groups.Add(group);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, group);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = group.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Groups/5
        public HttpResponseMessage DeleteGroup(int id)
        {
            Group group = db.Groups
                          .ForUser(CurrentUserName)//TO DO add ForUser extensions to IQueryable<Group>
                          .Single(n => n.Id == id);
            if (group == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Groups.Remove(group);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, group);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}