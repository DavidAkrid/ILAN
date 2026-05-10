using System.Collections;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    public static HandAnimation Instance;

    [SerializeField] private Animator animator;
    [SerializeField] private string clipName;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator stuffAnimator;
    [SerializeField] private string stuffClipName;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        if (stuffAnimator != null)
            stuffAnimator.enabled = false;
    }

    public void Play()
    {
        gameObject.SetActive(true);
        animator.Play(clipName);
        if (stuffAnimator != null)
        {
            stuffAnimator.enabled = true;
            stuffAnimator.Play(stuffClipName);
        }
        StartCoroutine(WaitForComplete());
    }

    IEnumerator WaitForComplete()
    {
        yield return null;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        SceneLoader.LoadSceneCommand(nextScene);
    }
}
