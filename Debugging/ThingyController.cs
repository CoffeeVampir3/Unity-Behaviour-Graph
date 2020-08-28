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
            randomRadiusSize = Random.Range(0, randomRadiusSize);
            speed = Random.Range(0, speed);
        }

        public void Update()
        {
            tree.Tick();
        }

        private Vector2 GetPointInRadius(float radiusSize)
        {
            Vector2 pointOffset = Random.insideUnitCircle * radiusSize;
            Vector2 position = transform.position;
            return position + pointOffset;
        }

        private Vector2 originalPosition;
        private Vector2 waypoint;
        private bool moving = false;
        [SerializeField] 
        private float randomRadiusSize = .5f;
        [SerializeField] 
        private float speed = 50f;
        private float totalTraversed = 0f;
        
        [Service]
        public IEnumerator MoveToRandomPoint(GameObject thisObject)
        {
            if (!moving)
            {
                moving = true;
                totalTraversed = 0.0f;
                originalPosition = transform.position;
                waypoint = GetPointInRadius(randomRadiusSize);
            }
            
            while (totalTraversed < 1.0f)
            {
                totalTraversed += speed * Time.deltaTime;
                transform.position = Vector2.Lerp(originalPosition, waypoint, totalTraversed);
                yield return new WaitForEndOfFrame();
            }

            moving = false;
            originalPosition = thisObject.transform.position;
            yield return null;
        }
    }
}