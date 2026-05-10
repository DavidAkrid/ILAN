using System;
using UnityEngine;
using Yarn.Unity;

public class ObjectState : MonoBehaviour
{
    [Serializable]
    public struct State
    {
        public string flag;
        public Sprite sprite;
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private State[] states;
    [SerializeField] private string enableFlag;
    [SerializeField] private string disableFlag;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
#endif
    }

    void Awake()
    {
        if (defaultSprite == null && spriteRenderer != null)
            defaultSprite = spriteRenderer.sprite;
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        var gm = GameManager.GetOrCreate();

        if (!string.IsNullOrEmpty(enableFlag))
            gameObject.SetActive(gm.IsTriggered(enableFlag));

        if (!string.IsNullOrEmpty(disableFlag) && gm.IsTriggered(disableFlag))
            gameObject.SetActive(false);

        if (!gameObject.activeSelf || spriteRenderer == null) return;

        foreach (State state in states)
        {
            if (gm.IsTriggered(state.flag))
            {
                spriteRenderer.sprite = state.sprite;
                return;
            }
        }
        spriteRenderer.sprite = defaultSprite;
    }
}
