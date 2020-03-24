using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HillLabTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HillLabTest.Controllers
{
    [Route("api/[Controller]")]
    public class CategoriesController : ControllerBase
    {

        private readonly ILogger<CategoriesController> _logger;
        private readonly IProductContextWrapper _productContextWrapper;
        private IMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger, IProductContextWrapper productContextWrapper, IMapper mapper)
        {
            _logger = logger;
            _productContextWrapper = productContextWrapper;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var result = await _productContextWrapper.Category.GetAllCategories();
            var mappedResult = _mapper.Map<IEnumerable<CategoryDTO>>(result);
            return mappedResult;


        }
        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<CategoryDTO> GetCategory(int id)
        {
            var result = await _productContextWrapper.Category.GetCategoryById(id);
            var mappedResult = _mapper.Map<CategoryDTO>(result);
            return mappedResult;

        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryDTO categoryDTO)
        {
            try
            {
                if (categoryDTO == null)
                {
                    return BadRequest("Category is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model");
                }

                var category = _mapper.Map<Category>(categoryDTO);

                await _productContextWrapper.Category.CreateCategory(category);
                int result = await _productContextWrapper.Save();

                var newCategory = _mapper.Map<CategoryDTO>(category);

                return CreatedAtRoute("GetCategoryById", new { id = newCategory.CategoryId }, newCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryDTO categoryDTO)
        {
            try
            {
                if (categoryDTO == null)
                {
                    return BadRequest("Category is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model");
                }

                var category = _mapper.Map<Category>(categoryDTO);

                var categoryDB = await _productContextWrapper.Category.GetCategoryById(category.CategoryId);
                if (categoryDB == null)
                {
                    return NotFound();
                }

                categoryDB.CategoryName = category.CategoryName;

                _productContextWrapper.Category.UpdateCategory(categoryDB);
                int result = await _productContextWrapper.Save();
                if (result > 0)
                    return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var productsInCategory = await _productContextWrapper.Product.GetProductsInCategory(id);
                if(productsInCategory != null && productsInCategory.Count() > 0)
                    return BadRequest("Cannot delete the category. There are products under the category");

                var category = await _productContextWrapper.Category.GetCategoryById(id);
                if (category == null)
                    return NotFound();

                _productContextWrapper.Category.DeleteCategory(category);
                int result = await _productContextWrapper.Save();
                if (result > 0)
                    return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "Internal server error");
        }

    }
}
