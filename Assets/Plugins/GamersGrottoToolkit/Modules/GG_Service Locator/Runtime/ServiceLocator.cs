using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamersGrotto.GG_Service_Locator
{
    /// <summary>
    /// It is recommended to have a Bootstrap class that initializes all the services and registers them with the ServiceLocator.
    /// This bootstrapped class should also have the [DefaultExecutionOrder(-1)] attribute to ensure it runs before any other script.
    /// 
    /// </summary>
    public static class ServiceLocator
    {
        static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service) where T: class
        {
            services[typeof(T)] = service;
        }
        public static T Get<T>() where T: class
        {
            if (services.TryGetValue(typeof(T), out object service)) {
                return service as T;
            }
            
            throw new Exception( $"Service of type {typeof(T)} not found");
        }
    
        public static void Unregister<T>() where T: class
        {
            services.Remove(typeof(T));
        }
      
    }
}
