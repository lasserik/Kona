using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Kona.Infrastructure {
    /// <summary>
    /// Handles plugins based on invocation through Reflection
    /// </summary>
    public class ReflectionPluginEngine {
        private List<Plugin> _plugins = new List<Plugin>();
        private Hashtable _methods = Hashtable.Synchronized(new Hashtable());
        private Hashtable _cached = Hashtable.Synchronized(new Hashtable());

        public IList<Plugin> InitializePlugins(IList codeAssemblies) {
            if (codeAssemblies != null) {
                foreach (Assembly assembly in codeAssemblies) {
                    //TODO: Why do we skip these assemblies!?!?! They are in "AppCode"!
                    if (!assembly.FullName.StartsWith("System") & !assembly.FullName.StartsWith("Microsoft.VisualStudio")) {
                        try {
                            foreach (var type in assembly.GetTypes()) {
                                if (typeof(Plugin).IsAssignableFrom(type) && type != typeof(Plugin)) {
                                    var plugin = (Plugin)Activator.CreateInstance(type);
                                    plugin.Initialize();

                                    //default the settings until overridden
                                    plugin.Settings = new PluginSetting();

                                    _plugins.Add(plugin);
                                }
                            }
                        }
                        catch (ReflectionTypeLoadException e) {
                            throw new InvalidOperationException(e.LoaderExceptions[0].Message);
                        }
                    }
                }
            }
            return _plugins;
        }

        /// <summary>
        /// Adds a plugin to the current plugin queue,
        /// replacing any current instance
        /// </summary>
        /// <param name="plugin"></param>
        public void AddPlugin(Plugin plugin) {
            if (_plugins.Contains(plugin))
                _plugins.Remove(plugin);
            _plugins.Add(plugin);
        }

        /// <summary>
        /// Return a delegate to execute the plugin script operation. Return null if there is no plugin supporting
        /// this operation.
        /// </summary>
        public Func<TResult> FindExecuteOperation<TResult>(string pluginName, string operation, object[] input) {
            //TODO: Create an plugin instance per request...
            var plugin = _plugins
                .Where(x => x.GetType().Name.Equals(pluginName, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();

            if (plugin == null)
                return null;

            MethodInfo method = plugin.GetType().GetMethods().FirstOrDefault(x => x.Name == operation);

            if (method == null)
                return null;

            return () => (TResult)method.Invoke(plugin, input);
        }

        public T GetMethod<T>(string operation) where T : class {
            var operationMethods = _methods[operation] as Hashtable;
            var operationCached = _cached[operation] as Hashtable;
            if (operationMethods == null) {
                operationMethods = Hashtable.Synchronized(new Hashtable());
                _methods.Add(operation, operationMethods);
                operationCached = Hashtable.Synchronized(new Hashtable());
                _cached.Add(operation, operationCached);
            }
            var processDelegate = (Delegate)operationMethods[typeof(T)];
            if (processDelegate == null) {
                if (operationCached[typeof(T)] == null) {
                    foreach (var plugin in _plugins) {
                        var pluginType = plugin.GetType();
                        var processMethod = pluginType.GetMethod(operation,
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                        if (processMethod == null) continue;
                        var newDelegate = Delegate.CreateDelegate(typeof(T), plugin, processMethod, false);
                        if (newDelegate != null) {
                            if (processDelegate == null) {
                                processDelegate = newDelegate;
                            }
                            else {
                                processDelegate = Delegate.Combine(processDelegate, newDelegate);
                            }
                        }
                    }
                    operationMethods[typeof(T)] = processDelegate;
                    operationCached[typeof(T)] = true;
                }
            }
            return (T)(object)processDelegate;
        }

        public T Process<T>(string operation, T input) where T : class {
            var methods = GetMethod<Func<T, T>>(operation);
            if (methods != null) {
                foreach (Func<T, T> pluginMethod in methods.GetInvocationList()) {
                    if (input == null) return null;
                    input = pluginMethod(input);
                }
            }
            return input;
        }

        public bool IsTrue<T>(string operation, T input) {
            Delegate methods = GetMethod<Func<T, bool>>(operation);
            if (methods == null) return true;
            foreach (var pluginMethod in methods.GetInvocationList()) {
                if (!(bool)pluginMethod.DynamicInvoke(input)) {
                    return false;
                }
            }
            return true;
        }
    }
}
