using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;


        NavMeshAgent navMeshAgent;
        Health health;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }


        // Update is called once per frame
        void Update()
        {
            //Note: this is an interesting approach to embed an evaulation on update for Navmesh Agent. It's not a standard practice but it's nifty. 
            //Consistency is still key.
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();

        }


        public void StartMoveAction(Vector3 destination, float speedFraction){

            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }


        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }


        public void Cancel(){
            navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}