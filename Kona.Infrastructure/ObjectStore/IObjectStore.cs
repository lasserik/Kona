using System;
using System.Collections.Generic;
namespace Kona.Infrastructure {
    public interface IObjectStore {
        void Delete(string key);
        void Delete(string key, string searchString);
        T Get<T>(string key) where T : class, new();
        T Get<T>(string key, string searchString) where T : class, new();
        IList<T> GetList<T>(string key) where T : class, new();
        void Store<T>(string key, string searchString, T value) where T : class, new();
        void Store<T>(string key, T value) where T : class, new();
    }
}
