using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private string previousScene;

    void Awake() {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public static SceneLoader GetOrCreate() {
        if (Instance == null) {
            var go = new GameObject("SceneLoader");
            Instance = go.AddComponent<SceneLoader>();
            DontDestroyOnLoad(go);
        }
        return Instance;
    }

    [YarnCommand("load_scene")]
    public static void LoadSceneCommand(string sceneName) {
        var loader = GetOrCreate();
        loader.previousScene = SceneManager.GetActiveScene().name;
        loader.StartCoroutine(loader.TransitionAndLoad(sceneName));
    }

    [YarnCommand("return_scene")]
    public static void ReturnScene() {
        if (Instance.previousScene != null)
            Instance.StartCoroutine(Instance.TransitionAndLoad(Instance.previousScene));
    }

    IEnumerator TransitionAndLoad(string sceneName) {
        // fade to black here later
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(sceneName);
    }
}
