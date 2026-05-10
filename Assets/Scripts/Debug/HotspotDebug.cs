using UnityEngine;

public class HotspotDebug : MonoBehaviour
{
    public bool showDebug = true;

    void OnValidate()
    {
        ApplyDebugVisibility(showDebug);
    }

    public void SetVisible(bool visible)
    {
        showDebug = visible;
        ApplyDebugVisibility(visible);
    }

    private void ApplyDebugVisibility(bool visible)
    {
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>(true))
            sr.enabled = visible;
    }
}
