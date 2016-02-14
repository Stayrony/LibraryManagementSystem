// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateCategoryViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The create category view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Contract;
    using LMS.UI.Utility;

    /// <summary>
    /// The create category view model.
    /// </summary>
    public class CreateCategoryViewModel : ViewModelBase
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView view;

        /// <summary>
        /// The create category command.
        /// </summary>
        private RelayCommand createCategoryCommand;

        /// <summary>
        /// The category manager.
        /// </summary>
        private CategoryManager categoryManager;

        /// <summary>
        /// The all categories.
        /// </summary>
        private ObservableCollection<Category> allCategories;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryViewModel"/> class.
        /// </summary>
        /// <param name="View">
        /// The view.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public CreateCategoryViewModel(IView View)
        {
            try
            {
                this.view = View;
                this.categoryManager = new CategoryManager();
                this.allCategories = new ObservableCollection<Category>(categoryManager.GetAllCategories());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string CategoryName
        {
            get
            {
                return (string)GetValue(CategoryProperty);
            }

            set
            {
                SetValue(CategoryProperty, value);
            }
        }

        /// <summary>
        /// The category property.
        /// </summary>
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
            "CategoryName", 
            typeof(string), 
            typeof(CreateCategoryViewModel), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets the create category command.
        /// </summary>
        public ICommand CreateCategoryCommand
        {
            get
            {
                if (this.createCategoryCommand == null)
                {
                    this.createCategoryCommand = new RelayCommand(param => this.CreateCategory());
                }

                return this.createCategoryCommand;
            }
        }

        /// <summary>
        /// The create category.
        /// </summary>
        private void CreateCategory()
        {
            try
            {
                this.categoryManager.CreateCategory(this.CategoryName);
                Category newCategory = new Category { CategoryName = this.CategoryName };
                this.AllCategories.Insert(0, newCategory);
                this.CategoryName = string.Empty;
            }
            catch (Exception exception)
            {
                this.view.ShowError(exception.Message);
            }
        }

        /// <summary>
        /// Gets the all categories.
        /// </summary>
        public ObservableCollection<Category> AllCategories
        {
            get
            {
                return this.allCategories;
            }
        }
    }
}