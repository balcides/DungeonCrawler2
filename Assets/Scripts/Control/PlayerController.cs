using System;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        void Start()
        {
            health = GetComponent<Health>();
        }

        void Update(){
            if (health.IsDead()) return;
            if(InteractWithCombat()) return; //on fail, skips anything after on update
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat(){
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits){

                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject)){
                    continue; //keep going in for loop, go to the next thing
                }

                if(Input.GetMouseButton(0)){
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }


        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0)){
                    //TODO: Refactor 1f when ready
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}