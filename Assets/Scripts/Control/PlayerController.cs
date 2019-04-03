using System;
using RPG.Movement;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        
        void Update(){
            if(InteractWithCombat()) return; //on fail, skips anything after on update
            if(InteractWithMovement()) return;
        }


        private bool InteractWithCombat(){
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits){

                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(!GetComponent<Fighter>().CanAttack(target)){
                    continue; //keep going in for loop, go to the next thing
                }

                if(Input.GetMouseButtonDown(0)){
                    GetComponent<Fighter>().Attack(target);
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
                    GetComponent<Mover>().StartMoveAction(hit.point);
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