using UnityEngine;

public class TileView : MonoBehaviour
{
    public Transform HolderPosition => _holderPosition;

    [SerializeField] private Transform _holderPosition;
}
