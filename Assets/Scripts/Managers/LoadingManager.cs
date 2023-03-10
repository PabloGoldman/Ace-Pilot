using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public string sceneToLoad;

    public Slider loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            yield return null;
        }
    }
}
    