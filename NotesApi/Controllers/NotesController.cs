using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using System;
using System.Collections.Generic;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        static List<Notes> _notes = new List<Notes> { new Notes { Id = new System.Guid("2c0332ca-6f03-41a3-b331-aebc7f932f9a"), CategoryId = "1", OwnerId = new System.Guid("b081f808-7482-4bac-9733-8ffee3e4e1d9"), Title = "First Note", Description = "First Note Description" },
        new Notes { Id = new System.Guid("eac5067d-bb12-4458-8b9e-e6c545e43f7d"), CategoryId = "1", OwnerId = new System.Guid(), Title = "Second Note", Description = "Second Note Description" },
        new Notes { Id = new System.Guid("e8d26c47-1e9c-4429-a914-728617f32aab"), CategoryId = "3", OwnerId = new System.Guid(), Title = "Third Note", Description = "Third Note Description" },
        new Notes { Id = new System.Guid(), CategoryId = "2", OwnerId = new System.Guid(), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Notes { Id = new System.Guid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };


        public NotesController() { }

        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest("Note cannot be null");
            }
            _notes.Add(note);

            //   return StatusCode(StatusCodes.Status500InternalServerError, "Error in processing the note");
            return Ok();
        }

        [HttpGet("OwnerId/{id}")]
        public IActionResult GetByOwnerId(Guid id)
        {
            List<Notes> note = _notes.FindAll(note => note.OwnerId == id);

            return Ok(note);
        }

        /// <summary>
        ///     Return a note find by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetNoteById")]
        public IActionResult GetNoteById(Guid id)
        {

            List<Notes> note = _notes.FindAll(note => note.Id == id);
            if (note == null)
            {
                return BadRequest("Note was not found");
            }
            return Ok(note);
        }

        /// <summary>
        ///     See headers for url location of resources
        /// </summary>
        /// <param name="note"></param>
        /// <returns>See headers for url location of resources</returns>
        [HttpPost("Create")]
        public ActionResult<Notes> CreateWhitMethod([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest("Note was not found");
            }

            _notes.Add(note);

            return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
        }




        /// <summary>
        /// Returns a list of notes
        /// </summary>
        /// <returns></returns>
/*        [HttpGet("{id}")]
        public IActionResult Get(string id, string id2, string id3)
        {
            return Ok($"id: {id} id2: {id2}, id3: {id3}");
        }*/

        //  [HttpPost("")]
        //    public IActionResult Post([FromBody] Note bodyContent)  => Ok(bodyContent);

    }
}
