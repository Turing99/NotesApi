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
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Second Note", Description = "Second Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "3", OwnerId = new System.Guid(), Title = "Third Note", Description = "Third Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "2", OwnerId = new System.Guid(), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Fifth Note", Description = "Fifth Note Description" }
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
            List<Notes> note = _notes.FindAll(note => note.OwnerId.Equals(id));

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

            List<Notes> note = _notes.FindAll(note => note.Id.Equals(id));
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
        public ActionResult<Notes> CreateWhithMethod([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest("Note was not found");
            }

            _notes.Add(note);

            return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Notes noteToUpdate)
        {
            if (noteToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }

            var index = _notes.FindIndex(note => note.Id.Equals(id));

            if (index == -1)
            {
                return NotFound();
            }

            noteToUpdate.Id = _notes[index].Id;
            _notes[index] = noteToUpdate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var index = _notes.FindIndex(note => note.Id.Equals(id));

            if (index == -1)
            {
                return NotFound();
            }

            _notes.RemoveAt(index);

            return NoContent();
        }

        [HttpPatch("{id}/title")]
        public IActionResult UpdateTitleNote(Guid id, [FromBody] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("The string cannot be null");
            }

            var index = _notes.FindIndex(note => note.Id.Equals(id));

            if (index == -1)
            {
                return NotFound();
            }

            _notes[index].Title = title;
            return Ok(_notes[index]);
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
