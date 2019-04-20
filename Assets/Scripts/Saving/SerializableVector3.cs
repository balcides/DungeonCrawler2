using UnityEngine;
//Unity's transform values aren't serializable to convert over to binary for save, 
//instead we make our own data format for vector 3 that is such. Crazy, I know. :/

namespace RPG.Saving
{
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;

        public SerializableVector3(Vector3 vector){
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector(){

            return new Vector3(x, y, z);
        }
    }
}
