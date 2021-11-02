namespace NotesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NotesApi.Models;
    using NotesApi.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="OwnerController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {

        readonly IOwnerCollectionService _ownerCollectionService;


        public OwnerController(IOwnerCollectionService ownerCollectionService)
        {
            _ownerCollectionService = ownerCollectionService;
        }

        /// <summary>
        /// The GetAllOwners.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {

            List<Owner> owners = await _ownerCollectionService.GetAll();
            return Ok(owners);

        }

        /// <summary>
        /// The AddOwner.
        /// </summary>
        /// <param name="bodyContent">The bodyContent<see cref="Owner"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> AddOwner([FromBody] Owner bodyContent)
        {

            if (bodyContent == null)
            {
                return BadRequest("Owner cannot be null");
            }

            if (await _ownerCollectionService.Create(bodyContent))
            {
                return Ok();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a specified owner.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            var index = await _ownerCollectionService.Delete(id);

            if (index == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// The UpdateOwner.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="ownerToUpdate">The ownerToUpdate<see cref="Owner"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] Owner ownerToUpdate)
        {
            if (ownerToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }

            var index = await _ownerCollectionService.Update(id, ownerToUpdate);

            if (index == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
