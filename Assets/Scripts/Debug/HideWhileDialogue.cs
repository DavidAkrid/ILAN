using UnityEngine;
using Yarn.Unity;

public class HideWhileDialogue : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnValidate()
    {
#if UNITY_EDITOR
        if (dialogueRunner == null)
            dialogueRunner = FindAnyObjectByType<DialogueRunner>();
#endif
    }

    void Update()
    {
        var active = !dialogueRunner.IsDialogueRunning;
        if (spriteRenderer != null) spriteRenderer.enabled = active;
        if (col != null) col.enabled = active;
    }
}
