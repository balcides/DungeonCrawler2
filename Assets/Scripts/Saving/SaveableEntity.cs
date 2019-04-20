using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour{

        [SerializeField] string uniqueIdentifier = "";

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

        private void Update() {
            if(Application.IsPlaying(gameObject)) return;
            print("Editing");
        }
    }
}