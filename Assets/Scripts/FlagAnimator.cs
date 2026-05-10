using UnityEngine;

public class FlagAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string flag;
    [SerializeField] private string animationState;
    [SerializeField] private bool invert;

    void OnValidate()
    {
#if UNITY_EDITOR
        if (animator == null)
            animator = GetComponent<Animator>();
#endif
    }

    void Start() => Refresh();

    public void Refresh()
    {
        bool triggered = GameManager.GetOrCreate().IsTriggered(flag);
        if (triggered != invert)
            animator.Play(animationState);
        else
            animator.Play("Default");
    }
}
