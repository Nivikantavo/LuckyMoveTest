using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -10);

    private Vector3 _velocity;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = new Vector3(target.position.x, 0, 0) + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref _velocity,
            smoothTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
