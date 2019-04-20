
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            //'using' statement makes it easier so we don't need to add stream.Close() when done for memory h
            using (FileStream stream = File.Open(path, FileMode.Create)){

                //good example of format data for save/serializing
                //byte[] bytes = Encoding.UTF8.GetBytes("!Hola Mundo!");
  
                //Serializing takes our data and turns it into binary automatically
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }


        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }


        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            //state["helloq"]  = 4;
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()){

                state[saveable.GetUniqueIdentifer()] = saveable.CaptureState();
            }
            return state;
        }


        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.RestoreState(stateDict[saveable.GetUniqueIdentifer()]);
            }
        }


        private string GetPathFromSaveFile(string saveFile)
        {

            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }

}
