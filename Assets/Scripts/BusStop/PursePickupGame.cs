using UnityEngine;

public class PursePickupGame : MonoBehaviour
{
    public static PursePickupGame Instance;

    [SerializeField] private GameObject[] items;

    void Awake()
    {
        Instance = this;
    }

    public void ItemPickedUp(GameObject item)
    {
        item.SetActive(false);

        foreach (GameObject i in items)
            if (i.activeSelf) return;

        GameManager.GetOrCreate().SetTriggered("purse_game_complete");
        HandAnimation.Instance.Play();
    }
}
