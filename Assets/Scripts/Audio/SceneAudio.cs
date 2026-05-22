using System.Collections.Generic;
using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    [SerializeField] private List<NamedClip> clips = new();

    void Awake()
    {
        AudioManager.GetOrCreate().RegisterClips(clips);
    }
}
