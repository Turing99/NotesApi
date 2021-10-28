using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        List<Category> categories = new List<Category>()
        {
           new Category(){ Id = new Guid("75e4aa03-26bb-4443-a01c-ebeb1c44a404"), Name ="To do"},
           new Category(){ Id = new System.Guid("e9886d05-9f08-424f-9477-3490a33880c3"), Name ="Doing"},
           new Category(){ Id = new System.Guid(), Name = "Done"}

        };

        public CategoriesController() { }


        /// <summary>
        ///     Returns the full list of categories
        /// </summary>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(categories);
        }


        /// <summary>
        ///     Returns only a category selected by the param id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        /// 

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {

            Category category = categories.FirstOrDefault(category => category.Id == id);
            if (category == null)
            {
                return BadRequest("Did not find the category with the id specified");
            }

            return Ok(category);
        }

        /// <summary>
        ///     Add a new category to the list
        /// </summary>
        /// <param name="bodyContent"></param>
        /// <response code="201">Created</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category bodyContent)
        {

            Category category = categories.FirstOrDefault(category => category.Id == bodyContent.Id);

            if (category == null)
            {
                categories.Add(bodyContent);

                return Ok(categories);
            }

            return BadRequest("Duplicate Id");

        }

        /// <summary>
        ///     Delete a category selected by the param id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {

            Category category = categories.FirstOrDefault(category => category.Id == id);
            if (category == null)
            {
                return BadRequest("Category was not found!");

            }

            categories.RemoveAt(categories.IndexOf(category));
            return Ok(categories);

        }
    }
}
