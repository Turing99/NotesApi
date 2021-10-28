namespace NotesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NotesApi.Models;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="OwnerController" />.
    /// </summary>
    /// 

    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        /// <summary>
        /// Defines the owners.
        /// </summary>
        internal List<Owner> owners = new List<Owner>()
        {
           new Owner(){ Id = new System.Guid(), Name ="Georgel"},
           new Owner(){ Id = new System.Guid(), Name ="Ivanovici"},
           new Owner(){ Id = new System.Guid(), Name = "Mache"}

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
            Owner owner = owners.FirstOrDefault(o => o.Id == bodyContent.Id);

            if (owner == null)
            {
                owners.Add(bodyContent);
                return Ok(owners);
            }

            return BadRequest("Duplicate Id");

        }
    }
}
