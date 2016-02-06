using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Helper properties for working with text fields.
    /// </summary>
    public static class TextFieldAssist
    {
        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty HintProperty = DependencyProperty.RegisterAttached(
            "Hint",
            typeof(string),
            typeof(TextFieldAssist),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the hint.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHint(DependencyObject element, string value)
        {
            element.SetValue(HintProperty, value);
        }

        /// <summary>
        /// Gets the hint.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string GetHint(DependencyObject element)
        {
            return (string)element.GetValue(HintProperty);
        }

        /// <summary>
        /// The text box view margin property
        /// </summary>
        public static readonly DependencyProperty TextBoxViewMarginProperty = DependencyProperty.RegisterAttached(
            "TextBoxViewMargin",
            typeof(Thickness),
            typeof(TextFieldAssist),
            new PropertyMetadata(new Thickness(double.NegativeInfinity), TextBoxViewMarginPropertyChangedCallback));

        /// <summary>
        /// Sets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetTextBoxViewMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(TextBoxViewMarginProperty, value);
        }

        /// <summary>
        /// Gets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static Thickness GetTextBoxViewMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(TextBoxViewMarginProperty);
        }

        /// <summary>
        /// The hint opacity property
        /// </summary>
        public static readonly DependencyProperty HintOpacityProperty = DependencyProperty.RegisterAttached(
            "HintOpacity",
            typeof(double),
            typeof(TextFieldAssist),
            new PropertyMetadata(.48));

        /// <summary>
        /// Gets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static double GetHintOpacityProperty(DependencyObject element)
        {
            return (double)element.GetValue(HintOpacityProperty);
        }

        /// <summary>
        /// Sets the hint opacity.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHintOpacity(DependencyObject element, double value)
        {
            element.SetValue(HintOpacityProperty, value);
        }

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static readonly DependencyProperty DecorationVisibilityProperty = DependencyProperty.RegisterAttached(
            "DecorationVisibility", typeof (Visibility), typeof (TextFieldAssist), new PropertyMetadata(default(Visibility)));

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static void SetDecorationVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(DecorationVisibilityProperty, value);
        }

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Visibility GetDecorationVisibility(DependencyObject element)
        {
            return (Visibility) element.GetValue(DecorationVisibilityProperty);
        }

        /// <summary>
        /// Internal framework use only.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text", typeof (string), typeof (TextFieldAssist), new PropertyMetadata(default(string), TextPropertyChangedCallback));

        /// <summary>
        /// Internal framework use only.
        /// </summary>
        public static void SetText(DependencyObject element, string value)
        {
            element.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Internal framework use only.
        /// </summary>
        public static string GetText(DependencyObject element)
        {
            return (string)element.GetValue(TextProperty);
        }

        private static readonly DependencyPropertyKey IsNullOrEmptyPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "IsNullOrEmpty", typeof(bool), typeof(TextFieldAssist), new PropertyMetadata(true));

        private static readonly DependencyProperty IsNullOrEmptyProperty =
            IsNullOrEmptyPropertyKey.DependencyProperty;

        private static void SetIsNullOrEmpty(DependencyObject element, bool value)
        {
            element.SetValue(IsNullOrEmptyPropertyKey, value);
        }

        public static bool GetIsNullOrEmpty(DependencyObject element)
        {
            return (bool)element.GetValue(IsNullOrEmptyProperty);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static readonly DependencyProperty ManagedProperty = DependencyProperty.RegisterAttached(
            "Managed", typeof(Control), typeof(TextFieldAssist), new PropertyMetadata(default(Control), ManagedPropertyChangedCallback));                


        /// <summary>
        /// Framework use only.
        /// </summary>
        public static void SetManaged(DependencyObject element, TextBox value)
        {
            element.SetValue(ManagedProperty, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static TextBox GetManaged(DependencyObject element)
        {
            return (TextBox) element.GetValue(ManagedProperty);
        }        

        #region Methods

        /// <summary>
        /// Applies the text box view margin.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="margin">The margin.</param>
        private static void ApplyTextBoxViewMargin(Control textBox, Thickness margin)
        {
            if (margin.Equals(new Thickness(double.NegativeInfinity)))
            {
                return;
            }

            var frameworkElement = (textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer)?.Content as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Margin = margin;
            }
        }

        /// <summary>
        /// The text box view margin property changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The dependency property changed event args.</param>
        private static void TextBoxViewMarginPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var box = dependencyObject as Control; //could be a text box or password box
            if (box == null)
            {
                return;
            }

            if (box.IsLoaded)
            {
                ApplyTextBoxViewMargin(box, (Thickness) dependencyPropertyChangedEventArgs.NewValue);
            }

            box.Loaded += (sender, args) =>
            {
                var textBox = (Control) sender;
                ApplyTextBoxViewMargin(textBox, GetTextBoxViewMargin(textBox));
            };
        }

        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var frameworkElement = (FrameworkElement)dependencyObject;

            if (frameworkElement == null) return;

            var state = string.IsNullOrEmpty((dependencyPropertyChangedEventArgs.NewValue ?? "").ToString())
                ? "MaterialDesignStateTextEmpty"
                : "MaterialDesignStateTextNotEmpty";

            if (frameworkElement.IsLoaded)
            {
                VisualStateManager.GoToState(frameworkElement, state, true);
            }
            else
            {
                frameworkElement.Loaded += (sender, args) => VisualStateManager.GoToState(frameworkElement, state, false);
                frameworkElement.IsVisibleChanged += (sender, args) => VisualStateManager.GoToState(frameworkElement, state, true);
            }

            SetIsNullOrEmpty(dependencyObject, string.IsNullOrEmpty((dependencyPropertyChangedEventArgs.NewValue ?? "").ToString()));
        }

        private static void ManagedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyPropertyChangedEventArgs.OldValue as Control;
            if (control != null)
            {
                control.IsVisibleChanged -= ManagedTextBoxOnIsVisibleChanged;                
            }

            control = dependencyPropertyChangedEventArgs.NewValue as Control;
            if (control != null)
            {
                control.IsVisibleChanged += ManagedTextBoxOnIsVisibleChanged;                
            }
        }

        private static void ManagedTextBoxOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!RefreshState(sender as TextBox, textBox => textBox.Text))
                RefreshState(sender as ComboBox, comboBox => comboBox.Text);
        }

        private static bool RefreshState<TControl>(TControl control, Func<TControl,  string> textAccessor) where TControl : Control
        {
            if (control == null) return false;
            if (!control.IsVisible) return true;

            //yep, had to invoke post this to trigger refresh
            control.Dispatcher.BeginInvoke(new Action(() =>
            {
                var state = string.IsNullOrEmpty(textAccessor(control))
                    ? "MaterialDesignStateTextEmpty"
                    : "MaterialDesignStateTextNotEmpty";

                VisualStateManager.GoToState(control, state, false);
            }));

            return true;
        }

        #endregion
    }
}
