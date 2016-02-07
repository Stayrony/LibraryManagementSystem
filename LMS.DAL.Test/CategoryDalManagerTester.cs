using System;
using LMS.Service.Domain;
using NUnit.Framework;

namespace LMS.DAL.Test
{
    [TestFixture]
    public class CategoryDalManagerTester
    {
        private CategoryDalManager categoryDalManager;

        [SetUp]
        public void SetUp()
        {
            categoryDalManager = new CategoryDalManager();
        }

        [Test]
        public void CreateCategoryBasicTest()
        {
            Category category = categoryDalManager.CreateCategory("Business");
            Console.Write("Create new category : ");
            Console.WriteLine("CategoryID = " + category.CategoryID + "; CategoryName = " + category.CategoryName);
        }

        [Test]
        public void GetCategoryIDByCategoryNameBasicTest()
        {
            string categoryName = "Fiction";
            int? id = categoryDalManager.GetCategoryIDByCategoryName(categoryName);
            if (id.HasValue)
            {
                Console.WriteLine("CategoryID = " + id);
            }
            else
            {
                Console.WriteLine("Category " + categoryName + " is undefined");
            }
        }
    }
}