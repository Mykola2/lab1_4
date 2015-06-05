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
    public class ExpertController : ApiController
    {
        private registerEntities db = new registerEntities();

        // GET api/Expert
        public IEnumerable<experts> Getexperts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.experts.AsEnumerable();
        }

        
        public experts Getexperts(int id)
        {
            experts experts = db.experts.Find(id);
            if (experts == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return experts;
        }

       
        public HttpResponseMessage Postexperts(experts experts)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            /*if (id != experts.idExperts)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            */
            db.Entry(experts).State = EntityState.Modified;

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

        
        public HttpResponseMessage Putexperts(experts experts)
        {
            if (ModelState.IsValid)
            {
                db.experts.Add(experts);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, experts);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = experts.idExperts }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Expert/5
        public HttpResponseMessage Deleteexperts(int id)
        {
            experts experts = db.experts.Find(id);
            if (experts == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.experts.Remove(experts);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, experts);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}