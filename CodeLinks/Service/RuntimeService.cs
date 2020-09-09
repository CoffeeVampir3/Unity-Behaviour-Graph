using System;
using System.Collections;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Services
{
    public class RuntimeService
    {
        private CoroutineController controller;
        private GameObject target;
        private MonoBehaviour targetCtx;
        public Func<IEnumerator> executable;

        private bool initialized = false;
        public void Initialize(MethodInfo targetMethod, GameObject targetGameObject)
        {
            if (initialized && executable != null)
                return;
            
            Type t = targetMethod.DeclaringType;

            Component component;
            if (!targetGameObject.TryGetComponent(t, out component))
            {
                Debug.LogError("Could not bind function to game object: " + targetGameObject.name + 
                                    "using method: " + targetMethod.GetFullName());
            }

            executable = ServiceCreator.CreateServiceFunction(targetMethod, component);
            controller = new CoroutineController();

            target = targetGameObject;
            targetCtx = component as MonoBehaviour;
            initialized = true;
        }

        public bool Execute()
        {
            if (controller.state == CoroutineController.CoroutineState.Ready)
            {
                controller.Start(targetCtx, executable());
            }
            if (controller.state == CoroutineController.CoroutineState.Running)
            {
                return true;
            }
            
            controller.Finish();
            return false;
        }
    }
}