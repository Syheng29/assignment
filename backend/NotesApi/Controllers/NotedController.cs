using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Context;
using NotesApi.Models;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly AppDbContext context;
        public NotesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.notes.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}", Name = "Notes")]
        public ActionResult Get(int id)
        {
            try
            {
                var gestor = context.notes.FirstOrDefault(g => g.Id == id);
                return Ok(gestor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public ActionResult Post([FromBody] Notes note)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                note.CreatedAt = DateTime.UtcNow;
                context.notes.Add(note);
                context.SaveChanges();
                return CreatedAtRoute("Notes", new { id = note.Id }, note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Notes notes)
        {
            try
            {
                if (notes.Id == id)
                {
                    notes.UpdatedAt = DateTime.UtcNow;
                    context.Entry(notes).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Notes", new { id = notes.Id }, notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var note = context.notes.FirstOrDefault(g => g.Id == id);
                if (note != null)
                {
                    context.notes.Remove(note);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
