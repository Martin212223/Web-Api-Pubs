using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Data;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {

        private readonly pubsContext context;

        public PublisherController(pubsContext context)
        {
            this.context = context;
        }

        //GET todos
        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return context.Publishers.ToList();
        }

        //GET por id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Publisher>> Get(string id)
        {
            //Publisher publisher = (from pub in context.Publishers where pub.PubId == id select pub).SingleOrDefault();

            //return publisher;

            var result = context.Publishers.Include(x => x.Titles).ToList();

            return result;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Autor>> Get()
        //{
        //    //return context.Autores.ToList();
        //    var result = context.Autores.Include(x => x.Libros).ToList();
        //    return result;
        //}


        //POST
        [HttpPost]
        public ActionResult<Publisher> Post(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Publishers.Add(publisher);

            context.SaveChanges();

            return Ok();
        }

        //PUT
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody]Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            context.Entry(publisher).State = EntityState.Modified;

            context.SaveChanges();

            return Ok();
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Publisher> Delete(string id)
        {
            var publisher = (from pub in context.Publishers where pub.PubId == id select pub).SingleOrDefault();

            if (publisher == null)
            {
                return NotFound();
            }

            context.Publishers.Remove(publisher);

            context.SaveChanges();

            return publisher;
        }
    }
}
