using System;
using System.Collections.Generic;
namespace Kona.Infrastructure {
    public interface IPluginEngine {
        TResult Execute<TResult>(string pluginName, string operation, params object[] input);
        TResult ExecuteFirst<TResult>(string operation, params object[] input);
        T GetMethod<T>(string operation) where T : class;
        void InitializePlugins();
        void InitializePlugins(System.Collections.IList codeAssemblies, string scriptLocalServerPath);
        bool IsTrue<T>(string operation, T input);
        Kona.Infrastructure.Plugin[] Plugins { get; }
        T Process<T>(string operation, T input) where T : class;
    }
}
