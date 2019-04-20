using UnityEditor;
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
            if(string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");    //needs to be done this way to work with Unity's framework

            if(property.stringValue == ""){

                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties(); //tell Unity you've made that change if the value is empty for making a UUID
            }
        }
    }
}