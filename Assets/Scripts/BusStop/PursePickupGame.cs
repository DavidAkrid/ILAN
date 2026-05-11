using UnityEngine;
using Yarn.Unity;

public class PursePickupGame : MonoBehaviour
{
    public static PursePickupGame Instance;

    [SerializeField] private GameObject[] items;
    [SerializeField] private string yarnNode;
    private DialogueRunner dialogueRunner;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    public void ItemPickedUp(GameObject item)
    {
        item.SetActive(false);

        foreach (GameObject i in items)
            if (i.activeSelf) return;

        GameManager.GetOrCreate().SetTriggered("purse_game_complete");
        if (!string.IsNullOrEmpty(yarnNode))
        {
            if (dialogueRunner == null) Debug.LogWarning("PursePickupGame: no DialogueRunner found in scene.");
            else dialogueRunner.StartDialogue(yarnNode);
        }
        HandAnimation.Instance.Play();
    }
}
