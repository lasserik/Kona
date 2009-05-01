using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace Kona.Infrastructure {
    public class Plugin {
        private static PluginEngine _engine = new PluginEngine();

        // Plugin settings
        private Dictionary<string, object> _settings = new Dictionary<string, object>();

        //settings
        public PluginSetting Settings { get; set; }
        public bool IsEnabled {
            get {
                bool result = false;
                //the plugin is enabled if
                //1) the settings have been filled in
                //2) and the user says it is
                if (this.Settings.IsEnabled) {
                    //make sure each setting is set
                    result = this.Settings.IsEnabled;

                    //TODO: Figure out a check for required values


                }

                return result;
            }

        }
        public string PluginName {
            get {
                return this.GetType().Name;
            }
        }
        public string Code {
            get {
                return FilePath.GetFileText();
            }

        }
        string _filePath = "";
        public string FilePath {
            get {
                if (string.IsNullOrEmpty(_filePath)) {
                    string directoryRoot = HttpContext.Current.Server.MapPath("~/App_Code");
                    _filePath = this.GetType().Name.LocateFilePath(directoryRoot);
                }
                return _filePath;
            }
            set {
                _filePath = value;
            }
        }


        public static void ValidateSetting(string pluginName, string settingName, object value) {
            var plugin = Plugins.Where(x => x.GetType().Name.Equals(pluginName, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
            if (plugin != null) {

                PropertyInfo prop = plugin.GetType().GetProperty(settingName);

                //see if it can be assigned this value
                if (prop != null) {
                    //set the value to see if it can be coerced
                    try {
                        value.ChangeType(prop.PropertyType);
                    }
                    catch {
                        throw new InvalidOperationException("Can't assign " + value.ToString() + " to " + prop.PropertyType.Name);
                    }
                }
                else {
                    throw new InvalidOperationException("No setting found to validate with that name");
                }

            }
            else {
                throw new InvalidOperationException("No plugin found to validate with that name");
            }
        }

        /// <summary>
        /// Optionally override this in a plug-in to add an initialization step.
        /// </summary>
        public virtual void Initialize() { }


        public object GetSetting(string name) {
            //see if this setting is presetn
            object result = null;
            if (this.Settings.Settings.ContainsKey(name))
                result = this.Settings.Settings[name];

            return result;
        }

        public override bool Equals(object obj) {
            if (obj is Plugin) {
                var compareTo = obj as Plugin;
                //compare by name
                return compareTo.GetType().Name.Equals(this.GetType().Name,StringComparison.InvariantCultureIgnoreCase);
            } else {
                return base.Equals(obj);
            }
        }

        #region Forwarders to PluginEngine

        public static void InitializePlugins() {
            _engine.InitializePlugins();
        }

        /// <summary>
        /// Discovers and initializes the plug-ins.
        /// </summary>
        public static void InitializePlugins(IList codeAssemblies, string scriptLocalServerPath) {
            _engine.InitializePlugins(codeAssemblies, scriptLocalServerPath);
        }

        /// <summary>
        /// The list of installed plug-ins.
        /// </summary>
        public static Plugin[] Plugins {
            get {
                return _engine.Plugins;
            }
        }

        public static TResult Execute<TResult>(string pluginName, string operation, params object[] input) {
            return _engine.Execute<TResult>(pluginName, operation, input);
        }

        public static TResult ExecuteFirst<TResult>(string operation, params object[] input) {
            return _engine.ExecuteFirst<TResult>(operation, input);
        }

        /// <summary>
        /// Gets a method that calls all the plug-ins of the provided signature.
        /// </summary>
        /// <typeparam name="T">A delegate type describing the expected signature.</typeparam>
        /// <returns>A method that calls all the plug-ins with the signature of T.</returns>
        public static T GetMethod<T>(string operation) where T : class {
            return _engine.GetMethod<T>(operation);
        }

        /// <summary>
        /// Feeds the provided input through all plug-ins that have T [operation](string, T)
        /// as their signature, where [operation] is the contents of the operation parameter.
        /// </summary>
        /// <typeparam name="T">The type of the input to process.</typeparam>
        /// <param name="operation">The key of the operation to perform.</param>
        /// <param name="input">The object to process.</param>
        /// <returns>The processed object.</returns>
        public static T Process<T>(string operation, T input) where T : class {
            return _engine.Process<T>(operation, input);
        }

        /// <summary>
        /// Puts the provided object through all plug-ins with signature
        /// bool Process(string, T). The return value is false if any of the
        /// plug-ins returned false, and true otherwise. This is the pattern
        /// to use for pluggable validation logic for example.
        /// </summary>
        /// <typeparam name="T">The type of the input to process.</typeparam>
        /// <param name="operation">The key of the operation to perform.</param>
        /// <param name="input">The object to process.</param>
        /// <returns>False if any of the plug-ins returned false.</returns>
        public static bool IsTrue<T>(string operation, T input) {
            return _engine.IsTrue<T>(operation, input);
        }
        #endregion
    }
}
