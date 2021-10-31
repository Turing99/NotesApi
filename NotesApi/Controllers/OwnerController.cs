namespace NotesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NotesApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="OwnerController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        /// <summary>
        /// Defines the owners.
        /// </summary>
        static List<Owner> owners = new List<Owner>()
        {
           new Owner(){ Id = Guid.NewGuid(), Name ="Georgel"},
           new Owner(){ Id = Guid.NewGuid(), Name ="Ivanovici"},
           new Owner(){ Id = Guid.NewGuid(), Name = "Mache"}

        };

        /// <summary>
        /// The GetAllOwners.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult GetAllOwners()
        {
            return Ok(owners);
        }

        /// <summary>
        /// The AddOwner.
        /// </summary>
        /// <param name="bodyContent">The bodyContent<see cref="Owner"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public IActionResult AddOwner([FromBody] Owner bodyContent)
        {
            Owner owner = owners.FirstOrDefault(o => o.Id.Equals(bodyContent.Id));

            if (owner == null)
            {
                owners.Add(bodyContent);
                return Ok(owners);
            }

            return BadRequest("Duplicate Id");
        }

        /// <summary>
        /// Delete a specified owner.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            var index = owners.FindIndex(owner => owner.Id.Equals(id));

            if (index == -1)
            {
                return NotFound();
            }
            owners.RemoveAt(index);

            return NoContent();
        }

        /// <summary>
        /// The UpdateOwner.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="ownerToUpdate">The ownerToUpdate<see cref="Owner"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] Owner ownerToUpdate)
        {
            if (ownerToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }

            var index = owners.FindIndex(owner => owner.Id.Equals(id));

            if (index == -1)
            {
                return NotFound();
            }

            ownerToUpdate.Id = owners[index].Id;
            owners[index] = ownerToUpdate;

            return NoContent();
        }
    }
}
