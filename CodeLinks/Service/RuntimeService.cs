using System;
using System.Collections;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Services
{
    public class RuntimeService
    {
        private ServiceCoroutineExtension.CoroutineController controller;
        private Func<GameObject, IEnumerator> executable;

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
            controller = new ServiceCoroutineExtension.
                CoroutineController(executable(targetGameObject));
            
            initialized = true;
        }

        public bool Execute()
        {
            if (controller.state == ServiceCoroutineExtension.CoroutineState.Ready)
            {
                controller.Start();
            }
            if (controller.state == ServiceCoroutineExtension.CoroutineState.Running)
            {
                return true;
            }
            
            controller.Finish();
            return false;
        }
    }
}