using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 _targetPos;
    private float _approximatelyValue = 0.1f;

    public void MoveToStep(Vector3 stepPosition)
    {
        _targetPos = stepPosition;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
    }

    public bool IsAtTargetPosition()
    {
        return Vector3.Distance(transform.position, _targetPos) < _approximatelyValue;
    }
}
