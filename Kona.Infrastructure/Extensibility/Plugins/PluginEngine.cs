using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Web;

namespace Kona.Infrastructure {
    /// <summary>
    /// Central repository of plugins
    /// </summary>
    public class PluginEngine : IPluginEngine {
        private bool _initialized; // InitializePlugins needs to be reentrant
        private object _initializationLock = new object();
        private ReflectionPluginEngine _reflectionEngine = new ReflectionPluginEngine();
        private List<Plugin> _plugins = new List<Plugin>();


        public PluginEngine() {
            InitializePlugins();
        }

        /// <summary>
        /// initializes based on App_Code
        /// </summary>
        public void InitializePlugins() {
            
            var codeAssemblies = System.Web.Compilation.BuildManager.CodeAssemblies;
            var scriptLocalServerPath = HttpContext.Current.Server.MapPath("/App_Code");
            InitializePlugins(codeAssemblies, scriptLocalServerPath);
        }

        public void AddReflectionPlugin(Plugin plug) {
            _reflectionEngine.AddPlugin(plug);
        }

        /// <summary>
        /// Discovers and initializes the plug-ins.
        /// </summary>
        public void InitializePlugins(IList codeAssemblies, string scriptLocalServerPath) {
            if (!_initialized) {
                lock (_initializationLock) {
                    if (!_initialized) {
                        _plugins.AddRange(_reflectionEngine.InitializePlugins(codeAssemblies));

                        _initialized = true;
                    }
                }
            }
        }

        public Plugin[] Plugins {
            get { return _plugins.ToArray(); }
        }

        public TResult Execute<TResult>(string pluginName, string operation, params object[] input) {
            var func = _reflectionEngine.FindExecuteOperation<TResult>(pluginName, operation, input);
            return func();
        }

        public TResult ExecuteFirst<TResult>(string operation, params object[] input) {
            MethodInfo method = null;
            Plugin foundPlugin = null;
            TResult result = default(TResult);
            foreach (var plugin in _plugins) {
                method = plugin.GetType().GetMethods().FirstOrDefault(x => x.Name == operation);
                if (method != null) {
                    foundPlugin = plugin;
                    break;
                }
            }

            if (method != null) {
                result = (TResult)method.Invoke(foundPlugin, input);

            }
            //load the first

            return result;

        }

        /// <summary>
        /// Gets a method that calls all the plug-ins of the provided signature.
        /// </summary>
        /// <typeparam name="T">A delegate type describing the expected signature.</typeparam>
        /// <returns>A method that calls all the plug-ins with the signature of T.</returns>
        public T GetMethod<T>(string operation) where T : class {
            return _reflectionEngine.GetMethod<T>(operation);
        }

        /// <summary>
        /// Feeds the provided input through all plug-ins that have T [operation](string, T)
        /// as their signature, where [operation] is the contents of the operation parameter.
        /// </summary>
        /// <typeparam name="T">The type of the input to process.</typeparam>
        /// <param name="operation">The key of the operation to perform.</param>
        /// <param name="input">The object to process.</param>
        /// <returns>The processed object.</returns>
        public T Process<T>(string operation, T input) where T : class {

            return _reflectionEngine.Process<T>(operation, input);
            
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
        public bool IsTrue<T>(string operation, T input) {
            return _reflectionEngine.IsTrue<T>(operation, input);
        }

 
    }
}
