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
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class DocumentsController : ApiController
    {
        private registerEntities db = new registerEntities();

        // GET api/Documents
        public IEnumerable<documents> Getdocuments()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.documents.AsEnumerable();
        }

        // GET api/Documents/5
        public documents Getdocuments(string id)
        {
            documents documents = db.documents.Find(id);
            if (documents == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return documents;
        }

        // PUT api/Documents/5
        public HttpResponseMessage Postdocuments(int id, documents documents)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != documents.idDocuments)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(documents).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Documents
        public HttpResponseMessage Putdocuments(documents documents)
        {
            if (ModelState.IsValid)
            {
                db.documents.Add(documents);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, documents);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = documents.idDocuments }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Documents/5
        public HttpResponseMessage Deletedocuments(int id)
        {
            documents documents = db.documents.Find(id);
            if (documents == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.documents.Remove(documents);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, documents);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}