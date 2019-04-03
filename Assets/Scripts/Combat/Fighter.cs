using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;
        float timeSinceLastAttack = 0f;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

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
        /*
            Testing comment
        */ 
        {
            if(timeSinceLastAttack > timeBetweenAttacks){
                //this will trigger event Hit()
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }


        void Hit()
        /*
            Animation Event
         */
        {
            Health healtComponent = target.GetComponent<Health>();
            healtComponent.TakeDamage(weaponDamage);
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

  
    }
}