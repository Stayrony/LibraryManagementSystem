// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddRemoveBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The add remove book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using LMS.Service.BLL;
using LMS.Service.Domain;

using Microsoft.Practices.Prism.PubSubEvents;

namespace LMS.ViewModel
{
    using System;
    using System.CodeDom;
    using System.Windows.Input;

    using LMS.UI.Contract;
    using LMS.UI.Utility;

    /// <summary>
    /// The add remove book view model.
    /// </summary>
    public class AddRemoveBookViewModel : ViewModelBase
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The add book command.
        /// </summary>
        private RelayCommand addBookCommand;

        /// <summary>
        /// The remove book command.
        /// </summary>
        private RelayCommand removeBookCommand;

        /// <summary>
        /// The category manager.
        /// </summary>
        private CategoryManager categoryManager;

        /// <summary>
        /// The book manager.
        /// </summary>
        private BookManager bookManager;

        /// <summary>
        /// The _all categories.
        /// </summary>
        private ObservableCollection<Category> _allCategories;

        /// <summary>
        /// The _event aggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The subscription token.
        /// </summary>
        private SubscriptionToken subscriptionToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRemoveBookViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="eventAggregator">
        /// The event Aggregator.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public AddRemoveBookViewModel(IView view, IEventAggregator eventAggregator)
        {
            try
            {
                this.View = view;
                this._eventAggregator = eventAggregator;
                this.categoryManager = new CategoryManager();
                this.bookManager = new BookManager();
                this._allCategories = new ObservableCollection<Category>(this.categoryManager.GetAllCategories());
                CreateCategoryEvent categoryAddedEvent = eventAggregator.GetEvent<CreateCategoryEvent>();
                this.subscriptionToken = categoryAddedEvent.Subscribe(
                    this.AddNewCategoryEventHandler, 
                    ThreadOption.UIThread, 
                    false, 
                    (category) => true);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// The add new category event handler.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public void AddNewCategoryEventHandler(Category category)
        {
            this.AllCategories.Add(category);
        }

        /// <summary>
        /// Gets the add book command.
        /// </summary>
        public ICommand AddBookCommand
        {
            get
            {
                if (this.addBookCommand == null)
                {
                    this.addBookCommand = new RelayCommand(param => this.AddBook());
                }

                return this.addBookCommand;
            }
        }

        /// <summary>
        /// The add book.
        /// </summary>
        private void AddBook()
        {
            Book book = new Book();
            book.Author = this.Author;
            book.Title = this.Title;
            book.QuantityOfBooksIssued = this.SelectedQuantity;
            book.Category = this.SelectedCategory.CategoryName;
            Book newBook = this.bookManager.CreateBook(book);

            this._eventAggregator.GetEvent<CreateBookEvent>().Publish(newBook);

            // Empty fields
            this.Author = string.Empty;
            this.Title = string.Empty;
            this.SelectedCategory = null;

            // TODO empty SelectedQuantity
            this.SelectedQuantity = new int();
        }

        /// <summary>
        /// Gets the remove book command.
        /// </summary>
        public ICommand RemoveBookCommand
        {
            get
            {
                if (this.removeBookCommand == null)
                {
                    this.removeBookCommand = new RelayCommand(param => this.RemoveBook());
                }

                return this.removeBookCommand;
            }
        }

        /// <summary>
        /// The remove book.
        /// </summary>
        private void RemoveBook()
        {
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get
            {
                return (string)this.GetValue(TitleProperty);
            }

            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// The title property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", 
            typeof(string), 
            typeof(AddRemoveBookViewModel), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author
        {
            get
            {
                return (string)this.GetValue(AuthorProperty);
            }

            set
            {
                this.SetValue(AuthorProperty, value);
            }
        }

        /// <summary>
        /// The author property.
        /// </summary>
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register(
            "Author", 
            typeof(string), 
            typeof(AddRemoveBookViewModel), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the quantity of books issued.
        /// </summary>
        public int QuantityOfBooksIssued
        {
            get
            {
                return (int)this.GetValue(QuantityOfBooksIssuedProperty);
            }

            set
            {
                this.SetValue(QuantityOfBooksIssuedProperty, value);
            }
        }

        /// <summary>
        /// The quantity of books issued property.
        /// </summary>
        public static readonly DependencyProperty QuantityOfBooksIssuedProperty =
            DependencyProperty.Register(
                "QuantityOfBooksIssued", 
                typeof(int), 
                typeof(AddRemoveBookViewModel), 
                new PropertyMetadata(0));

        /// <summary>
        /// Gets the list quantity.
        /// </summary>
        public IList<int> ListQuantity
        {
            get
            {
                return new List<int>(Enumerable.Range(1, 500));
            }
        }

        /// <summary>
        /// Gets or sets the selected quantity.
        /// </summary>
        public int SelectedQuantity
        {
            get
            {
                return (int)this.GetValue(SelectedQuantityProperty);
            }

            set
            {
                this.SetValue(SelectedQuantityProperty, value);
            }
        }

        /// <summary>
        /// The selected quantity property.
        /// </summary>
        public static readonly DependencyProperty SelectedQuantityProperty =
            DependencyProperty.Register(
                "SelectedQuantity", 
                typeof(int), 
                typeof(AddRemoveBookViewModel), 
                new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the selected category.
        /// </summary>
        public Category SelectedCategory
        {
            get
            {
                return (Category)this.GetValue(SelectedCategoryProperty);
            }

            set
            {
                this.SetValue(SelectedCategoryProperty, value);
            }
        }

        /// <summary>
        /// The selected category property.
        /// </summary>
        public static readonly DependencyProperty SelectedCategoryProperty =
            DependencyProperty.Register(
                "SelectedCategory", 
                typeof(Category), 
                typeof(AddRemoveBookViewModel), 
                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets the all categories.
        /// </summary>
        public ObservableCollection<Category> AllCategories
        {
            get
            {
                // List<Category> categories = this.categoryManager.GetAllCategories();
                // List<string> result = new List<string>();
                // foreach (Category category in categories)
                // {
                // result.Add(category.CategoryName);
                // }

                // return result;
                // return this.categoryManager.GetAllCategories();
                return this._allCategories;
            }
        }
    }
}