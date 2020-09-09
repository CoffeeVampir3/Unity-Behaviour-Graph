using System;
using UnityEngine;

namespace BehaviourGraph.Debugging
{
    public class PlayerController : MonoBehaviour
    {
        //Example code only =D
        public static PlayerController instance;

        public void Awake()
        {
            instance = this;
        }

        public void Update()
        {
            var whores = Input.GetAxis("Horizontal");
            var verts = Input.GetAxis("Vertical");

            var transform1 = transform;
            
            var p = transform1.localPosition;
            p += new Vector3(whores, verts);

            transform1.localPosition = p;
        }
    }
}