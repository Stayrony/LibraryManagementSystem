﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryDalManagerTester.cs" company="">
//   
// </copyright>
// <summary>
//   The category dal manager tester.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace LMS.DAL.Test
{
    using System;

    using LMS.Service.Domain;

    using NUnit.Framework;

    /// <summary>
    /// The category dal manager tester.
    /// </summary>
    [TestFixture]
    public class CategoryDalManagerTester
    {
        /// <summary>
        /// The category dal manager.
        /// </summary>
        private CategoryDalManager categoryDalManager;

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.categoryDalManager = new CategoryDalManager();
        }

        /// <summary>
        /// The create category basic test.
        /// </summary>
        [Test]
        public void CreateCategoryBasicTest()
        {
            Category category = this.categoryDalManager.CreateCategory("Business");
            Console.Write("Create new category : ");
            Console.WriteLine("CategoryID = " + category.CategoryID + "; CategoryName = " + category.CategoryName);
        }

        /// <summary>
        /// The get category id by category name basic test.
        /// </summary>
        [Test]
        public void GetCategoryIDByCategoryNameBasicTest()
        {
            string categoryName = "Fiction";
            int id = this.categoryDalManager.GetCategoryIDByCategoryName(categoryName);
            if (id != 0)
            {
                Console.WriteLine("CategoryID = " + id);
            }
            else
            {
                Console.WriteLine("Category " + categoryName + " is undefined");
            }
        }

        /// <summary>
        /// The get all categories basic test.
        /// </summary>
        [Test]
        public void GetAllCategoriesBasicTest()
        {
            List<Category> categories = new List<Category>();
            categories = categoryDalManager.GetAllCategories();
            foreach (Category category in categories)
            {
                Console.WriteLine("CategoryID = " + category.CategoryID + "; CategoryName = " + category.CategoryName);
                Console.WriteLine();
            }
        }
    }
}