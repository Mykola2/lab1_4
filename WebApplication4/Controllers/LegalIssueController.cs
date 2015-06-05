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
    public class LegalIssueController : ApiController
    {
        private registerEntities db = new registerEntities();

        // GET api/LegalIssue
        public IEnumerable<legalissues> Getlegalissues()
        {
            var legalissues = db.legalissues.Include(l => l.experts);
            return legalissues.AsEnumerable();
        }

        // GET api/LegalIssue/5
        public legalissues Getlegalissues(int id)
        {
            legalissues legalissues = db.legalissues.Find(id);
            if (legalissues == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return legalissues;
        }

        // PUT api/LegalIssue/5
        public HttpResponseMessage Putlegalissues(int id, legalissues legalissues)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != legalissues.idLegalIssues)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(legalissues).State = EntityState.Modified;

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

        // POST api/LegalIssue
        public HttpResponseMessage Postlegalissues(legalissues legalissues)
        {
            if (ModelState.IsValid)
            {
                db.legalissues.Add(legalissues);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, legalissues);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = legalissues.idLegalIssues }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/LegalIssue/5
        public HttpResponseMessage Deletelegalissues(int id)
        {
            legalissues legalissues = db.legalissues.Find(id);
            if (legalissues == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.legalissues.Remove(legalissues);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, legalissues);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}