using UnityEngine;

namespace RPG.Saving
{
    public class SaveableEntity : MonoBehaviour{

        public string GetUniqueIdentifer(){
            return "";
        }

        public object CaptureState(){

            print("Capturing state for " + GetUniqueIdentifer());
            return null;
        }

        public void RestoreState(object state){

            print("Restoring state for " + GetUniqueIdentifer());
        }
    }
}