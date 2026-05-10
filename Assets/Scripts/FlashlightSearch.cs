using UnityEngine;
using Yarn.Unity;

public class FlashlightSearch : MonoBehaviour
{
    [SerializeField] private Transform flashlight;
    [SerializeField] private Transform[] searchSpots;
    [SerializeField] private float radius = 1f;
    [SerializeField] private string completionNode;
    [SerializeField] private DialogueRunner dialogueRunner;

    private bool[] found;
    private bool complete;

    void Start()
    {
        found = new bool[searchSpots.Length];
        if (dialogueRunner == null)
            dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    void OnDrawGizmos()
    {
        if (searchSpots == null) return;
        Gizmos.color = Color.yellow;
        foreach (var spot in searchSpots)
            if (spot != null)
                Gizmos.DrawWireSphere(spot.position, radius);
    }

    void Update()
    {
        if (complete) return;
        for (int i = 0; i < searchSpots.Length; i++)
        {
            if (!found[i] && Vector2.Distance(flashlight.position, searchSpots[i].position) < radius)
                found[i] = true;
        }
        for (int j = 0; j < searchSpots.Length; j++)
            Debug.Log($"Spot {j}: distance={Vector2.Distance(flashlight.position, searchSpots[j].position):F2} found={found[j]}");

        if (System.Array.TrueForAll(found, f => f))
        {
            complete = true;
            dialogueRunner.StartDialogue(completionNode);
        }
    }
}
