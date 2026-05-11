using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private string previousScene;
    private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

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

    void EnsureFadeCanvas()
    {
        if (fadeImage != null) return;

        var canvasGO = new GameObject("FadeCanvas");
        DontDestroyOnLoad(canvasGO);
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        var imageGO = new GameObject("FadeImage");
        imageGO.transform.SetParent(canvasGO.transform, false);
        fadeImage = imageGO.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = false;
        var rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    [YarnCommand("load_scene")]
    public static void LoadSceneCommand(string sceneName) {
        var loader = GetOrCreate();
        loader.previousScene = SceneManager.GetActiveScene().name;
        loader.StartCoroutine(loader.TransitionAndLoad(sceneName, false));
    }

    [YarnCommand("load_scene_fade")]
    public static void LoadSceneFadeCommand(string sceneName) {
        var loader = GetOrCreate();
        loader.previousScene = SceneManager.GetActiveScene().name;
        loader.StartCoroutine(loader.TransitionAndLoad(sceneName, true));
    }

    [YarnCommand("return_scene")]
    public static void ReturnScene() {
        if (Instance.previousScene != null)
            Instance.StartCoroutine(Instance.TransitionAndLoad(Instance.previousScene, false));
    }

    [YarnCommand("return_scene_fade")]
    public static void ReturnSceneFade() {
        if (Instance.previousScene != null)
            Instance.StartCoroutine(Instance.TransitionAndLoad(Instance.previousScene, true));
    }

    IEnumerator TransitionAndLoad(string sceneName, bool fade) {
        if (fade) { EnsureFadeCanvas(); yield return StartCoroutine(Fade(0f, 1f)); }
        SceneManager.LoadScene(sceneName);
        if (fade) yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        fadeImage.color = new Color(0, 0, 0, from);
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(from, to, elapsed / fadeDuration));
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, to);
    }
}
