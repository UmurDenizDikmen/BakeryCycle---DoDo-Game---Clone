using UnityEngine;

namespace PathCreation.Examples
{
  
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed;
        float distanceTravelled;
        Animator animator;

        void Start() {
            if (pathCreator != null)
            {
                
                pathCreator.pathUpdated += OnPathChanged;
            }
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (pathCreator != null)
            {
                if (Input.GetMouseButton(0))
                {
                    animator.SetBool("isWalking", true);
                    speed = 2f;
                    distanceTravelled += speed * Time.deltaTime;
                }
                else
                {
                    animator.SetBool("isWalking", false);
                    speed = 0f;
                    distanceTravelled += speed * Time.deltaTime;
                }
               
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
           
        }

    
        void OnPathChanged() 
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}