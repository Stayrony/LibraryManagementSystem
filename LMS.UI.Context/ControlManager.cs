// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlManager.cs" company="">
//   
// </copyright>
// <summary>
// The control manager. Main controller responsible for forms switching.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LMS.UI.Context
{
    public class ControlManager
    {

        /// <summary>
        /// The control manager.
        /// </summary>
        protected static ControlManager controlManager;

        /// <summary>
        /// The control dictionary.
        /// </summary>
        private readonly Dictionary<string, UIElement> controlDictionary = new Dictionary<string, UIElement>();

        /// <summary>
        /// The control type dictionary.
        /// </summary>
        private readonly Dictionary<string, Type> controlTypeDictionary = new Dictionary<string, Type>();

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <returns>
        /// The <see cref="ControlManager"/>.
        /// </returns>
        public static ControlManager GetInstance()
        {
            if (controlManager == null)
            {
                controlManager = new ControlManager();
            }

            return controlManager;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <example>
        /// <code>
        /// (ControlManager.GetInstance()).Add("ScenarioCollectionPlayControl", typeof(ScenarioCollectionPlayControl));
        /// this.ControlTypeDictionary.Add("MainWindow", typeof(MainWindow));
        /// </code>
        /// </example>
        public void Add(string key, Type elementType)
        {
            this.controlTypeDictionary.Add(key, elementType);
        }

        /// <summary>
        /// The get control pure.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="UIElement"/>.
        /// </returns>
        public UIElement GetControlPure(string key, params object[] parameter)
        {
            UIElement userControl;

            Type controlType = this.controlTypeDictionary[key];
            object _userControl;

            if (parameter != null)
            {
                _userControl = Activator.CreateInstance(controlType, parameter);
            }
            else
            {
                _userControl = Activator.CreateInstance(controlType);
            }

            userControl = (UIElement)_userControl;

            return userControl;
        }

        /// <summary>
        /// The get control.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="UIElement"/>.
        /// </returns>
        public UIElement GetControl(string key, params object[] parameter)
        {
            UIElement userControl = null;
            if (this.controlDictionary.ContainsKey(key))
            {
                userControl = this.controlDictionary[key];
            }
            else
            {
                if (this.controlTypeDictionary.ContainsKey(key))
                {
                    Type controlType = this.controlTypeDictionary[key];
                    object _userControl;
                    if (parameter != null)
                    {
                        _userControl = Activator.CreateInstance(controlType, parameter);
                    }
                    else
                    {
                        _userControl = Activator.CreateInstance(controlType);
                    }

                    userControl = (UIElement)_userControl;
                    this.controlDictionary.Add(key, userControl);
                }
            }

            return userControl;
        }

        /// <summary>
        /// The get control.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="UIElement"/>.
        /// </returns>
        public UIElement GetControl(string key)
        {
            return this.GetControl(key, null);
        }

        /// <summary>
        /// The place.
        /// </summary>
        /// <param name="containerName">
        /// The container name.
        /// </param>
        /// <param name="elementName">
        /// The element name.
        /// </param>
        public void Place(string containerName, string elementName)
        {
            ContentControl containerControl = this.GetControl(containerName) as ContentControl;
            ContentControl elementControl = this.GetControl(elementName) as ContentControl;

            if (containerControl != null)
            {
                containerControl.Content = elementControl;
            }
        }

        /// <summary>
        /// The place.
        /// </summary>
        /// <param name="containerName">
        /// The container name.
        /// </param>
        /// <param name="regionName">
        /// The region name.
        /// </param>
        /// <param name="elementName">
        /// The element name.
        /// </param>
        public void Place(string containerName, string regionName, string elementName)
        {
            this.Place(containerName, regionName, elementName, null);
        }

        /// <summary>
        /// The place.
        /// </summary>
        /// <param name="containerName">
        /// The container name.
        /// </param>
        /// <param name="regionName">
        /// The region name.
        /// </param>
        /// <param name="elementName">
        /// The element name.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Place(string containerName, string regionName, string elementName, params object[] parameter)
        {
            ContentControl containerControl = this.GetControl(containerName) as ContentControl;
            ContentControl elementControl = this.GetControl(elementName, parameter) as ContentControl;
            object o = containerControl.FindName(regionName);

            if (o is DockPanel)
            {
                ((DockPanel)o).Children.Clear();

                if (elementControl != null)
                {
                    ((DockPanel)o).Children.Add(elementControl);
                }
            }
            else if (o is Grid)
            {
                ((Grid)o).Children.Clear();
                if (elementControl != null)
                {
                    ((Grid)o).Children.Add(elementControl);
                }
            }
            else
            {
                ContentControl regionControl = o as ContentControl;
                if (elementControl != null)
                {
                    regionControl.Content = elementControl;
                }
            }
            }

    }
}
