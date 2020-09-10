using System.Collections;
using BehaviourGraph.Attributes;
using Coffee.BehaviourTree;
using UnityEngine;
using UnityEngine.UI;
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
            speed = Random.Range(.5f*speed, speed);
            randomRadiusSize = Random.Range(.5f*randomRadiusSize, randomRadiusSize);
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
        private float speed = 1f;
        private float totalTraversed = 0f;
        [SerializeField] 
        private Image img;
        
        WaitForEndOfFrame cachedWait = new WaitForEndOfFrame();

        [Service]
        public IEnumerator MoveToRandomPoint()
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
                yield return cachedWait;
            }

            moving = false;
            originalPosition = transform.localPosition;
        }

        public float DistanceToPlayer()
        {
            return 
                Vector3.Distance(playerTransform.localPosition, transform.localPosition);
        }
        
        //Good coding practice kappa
        [SerializeField]
        private Transform playerTransform = null;
        [SerializeField] 
        private float chaseSpeed = 1;
        [SerializeField] 
        private float smellRadius = 50;
        
        [Service]
        public IEnumerator ChasePlayer()
        {
            img.color = Color.red;
            while (DistanceToPlayer() < smellRadius)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition,
                    playerTransform.localPosition, chaseSpeed);
                yield return cachedWait;
            }
            img.color = Color.blue;
        }

        [Condition]
        public bool IsPlayerNearby()
        {
            Debug.Log(name);
            return DistanceToPlayer() < smellRadius;
        }
    }
}