using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<string> inventory = new List<string>();
    public Dictionary<string, bool> triggered = new Dictionary<string, bool>();

    void Awake() {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public static GameManager GetOrCreate() {
        if (Instance == null) {
            var go = new GameObject("GameManager");
            Instance = go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
        return Instance;
    }

    public void AddItem(string item) { if (!inventory.Contains(item)) inventory.Add(item); }
    public bool HasItem(string item) { return inventory.Contains(item); }
    public void SetTriggered(string key) { triggered[key] = true; }
    public bool IsTriggered(string key) { return triggered.ContainsKey(key) && triggered[key]; }

    [YarnCommand("set_flag")]
    public static void SetFlag(string key) { GetOrCreate().SetTriggered(key); RefreshAll(); }

    [YarnCommand("unset_flag")]
    public static void UnsetFlag(string key) { GetOrCreate().triggered.Remove(key); RefreshAll(); }

    public static void RefreshAll()
    {
        foreach (var os in FindObjectsByType<ObjectState>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            os.Refresh();
        foreach (var fa in FindObjectsByType<FlagAnimator>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            fa.Refresh();
    }

    [YarnCommand("start_timer")]
    public static void StartTimer(string flag, float duration)
    {
        var gm = GetOrCreate();
        gm.SetTriggered(flag);
        RefreshAll();
        gm.StartCoroutine(gm.TimerCoroutine(flag, duration));
    }

    IEnumerator TimerCoroutine(string flag, float duration)
    {
        yield return new WaitForSeconds(duration);
        triggered.Remove(flag);
        RefreshAll();
    }

    [YarnCommand("add_item")]
    public static void AddItemCommand(string item) { GetOrCreate().AddItem(item); }

    [YarnFunction("has_flag")]
    public static bool HasFlag(string key) { return GetOrCreate().IsTriggered(key); }

    [YarnFunction("has_item")]
    public static bool HasItemFunction(string item) { return GetOrCreate().HasItem(item); }

    public void Restart() {
        inventory.Clear();
        triggered.Clear();
        SceneManager.LoadScene(0);
    }
}