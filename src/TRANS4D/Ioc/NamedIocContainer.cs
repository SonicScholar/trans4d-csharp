using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ioc
{
    public class NamedIocContainer
    {
        private static readonly Lazy<NamedIocContainer> _instance = new Lazy<NamedIocContainer>(() => new NamedIocContainer());
        public static NamedIocContainer Instance => _instance.Value;

        protected NamedIocContainer() { }

        private readonly ConcurrentDictionary<string, object> _registrations = new ConcurrentDictionary<string, object>();

        public void Register<T>(string name, T instance)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            _registrations[name] = instance;
        }

        public T Get<T>(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (_registrations.TryGetValue(name, out var obj) && obj is T t)
                return t;
            throw new KeyNotFoundException($"No registration for '{name}' of type {typeof(T).FullName}.");
        }

        public bool IsRegistered(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return _registrations.ContainsKey(name);
        }
    }
}
