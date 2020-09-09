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
        private GameObject target;
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
            controller = new ServiceCoroutineExtension.
                CoroutineController();

            target = targetGameObject;
            
            initialized = true;
        }

        public bool Execute()
        {
            if (controller.state == ServiceCoroutineExtension.CoroutineState.Ready)
            {
                controller.Start(executable(target));
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