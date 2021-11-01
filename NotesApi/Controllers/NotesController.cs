namespace NotesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NotesApi.Models;
    using NotesApi.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="NotesController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        INoteCollectionService _noteCollectionService;

        public NotesController(INoteCollectionService noteCollectionService)
        {
            _noteCollectionService = noteCollectionService ?? throw new ArgumentNullException(nameof(noteCollectionService));

        }

        /// <summary>
        /// The GetNotes.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult GetNotes()
        {
            List<Notes> notes = _noteCollectionService.GetAll();
            return Ok(notes);

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

            if (_noteCollectionService.Create(note))
            {
                return CreatedAtRoute("GetNoteById", new { id = note.Id.ToString() }, note);
            }
            return NoContent();
        }

        ///// <summary>
        ///// The GetByOwnerId.
        ///// </summary>
        ///// <param name="id">The id<see cref="Guid"/>.</param>
        ///// <returns>The <see cref="IActionResult"/>.</returns>
        //[HttpGet("OwnerId/{id}")]
        //public IActionResult GetByOwnerId(Guid id)
        //{
        //    List<Notes> note = _notes.FindAll(note => note.OwnerId.Equals(id));

        //    return Ok(note);
        //}

        /// <summary>
        /// Return a note find by a specified id.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
        [HttpGet("{id}", Name = "GetNoteById")]
        public IActionResult GetNoteById(Guid id)
        {
            var note = _noteCollectionService.Get(id);

            if (note == null)
            {
                return BadRequest("Note was not found");
            }
            return Ok(note);
        }

        ///// <summary>
        ///// See headers for url location of resources.
        ///// </summary>
        ///// <param name="note">.</param>
        ///// <returns>See headers for url location of resources.</returns>
        //[HttpPost("Create")]
        //public ActionResult<Notes> CreateWhithMethod([FromBody] Notes note)
        //{
        //    if (note == null)
        //    {
        //        return BadRequest("Note was not found");
        //    }

        //    _notes.Add(note);

        //    return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
        //}

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

            if (_noteCollectionService.Update(id, noteToUpdate))
            {
                return Ok(_noteCollectionService.Get(id));
            }

            return NotFound("Note not found!");
        }

        /// <summary>
        /// The DeleteNote.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null!");
            }

            if (_noteCollectionService.Delete(id))
            {
                return NoContent();
            }

            return NotFound("Note not found!");
        }

        ///// <summary>
        ///// The UpdateTitleNote.
        ///// </summary>
        ///// <param name="id">The id<see cref="Guid"/>.</param>
        ///// <param name="title">The title<see cref="string"/>.</param>
        ///// <returns>The <see cref="IActionResult"/>.</returns>
        //[HttpPatch("{id}/title")]
        //public IActionResult UpdateTitleNote(Guid id, [FromBody] string title)
        //{
        //    if (string.IsNullOrEmpty(title))
        //    {
        //        return BadRequest("The string cannot be null");
        //    }

        //    var index = _notes.FindIndex(note => note.Id.Equals(id));

        //    if (index == -1)
        //    {
        //        return NotFound();
        //    }
        //    _notes[index].Title = title;
        //    return Ok(_notes[index]);
        //}

        ///// <summary>
        ///// The UpdateNoteTitleByIds.
        ///// </summary>
        ///// <param name="id">The id<see cref="Guid"/>.</param>
        ///// <param name="ownerId">The ownerId<see cref="Guid"/>.</param>
        ///// <param name="title">The title<see cref="string"/>.</param>
        ///// <returns>The <see cref="IActionResult"/>.</returns>
        //[HttpPatch("{id}/{ownerId}/title")]
        //public IActionResult UpdateNoteTitleByIds(Guid id, Guid ownerId, [FromBody] string title)
        //{
        //    if (string.IsNullOrEmpty(title))
        //    {
        //        return BadRequest("Title cannot be null!");
        //    }

        //    int index = _notes.FindIndex(note => (note.Id.Equals(id)) && (note.OwnerId.Equals(ownerId)));
        //    if (index == -1)
        //    {
        //        return NotFound();
        //    }

        //    _notes[index].Title = title;
        //    return Ok(_notes[index]);
        //}

        ///// <summary>
        ///// The DeleteNoteTitleByIds.
        ///// </summary>
        ///// <param name="id">The id<see cref="Guid"/>.</param>
        ///// <param name="ownerId">The ownerId<see cref="Guid"/>.</param>
        ///// <returns>The <see cref="IActionResult"/>.</returns>
        //[HttpDelete("{id}/{ownerId}")]
        //public IActionResult DeleteNoteTitleByIds(Guid id, Guid ownerId)
        //{
        //    int index = _notes.FindIndex(note => (note.Id.Equals(id)) && (note.OwnerId.Equals(ownerId)));
        //    if (index == -1)
        //    {
        //        return NotFound();
        //    }

        //    _notes.RemoveAt(index);

        //    return NoContent();
        //}

        ///// <summary>
        ///// The DeleteNotesByOwner.
        ///// </summary>
        ///// <param name="ownerId">The ownerId<see cref="Guid"/>.</param>
        ///// <returns>The <see cref="IActionResult"/>.</returns>
        //[HttpDelete("owner/{ownerId}")]
        //public IActionResult DeleteNotesByOwnerId(Guid ownerId)
        //{
        //    List<Notes> notesByOwner = _notes.FindAll(note => note.OwnerId.Equals(ownerId));
        //    if (notesByOwner.Count > 0)
        //    {
        //        return NotFound();
        //    }

        //    _notes.RemoveAll(note => note.OwnerId == ownerId);

        //    return NoContent();
        //}
    }
}
