using System;
using System.IO;
using System.Reflection;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for an About box.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        private static readonly string _companyUrl = "http://www.advancedpricinglogic.com/";

        public AboutViewModel()
        {
            LaunchWebPageCommand = ReactiveCommand.Create();
            LaunchWebPageCommand.Subscribe(x => LaunchWebPageExecuted(x));
        }

        public ReactiveCommand<object> LaunchWebPageCommand { get; private set; }

        public string Version
        {
            get
            {
                string result = String.Empty;

                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                if (version != null)
                {
                    result = version.ToString();
                }

                return result;
            }
        }

        public string Title
        {
            get
            {
                string result = CalculatePropertyValue<AssemblyTitleAttribute>("Title");

                if (string.IsNullOrEmpty(result))
                {
                    result = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                }

                return result;
            }
        }

        public string Description
        {
            get
            {
                return CalculatePropertyValue<AssemblyDescriptionAttribute>("Description");
            }
        }

        public string Product
        {
            get
            {
                return CalculatePropertyValue<AssemblyProductAttribute>("Product");
            }
        }

        public string Copyright
        {
            get
            {
                return CalculatePropertyValue<AssemblyCopyrightAttribute>("Copyright");
            }
        }

        public string Company
        {
            get
            {
                return CalculatePropertyValue<AssemblyCompanyAttribute>("Company");
            }
        }

        public string CompanyUrl
        {
            get { return _companyUrl; }
        }

        private void LaunchWebPageExecuted(object parameter)
        {
            string url = Convert.ToString(parameter);

            if (!String.IsNullOrWhiteSpace(url))
            {
                System.Diagnostics.Process.Start(url);
            }
        }

        /// <summary>
        /// Gets the specified property value either from the specifed attribute
        /// </summary>
        /// <typeparam name="T">Attribute type that we're trying to retrieve.</typeparam>
        /// <param name="propertyName">Property name to use on the attribute.</param>        
        /// <returns>The resulting string to use for a property.
        /// Returns null if no data could be retrieved.</returns>
        private string CalculatePropertyValue<T>(string propertyName)
        {
            string result = String.Empty;

            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                T attrib = (T)attributes[0];
                PropertyInfo property = attrib.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    result = property.GetValue(attributes[0], null) as string;
                }
            }

            return result;
        }
    }
}
