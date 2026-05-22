using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[System.Serializable]
public class NamedClip
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<NamedClip> globalClips = new();
    private Dictionary<string, NamedClip> clips = new();
    private Dictionary<string, AudioSource> loopingSources = new();
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            RegisterClips(globalClips);
        }
        else
        {
            Instance.RegisterClips(globalClips);
            Destroy(this);
        }
    }

    public static AudioManager GetOrCreate()
    {
        if (Instance == null)
        {
            var go = new GameObject("AudioManager");
            Instance = go.AddComponent<AudioManager>();
            DontDestroyOnLoad(go);
        }
        return Instance;
    }

    public void RegisterClips(List<NamedClip> namedClips)
    {
        foreach (var nc in namedClips)
            if (nc.clip != null) clips[nc.name] = nc;
    }

    NamedClip Get(string name)
    {
        clips.TryGetValue(name, out var nc);
        if (nc == null) Debug.LogWarning($"AudioManager: clip '{name}' not found");
        return nc;
    }

    public static void PlaySound(string name, float pitch = 1f, float volumeScale = 1f)
    {
        var am = GetOrCreate();
        var nc = am.Get(name);
        if (nc == null) return;

        if (nc.loop)
        {
            if (am.loopingSources.ContainsKey(name)) return;
            var src = am.gameObject.AddComponent<AudioSource>();
            src.clip = nc.clip;
            src.volume = nc.volume;
            src.loop = true;
            src.Play();
            am.loopingSources[name] = src;
        }
        else
        {
            am.sfxSource.pitch = pitch;
            am.sfxSource.PlayOneShot(nc.clip, nc.volume * volumeScale);
        }
    }

    public static void StopSound(string name)
    {
        var am = GetOrCreate();
        if (am.loopingSources.TryGetValue(name, out var src))
        {
            src.Stop();
            Destroy(src);
            am.loopingSources.Remove(name);
        }
    }

    public static void StopAllLooping()
    {
        var am = GetOrCreate();
        foreach (var src in am.loopingSources.Values)
        {
            src.Stop();
            Destroy(src);
        }
        am.loopingSources.Clear();
    }

    [YarnCommand("play_sound")]
    public static void PlaySoundCommand(string name) => PlaySound(name);

    [YarnCommand("stop_sound")]
    public static void StopSoundCommand(string name) => StopSound(name);
}
