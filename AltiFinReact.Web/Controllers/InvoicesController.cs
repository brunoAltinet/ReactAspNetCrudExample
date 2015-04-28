using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AltiFinReact.Core.Entities;
using AltiFinReact.Core.Services;
using AltiFinReact.Web.Models;
using Microsoft.Owin.Security.Facebook;

namespace AltiFinReact.Web.Controllers
{
    [EnableCors("*", "*", "*")]
    public class InvoicesController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        public InvoicesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Invoices
        public IQueryable<InvoiceDto> GetInvoices()
        {
            return db.Invoices.Include(x => x.Partner).Select(x => new InvoiceDto { Id = x.Id, Ordinal = x.Ordinal, PartnerName = x.Partner.Name });
        }

        // GET: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult GetInvoice(int id)
        {
            Invoice invoice = db.Invoices.Include(x => x.InvoiceItems).SingleOrDefault(x => x.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // PUT: api/Invoices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoice(int id, Invoice invoice)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.Id)
            {
                return BadRequest();
            }

            var ent = db.Invoices.Include(x => x.InvoiceItems).Single(x => x.Id == invoice.Id);

            db.Entry(ent).CurrentValues.SetValues(invoice);

            foreach (var entItem in ent.InvoiceItems.ToList())
                if (invoice.InvoiceItems.All(x => x.Id != entItem.Id))
                    db.InvoiceItems.Remove(entItem);

            foreach (var mItem in invoice.InvoiceItems)
            {
                mItem.Invoice = ent;
                var entItem = db.InvoiceItems.SingleOrDefault(x => x.Id == mItem.Id);
                if (entItem != null)
                    db.Entry(entItem).CurrentValues.SetValues(mItem);
                else
                    db.InvoiceItems.Add(mItem);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);
            foreach (var item in invoice.InvoiceItems)
            {
                item.Invoice = invoice;
                db.InvoiceItems.Add(item);
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult DeleteInvoice(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            db.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return db.Invoices.Count(e => e.Id == id) > 0;
        }
    }
}