using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Combat;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = 0f;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            //returns early, doesn't keep going if eval proves null
            if(target == null) return; 
            if(target.IsDead()) return; 

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
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
            transform.LookAt(target.transform);

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
            target.TakeDamage(weaponDamage);
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }


        public void Attack(CombatTarget combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }

        public bool CanAttack(CombatTarget combatTarget){
            if(combatTarget == null){ return false;}
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel(){
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

  
    }
}