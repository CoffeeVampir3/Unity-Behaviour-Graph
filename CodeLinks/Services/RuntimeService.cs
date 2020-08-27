using System;
using System.Collections;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Services
{
    public class RuntimeService
    {
        public Func<GameObject, IEnumerator> executable;

        private bool initialized = false;
        public void Initialize(MethodInfo targetMethod, GameObject targetGameObject)
        {
            if (initialized && executable != null)
                return;
            
            Type t = targetMethod.DeclaringType;

            Component component;
            if (!targetGameObject.TryGetComponent(t, out component))
            {
                throw new Exception("Could not bind function to game object: " + targetGameObject.name + 
                                    "using method: " + targetMethod.GetFullName());
            }

            executable = ServiceCreator.CreateServiceFunction(targetMethod, component);
            initialized = true;
        }
    }
}