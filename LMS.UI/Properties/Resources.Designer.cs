﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Resources.Designer.cs">
//   
// </copyright>
// <auto-generated>
//     Этот код создан программным средством.
//     Версия среды выполнения: 4.0.30319.0
//     Изменения в этом файле могут привести к неправильному поведению и будут утрачены, если
//     код создан повторно.
// </auto-generated>
// <summary>
//   Класс ресурсов со строгим типом для поиска локализованных строк и пр.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.UI.Properties
{
    using System.Globalization;
    using System.Resources;

    /// <summary>
    ///   Класс ресурсов со строгим типом для поиска локализованных строк и пр.
    /// </summary>
    // Этот класс был автоматически создан при помощи StronglyTypedResourceBuilder
    // класс с  помощью таких средств, как ResGen или Visual Studio.
    // Для добавления или удаления члена измените файл .ResX, а затем перезапустите ResGen
    // с параметром /str или заново постройте свой VS-проект.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources
    {
        /// <summary>
        /// The resource man.
        /// </summary>
        private static ResourceManager resourceMan;

        /// <summary>
        /// The resource culture.
        /// </summary>
        private static CultureInfo resourceCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        /// <summary>
        ///   Возврат кэшированного экземпляра ResourceManager, используемого этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (resourceMan == null)
                {
                    ResourceManager temp = new global::System.Resources.ResourceManager("LMS.UI.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }

                return resourceMan;
            }
        }

        /// <summary>
        ///   Переопределяет свойство CurrentUICulture текущего потока для всех
        ///   подстановки ресурсов с помощью этого класса ресурсов со строгим типом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }

            set
            {
                resourceCulture = value;
            }
        }
    }
}
