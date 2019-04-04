using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;

        Fighter fighter;
        GameObject player;
        Health health;
        Vector3 guardPosition;
        Mover mover;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start(){

            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            guardPosition = transform.position;
        }


        void Update()
        {
            if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                //Suspicion state
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }


        private void GuardBehavior()
        {
            //moves AI back to position when chase fails or out of range.
            mover.StartMoveAction(guardPosition);
        }


        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }


        private void AttackBehavior()
        {
            fighter.Attack(player);
        }


        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }


        //Called by Unity
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}