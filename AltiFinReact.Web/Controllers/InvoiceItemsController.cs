using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AltiFinReact.Core.Entities;
using AltiFinReact.Core.Services;

namespace AltiFinReact.Web.Controllers
{
    [EnableCors("*", "*", "*")]
    public class InvoiceItemsController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/InvoiceItems
        public IQueryable<InvoiceItem> GetInvoiceItems()
        {
            return db.InvoiceItems;
        }

        // GET: api/InvoiceItems/5
        [ResponseType(typeof(InvoiceItem))]
        public IHttpActionResult GetInvoiceItem(int id)
        {
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return Ok(invoiceItem);
        }

        // PUT: api/InvoiceItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoiceItem(int id, InvoiceItem invoiceItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceItem.Id)
            {
                return BadRequest();
            }

            db.Entry(invoiceItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/InvoiceItems
        [ResponseType(typeof(InvoiceItem))]
        public IHttpActionResult PostInvoiceItem(InvoiceItem invoiceItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceItems.Add(invoiceItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoiceItem.Id }, invoiceItem);
        }

        // DELETE: api/InvoiceItems/5
        [ResponseType(typeof(InvoiceItem))]
        public IHttpActionResult DeleteInvoiceItem(int id)
        {
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            db.InvoiceItems.Remove(invoiceItem);
            db.SaveChanges();

            return Ok(invoiceItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceItemExists(int id)
        {
            return db.InvoiceItems.Count(e => e.Id == id) > 0;
        }
    }
}