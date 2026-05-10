using UnityEngine;
using Yarn.Unity;

public class Hotspot : MonoBehaviour
{
    [SerializeField] DialogueReference dialogue = new();
    [SerializeField] private DialogueRunner dialogueRunner;
    public bool autoStart = false;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this)) return;
#endif
        if (dialogueRunner == null)
            dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        if (dialogueRunner != null && dialogueRunner.YarnProject != null && dialogue.project == null)
            dialogue.project = dialogueRunner.YarnProject;
    }

    void Start()
    {
        if (autoStart && dialogue.IsValid)
            dialogueRunner.StartDialogue(dialogue.nodeName);
    }

    void OnMouseDown()
    {
        if (dialogueRunner.IsDialogueRunning) return;
        if (!dialogue.IsValid) return;
        dialogueRunner.StartDialogue(dialogue.nodeName);
    }
}
