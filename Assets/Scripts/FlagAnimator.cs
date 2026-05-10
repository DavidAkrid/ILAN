using System;
using System.Collections;
using UnityEngine;

public class FlagAnimator : MonoBehaviour
{
    [Serializable]
    public struct FlagState
    {
        public string flag;
        public string animationState;
        public bool invert;
    }

    [SerializeField] private Animator animator;
    [SerializeField] private FlagState[] states;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (animator == null)
            animator = GetComponent<Animator>();
#endif
    }

    void Start() => StartCoroutine(RefreshNextFrame());

    private IEnumerator RefreshNextFrame()
    {
        yield return null;
        Refresh();
    }

    public void Refresh()
    {
        if (animator == null || states == null || states.Length == 0) return;
        var gm = GameManager.GetOrCreate();
        foreach (var s in states)
        {
            if (string.IsNullOrEmpty(s.flag) || string.IsNullOrEmpty(s.animationState)) continue;
            bool triggered = gm.IsTriggered(s.flag);
            if (triggered != s.invert)
            {
                animator.Play(s.animationState);
                return;
            }
        }
        animator.Play("Default");
    }
}
