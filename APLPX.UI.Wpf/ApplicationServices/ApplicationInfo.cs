using System.Reflection;
using System.IO;
using System;

namespace APLPX.UI.WPF.ApplicationServices
{
    /// <summary>
    /// Provides information about the running application.
    /// </summary>
    /// <remarks>Adapted from WPF Application Framework: https://waf.codeplex.com/ /// </remarks>
    public static class ApplicationInfo
    {
        private static readonly Lazy<string> _productName = new Lazy<string>(GetProductName);
        private static readonly Lazy<string> _version = new Lazy<string>(GetVersion);
        private static readonly Lazy<string> _company = new Lazy<string>(GetCompany);
        private static readonly Lazy<string> _copyright = new Lazy<string>(GetCopyright);
        private static readonly Lazy<string> _applicationPath = new Lazy<string>(GetApplicationPath);

        /// <summary>
        /// Gets the product name of the application.
        /// </summary>
        public static string ProductName
        {
            get { return _productName.Value; }
        }

        /// <summary>
        /// Gets the version number of the application.
        /// </summary>
        public static string Version
        {
            get { return _version.Value; }
        }

        /// <summary>
        /// Gets the company of the application.
        /// </summary>
        public static string Company
        {
            get { return _company.Value; }
        }

        /// <summary>
        /// Gets the copyright information of the application.
        /// </summary>
        public static string Copyright
        {
            get { return _copyright.Value; }
        }

        /// <summary>
        /// Gets the path for the executable file that started the application, not including the executable name.
        /// </summary>
        public static string ApplicationPath
        {
            get { return _applicationPath.Value; }
        }


        private static string GetProductName()
        {
            string result = "";

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var attribute = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyProductAttribute)));
                result = (attribute != null) ? attribute.Product : "";
            }
            return result;
        }

        private static string GetVersion()
        {
            string result = "";

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                result = entryAssembly.GetName().Version.ToString();
            }
            return result;
        }

        private static string GetCompany()
        {
            string result = "";

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var attribute = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCompanyAttribute)));

                result = (attribute != null) ? attribute.Company : "";
            }
            return result;
        }

        private static string GetCopyright()
        {
            string result = "";

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var attribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCopyrightAttribute));

                result = attribute != null ? attribute.Copyright : "";
            }
            return result;
        }

        private static string GetApplicationPath()
        {
            string result = "";

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                result = Path.GetDirectoryName(entryAssembly.Location);
            }
            return result;
        }
    }
}
