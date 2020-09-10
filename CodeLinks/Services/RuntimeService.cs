using System;
using System.Collections;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Services
{
    public class RuntimeService
    {
        private readonly CoroutineController controller;
        private readonly MonoBehaviour targetCtx;
        public readonly Func<IEnumerator> executable;
        public RuntimeService(MethodInfo targetMethod, GameObject targetGameObject)
        {
            Type declType = targetMethod.DeclaringType;
            if (!targetGameObject.TryGetComponent(declType, out var component))
            {
                Debug.LogError("Could not bind function to game object: " + targetGameObject.name + 
                                    "using method: " + targetMethod.GetFullName());
            }

            executable = ServiceCreator.CreateServiceFunction(targetMethod, component);
            controller = new CoroutineController();
            targetCtx = component as MonoBehaviour;
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