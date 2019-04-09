using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject PersistentObjectPrefab;
        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;

        }

        //this is the solution to using singletons which otherwise is too much responsibilty for one class 
        //and you don't want a chain effect of negative consequences as a result
        private void SpawnPersistentObjects()
        {
           GameObject persistentObject = Instantiate(PersistentObjectPrefab);
           DontDestroyOnLoad(persistentObject);
        }
    }
}
