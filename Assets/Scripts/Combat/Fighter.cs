using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;


        private void Update()
        {
            //returns early, doesn't keep going if eval proves null
            if(target == null) return; 

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }


        private void AttackBehavior()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }


        public void Attack(CombatTarget combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;

        }


        public void Cancel(){
            target = null;
        }

        //Animation Event
        void Hit(){
            

        }
    }
}