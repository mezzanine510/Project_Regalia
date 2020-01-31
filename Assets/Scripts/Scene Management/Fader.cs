using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour {
        CanvasGroup canvasGroup;
        float alpha;

        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        private void Start() {
            StartCoroutine(FadeOutIn());
            // StartCoroutine(FadeOut(2f));
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(2f);
            print("Faded out.");
            yield return FadeIn(2f);
            print("Faded in.");
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}