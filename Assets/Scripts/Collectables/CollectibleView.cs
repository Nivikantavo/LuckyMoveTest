using UnityEngine;

public class CollectibleView : MonoBehaviour
{
    [SerializeField] private ItemType type;
    public int Step { get; private set; }
    public ItemType Type => type;
    public void Init(int step)
    {
        Step = step;
    }

    public void PlayPickupEffect()
    {
        // TODO: ����� �������� �������, ���� � �.�.
        Destroy(gameObject);
    }
}
