using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;

        private void Start(){
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float time)
        {
            //alpha is not 1
            while(canvasGroup.alpha < 1){

                //moving alpha towards 1
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        //TODO: consolidate this with FadeOut BUT it may be nice and readable to have this.
        public IEnumerator FadeIn(float time)
        {
            //alpha is not 1
            while (canvasGroup.alpha > 0)
            {

                //moving alpha towards 1
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}

