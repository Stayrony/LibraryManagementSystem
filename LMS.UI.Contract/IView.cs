// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="">
//   
// </copyright>
// <summary>
//   The View interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.UI.Contract
{
    /// <summary>
    ///     The View interface.
    /// </summary>
    public interface IView
    {
        /// <summary>
        ///     The set focus.
        /// </summary>
        void SetFocus();

        /// <summary>
        /// The show error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        void ShowError(string error);
    }
}