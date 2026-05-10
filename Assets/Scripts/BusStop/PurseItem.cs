using UnityEngine;

public class PurseItem : MonoBehaviour
{
    void OnMouseDown()
    {
        PursePickupGame.Instance.ItemPickedUp(gameObject);
    }
}
