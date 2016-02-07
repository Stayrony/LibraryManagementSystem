using System;
using LMS.DAL;
using LMS.Service.Domain;

namespace LMS.Service.BLL
{
    public class CategoryManager
    {
        private CategoryDalManager categoryDalManager;

        public CategoryManager()
        {
            categoryDalManager = new CategoryDalManager();
        }

        public Category CreateCategory(string categoryName)
        {
            int? categoryID = categoryDalManager.GetCategoryIDByCategoryName(categoryName);
            if (categoryID.HasValue)
            {
                throw new Exception("Category " + categoryName + " already exist.");
            }
            Category category = categoryDalManager.CreateCategory(categoryName);
            return category;
        }
    }
}