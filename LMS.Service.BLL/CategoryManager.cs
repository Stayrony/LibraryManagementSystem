// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryManager.cs" company="">
//   
// </copyright>
// <summary>
//   The category manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace LMS.Service.BLL
{
    using System;

    using LMS.DAL;
    using LMS.Service.Domain;

    /// <summary>
    /// The category manager.
    /// </summary>
    public class CategoryManager
    {
        /// <summary>
        /// The category dal manager.
        /// </summary>
        private readonly CategoryDalManager categoryDalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryManager"/> class.
        /// </summary>
        public CategoryManager()
        {
            this.categoryDalManager = new CategoryDalManager();
        }

        /// <summary>
        /// The create category.
        /// </summary>
        /// <param name="categoryName">
        /// The category name.
        /// </param>
        /// <returns>
        /// The <see cref="Category"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public Category CreateCategory(string categoryName)
        {
            int categoryID = this.categoryDalManager.GetCategoryIDByCategoryName(categoryName);
            if (categoryID != 0)
            {
                // TODO ExceptionBase create AlreadyExist
                throw new Exception("Category " + categoryName + " already exist.");
            }

            Category category = this.categoryDalManager.CreateCategory(categoryName);
            return category;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Category> GetAllCategories()
        {
            return categoryDalManager.GetAllCategories();
        }
    }
}