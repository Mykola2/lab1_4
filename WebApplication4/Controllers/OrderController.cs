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
    public class OrderController : ApiController
    {
        private registerEntities db = new registerEntities();

        // GET api/Order
        public IEnumerable<commissionorders> Getcommissionorders()
        {
            return db.commissionorders.AsEnumerable();
        }

        // GET api/Order/5
        public commissionorders Getcommissionorders(int id)
        {
            commissionorders commissionorders = db.commissionorders.Find(id);
            if (commissionorders == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return commissionorders;
        }

        // PUT api/Order/5
        public HttpResponseMessage Postcommissionorders(commissionorders commissionorders)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

           /* if (id != commissionorders.idCommissionOrders)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            */
            db.Entry(commissionorders).State = EntityState.Modified;

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

        // POST api/Order
        public HttpResponseMessage Putcommissionorders(commissionorders commissionorders)
        {
            if (ModelState.IsValid)
            {
                db.commissionorders.Add(commissionorders);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, commissionorders);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = commissionorders.idCommissionOrders }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Order/5
        public HttpResponseMessage Deletecommissionorders(int id)
        {
            commissionorders commissionorders = db.commissionorders.Find(id);
            if (commissionorders == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.commissionorders.Remove(commissionorders);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, commissionorders);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}