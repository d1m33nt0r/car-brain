using System;
using System.Collections.Generic;

namespace NeuralNet.Editor.Services
{ 
    public class ServiceLocator
    {
        private static ServiceLocator locator = null;

        public static ServiceLocator Instance
        {
            get
            {
                if (locator == null)
                {
                    locator = new ServiceLocator();
                }
                return locator;
            }
        }
        
        private Dictionary<Type, object> registry = new Dictionary<Type, object>();
        
        private ServiceLocator()
        {
        }

        public void Register<T>(T serviceInstance)
        {
            registry[typeof(T)] = serviceInstance;
        }

        public T GetService<T>()
        {
            T serviceInstance = (T)registry[typeof(T)];
            return serviceInstance;
        }
    }
}