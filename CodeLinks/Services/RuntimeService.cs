using System;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Services
{
    public class RuntimeService
    {
        private readonly Func<ServiceState> executable;
        public RuntimeService(MethodInfo targetMethod, GameObject targetGameObject)
        {
            Type declType = targetMethod.DeclaringType;
            if (!targetGameObject.TryGetComponent(declType, out var component))
            {
                Debug.LogError("Could not bind function to game object: " + targetGameObject.name + 
                                    "using method: " + targetMethod.GetFullName());
            }

            executable = ServiceCreator.CreateServiceFunction(targetMethod, component);
        }

        public bool Execute()
        {
            return executable() == ServiceState.Running;
        }
    }
}