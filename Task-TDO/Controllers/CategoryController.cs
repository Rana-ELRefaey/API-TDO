using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_TDO.Models;

namespace Task_TDO.Controllers
{

    public class CategoryController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();

        //select ALL
        public IHttpActionResult GetAll()
        {
            var categories = from x in db.Categories
                              select new CategoryDTO()
                              {
                                  CategoryID = x.CategoryID,
                                  CategoryName = x.CategoryName
                              };

            return Ok(categories);
        }

        //Select by ID
        public IHttpActionResult GetById(int id)
        {
            var c = from x in db.Categories
                    where x.CategoryID == id
                    select new CategoryDTO()
                    {
                        CategoryID = x.CategoryID,
                        CategoryName = x.CategoryName,
                    };

            if (c == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(c);
            }
        }

        //Edit
        public IHttpActionResult Put(int id, Category ctg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ctg.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(ctg).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //insert New Categ
        public IHttpActionResult Post(Category ctg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(ctg);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ctg.CategoryID }, ctg);
        }

        //Delete
        public IHttpActionResult Delete(int id)
        {
            Category ctg = db.Categories.Find(id);
            if (ctg == null)
            {
                return NotFound();
            }

            db.Categories.Remove(ctg);
            db.SaveChanges();

            return Ok(ctg);
        }
    }
}
