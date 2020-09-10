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
        
        private Vector2 waypoint;
        [SerializeField] 
        private float randomRadiusSize = .5f;
        [SerializeField] 
        private float speed = 1f;
        [SerializeField] 
        private Image img = null;
        
        WaitForEndOfFrame cachedWait = new WaitForEndOfFrame();

        [Service]
        public IEnumerator FindWaypoint()
        {
            waypoint = GetPointInRadius(randomRadiusSize);
            yield return null;
        }

        [Service]
        public IEnumerator MoveToRandomPoint()
        {
            while (!IsPlayerNearby() && Vector2.Distance(waypoint, transform.localPosition) > .1f)
            {
                transform.localPosition = Vector2.MoveTowards(
                    transform.localPosition, waypoint, speed);
                yield return cachedWait;
            }
        }

        public float DistanceToPlayer()
        {
            return 
                Vector2.Distance(playerTransform.localPosition, transform.localPosition);
        }
        
        //Good coding practice kappa
        [SerializeField]
        private Transform playerTransform = null;
        [SerializeField] 
        private float chaseSpeed = 1;
        [SerializeField] 
        private float smellRadius = 50;
        [SerializeField] 
        private float chaseRadius = 150;
        
        [Service]
        public IEnumerator ChasePlayer()
        {
            img.color = Color.red;
            while (DistanceToPlayer() < chaseRadius)
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
            return DistanceToPlayer() < smellRadius;
        }
    }
}