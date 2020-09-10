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
            speed = Random.Range(.5f*speed, 1.5f*speed);
            chaseSpeed = Random.Range(.5f*chaseSpeed, 1.5f*chaseSpeed);

            randomRadiusSize = Random.Range(.5f*randomRadiusSize, randomRadiusSize);
            smellRadius = Random.Range(.75f*smellRadius, 1.25f*smellRadius);
            chaseRadius = Random.Range(.5f*chaseRadius, chaseRadius);
            currentRadius = smellRadius;
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
            currentRadius = smellRadius;
            waypoint = GetPointInRadius(randomRadiusSize);
            yield return null;
        }

        [Service]
        public IEnumerator MoveToRandomPoint()
        {
            while (!IsPlayerNearby() && Vector2.Distance(waypoint, transform.localPosition) > .1f)
            {
                transform.localPosition = Vector2.MoveTowards(
                    transform.localPosition, waypoint, 
                    speed * Time.deltaTime);
                
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

        private float currentRadius;
        [Service]
        public IEnumerator ChasePlayer()
        {
            img.color = Color.red;
            currentRadius = chaseRadius;
            while (IsPlayerNearby())
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition,
                    playerTransform.localPosition, chaseSpeed * Time.deltaTime);
                yield return cachedWait;
            }
            img.color = Color.blue;
        }

        [Condition]
        public bool IsPlayerNearby()
        {
            return DistanceToPlayer() < currentRadius;
        }
    }
}