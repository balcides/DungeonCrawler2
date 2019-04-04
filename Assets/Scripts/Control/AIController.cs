using RPG.Combat;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        GameObject player;
        Health health;

        private void Start(){

            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }


        void Update()
        {
            if(InAttackRangeOfPlayer() && fighter.CanAttack(player)){

                fighter.Attack(player);
            }else{
                fighter.Cancel();
            }
        }


        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}