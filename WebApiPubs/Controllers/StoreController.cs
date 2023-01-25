using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using WebApiPubs.Data;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        //GET todos
        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }

        //POST
        [HttpPost]
        public ActionResult<Store> Post(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Stores.Add(store);

            context.SaveChanges();

            return Ok();
        }

        //PUT
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Store store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }

            context.Entry(store).State = EntityState.Modified;

            context.SaveChanges();

            return Ok();
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            var store = (from str in context.Stores where str.StorId == id select str).SingleOrDefault();

            if (store == null)
            {
                return NotFound();
            }

            context.Stores.Remove(store);

            context.SaveChanges();

            return store;
        }

        //GET by id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Store>> Get(string id)
        {
            //Publisher publisher = (from pub in context.Publishers where pub.PubId == id select pub).SingleOrDefault();

            //return publisher;

            var result = context.Stores.Include(x => x.Sales).ToList();

            return result;
        }

        //GET by name
        [HttpGet("byname/{storName}")]
        public ActionResult<Store> GetByName(string storName)
        {

            Store store = (from str in context.Stores where str.StorName == storName select str).SingleOrDefault();

            return store;

        }

        //GET by zip
        [HttpGet("byzip/{zip}")]
        public ActionResult<Store> GetByZip(string zip)
        {

            Store store = (from str in context.Stores where str.Zip == zip select str).SingleOrDefault();

            return store;

        }

        //GET by citystate
        [HttpGet("bycitystate/{state}/{city}")]
        public ActionResult<Store> GetByCityState(string state, string city)
        {

            Store store = (from str in context.Stores where str.State == state && str.City == city select str).SingleOrDefault();

            return store;

        }

    }
}
