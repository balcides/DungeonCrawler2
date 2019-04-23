using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace RPG.Utilities
{
    public class Manifest : MonoBehaviour
    {
        static string DirRead = "/Volumes/MonarchGameDrive/Monarch/DungeonCrawler2/Assets/";
        static string DirSave = "/Volumes/MonarchGameDrive/Monarch/DungeonCrawler2/Assets/";
        static string SaveFileName = "Asset_Manifest.txt";

        [MenuItem("Tools/Print Assets Manifest")]
        private static void GetAllFiles(){

            print("Running Manifest Print...");
            List<string> testList = new List<string>();
            testList = DirSearch(DirRead);

            foreach(string item in testList){
                print(item);
            }
        }

        [MenuItem("Tools/Save Assets Manifest")]
        private static void SaveManifest(){

            List<string> testList = new List<string>();
            testList = DirSearch(DirSave);
            SaveFile(testList, DirSave, SaveFileName);
        }

        public static List<string> DirSearch(string sDir){
        /*
            Searches directory and outputs list

            sDir: Directory to read from
            return: List of files and subdirectories
         */
            var dirList = new List<string>();

            try{
                foreach (string d in Directory.GetDirectories(sDir)){
                    foreach (string f in Directory.GetFiles(d)){
                        dirList.Add(f);
                    }
                    dirList.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt){
                print(excpt.Message);
            }
            return dirList;
        }

        public static void SaveFile(List<string> input, string dir, string textFileName){
            /*
                Simple save list to text file and directory

                input: List of string.
                dir: Directory to save to.
                textFileName: Name of file to save. Extension not included, please add.
             */
            print("saving file..." + dir + textFileName);
            using (TextWriter tw = new StreamWriter(dir + textFileName)){

                foreach (string s in input)
                    tw.WriteLine(s);
            }

        }


    }


}