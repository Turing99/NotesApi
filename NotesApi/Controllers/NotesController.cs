namespace NotesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NotesApi.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="NotesController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// Defines the _notes.
        /// </summary>
        internal static List<Notes> _notes = new List<Notes> { new Notes { Id = new System.Guid("2c0332ca-6f03-41a3-b331-aebc7f932f9a"), CategoryId = "1", OwnerId = new System.Guid("b081f808-7482-4bac-9733-8ffee3e4e1d9"), Title = "First Note", Description = "First Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Second Note", Description = "Second Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "3", OwnerId = new System.Guid(), Title = "Third Note", Description = "Third Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "2", OwnerId = new System.Guid(), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        public NotesController()
        {
        }

        /// <summary>
        /// The GetNotes.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }

        /// <summary>
        /// The CreateNote.
        /// </summary>
        /// <param name="note">The note<see cref="Notes"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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

        /// <summary>
        /// The GetByOwnerId.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet("OwnerId/{id}")]
        public IActionResult GetByOwnerId(Guid id)
        {
            List<Notes> note = _notes.FindAll(note => note.OwnerId.Equals(id));

            return Ok(note);
        }

        /// <summary>
        /// Return a note find by a specified id.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
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
        /// See headers for url location of resources.
        /// </summary>
        /// <param name="note">.</param>
        /// <returns>See headers for url location of resources.</returns>
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

        /// <summary>
        /// The UpdateNote.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="noteToUpdate">The noteToUpdate<see cref="Notes"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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

        /// <summary>
        /// The DeleteNote.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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

        /// <summary>
        /// The UpdateTitleNote.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
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
        /// The UpdateNoteTitleByIds.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="ownerId">The ownerId<see cref="Guid"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPatch("{id}/{ownerId}/title")]
        public IActionResult UpdateNoteTitleByIds(Guid id, Guid ownerId, [FromBody] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title cannot be null!");
            }

            int index = _notes.FindIndex(note => (note.Id.Equals(id)) && (note.OwnerId.Equals(ownerId)));
            if (index == -1)
            {
                return NotFound();
            }

            _notes[index].Title = title;
            return Ok(_notes[index]);
        }

        /// <summary>
        /// The DeleteNoteTitleByIds.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="ownerId">The ownerId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpDelete("{id}/{ownerId}")]
        public IActionResult DeleteNoteTitleByIds(Guid id, Guid ownerId)
        {
            int index = _notes.FindIndex(note => (note.Id.Equals(id)) && (note.OwnerId.Equals(ownerId)));
            if (index == -1)
            {
                return NotFound();
            }

            _notes.RemoveAt(index);

            return NoContent();
        }

        /// <summary>
        /// The DeleteNotesByOwner.
        /// </summary>
        /// <param name="ownerId">The ownerId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpDelete("owner/{ownerId}")]
        public IActionResult DeleteNotesByOwnerId(Guid ownerId)
        {
            List<Notes> notesByOwner = _notes.FindAll(note => note.OwnerId.Equals(ownerId));
            if (notesByOwner.Count > 0)
            {
                return NotFound();
            }

            _notes.RemoveAll(note => note.OwnerId == ownerId);

            return NoContent();
        }
    }
}
