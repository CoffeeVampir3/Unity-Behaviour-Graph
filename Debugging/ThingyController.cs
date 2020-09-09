using System;
using System.Collections;
using BehaviourGraph.Services;
using Coffee.BehaviourTree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BehaviourGraph.Debugging
{
    public class ThingyController : MonoBehaviour
    {
        [SerializeField]
        public Coffee.Behaviour.BehaviourGraph graph;
        private BehaviourTree tree;

        private void Awake()
        {
            tree = graph.GenerateBehaviourTree(gameObject);
        }

        public void Update()
        {
            tree.Tick();
        }

        private Vector2 GetPointInRadius(float radiusSize)
        {
            Vector2 pointOffset = Random.insideUnitCircle * radiusSize;
            Vector2 position = transform.localPosition;
            return position + pointOffset;
        }

        private Vector2 originalPosition;
        private Vector2 waypoint;
        private bool moving = false;
        [SerializeField] 
        private float randomRadiusSize = .5f;
        [SerializeField] 
        private float speed = .01f;
        private float totalTraversed = 0f;
        
        [Service]
        public IEnumerator MoveToRandomPoint(GameObject thisObject)
        {
            if (!moving)
            {
                moving = true;
                totalTraversed = 0.0f;
                originalPosition = transform.localPosition;
                waypoint = GetPointInRadius(randomRadiusSize);
            }

            while (totalTraversed < 1.0f)
            {
                totalTraversed += speed * Time.deltaTime;
                transform.localPosition = Vector2.Lerp(originalPosition, waypoint, totalTraversed);
                yield return new WaitForEndOfFrame();
            }

            moving = false;
            originalPosition = transform.position;
        }
    }
}