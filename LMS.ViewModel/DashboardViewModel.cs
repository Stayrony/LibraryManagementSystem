// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashboardViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The dashboard view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



namespace LMS.ViewModel
{
    using System;
    using System.Windows.Input;

    using LMS.UI.Context;
    using LMS.UI.Contract;
    using LMS.UI.Utility;

    /// <summary>
    /// The dashboard view model.
    /// </summary>
    public class DashboardViewModel
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The issue book menu command.
        /// </summary>
        private RelayCommand issueBookMenuCommand;

        /// <summary>
        /// The create category command.
        /// </summary>
        private RelayCommand createCategoryCommand;

        /// <summary>
        /// The return book command.
        /// </summary>
        private RelayCommand returnBookCommand;

        /// <summary>
        /// The add remote book command.
        /// </summary>
        private RelayCommand addBookCommand;

        /// <summary>
        /// The remove book command.
        /// </summary>
        private RelayCommand removeBookCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public DashboardViewModel(IView view)
        {
            try
            {
                this.View = view;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Gets the issue book menu command.
        /// </summary>
        public ICommand IssueBookMenuCommand
        {
            get
            {
                if (this.issueBookMenuCommand == null)
                {
                    this.issueBookMenuCommand = new RelayCommand(param => this.IssueBookMenu());
                }

                return issueBookMenuCommand;
            }
        }

        /// <summary>
        /// The issue book menu.
        /// </summary>
        private void IssueBookMenu()
        {
            try
            {
                ControlManager.GetInstance().Place("DashboardControl", "mainRegion", "IssueBookControl");
            }
            catch (Exception exception)
            {
                this.View.ShowError(exception.Message);
            }
        }

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

                return createCategoryCommand;
            }
        }

        /// <summary>
        /// The create category.
        /// </summary>
        private void CreateCategory()
        {
            try
            {
                ControlManager.GetInstance().Place("DashboardControl", "mainRegion", "CreateCategoryControl");
            }
            catch (Exception exception)
            {
                this.View.ShowError(exception.Message);
            }
        }

        /// <summary>
        /// Gets the return book command.
        /// </summary>
        public ICommand ReturnBookCommand
        {
            get
            {
                if (this.returnBookCommand == null)
                {
                    this.returnBookCommand = new RelayCommand(param => this.ReturnBook());
                }

                return returnBookCommand;
            }
        }

        /// <summary>
        /// The return book.
        /// </summary>
        private void ReturnBook()
        {
            try
            {
                ControlManager.GetInstance().Place("DashboardControl", "mainRegion", "ReturnBookControl");
            }
            catch (Exception exception)
            {
                this.View.ShowError(exception.Message);
            }
        }

        /// <summary>
        /// Gets the add remove book command.
        /// </summary>
        public ICommand AddBookCommand
        {
            get
            {
                if (this.addBookCommand == null)
                {
                    this.addBookCommand = new RelayCommand(param => this.AddBook());
                }

                return addBookCommand;
            }
        }

        /// <summary>
        /// The add remove book.
        /// </summary>
        private void AddBook()
        {
            try
            {
                ControlManager.GetInstance().Place("DashboardControl", "mainRegion", "AddBookControl");
            }
            catch (Exception exception)
            {
                this.View.ShowError(exception.Message);
            }
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

                return removeBookCommand;
            }
        }

        /// <summary>
        /// The remove book.
        /// </summary>
        private void RemoveBook()
        {
            try
            {
                ControlManager.GetInstance().Place("DashboardControl", "mainRegion", "RemoveBookControl");
            }
            catch (Exception exception)
            {
                this.View.ShowError(exception.Message);
            }
        }
    }
}