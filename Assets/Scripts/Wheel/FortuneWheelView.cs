using System;
using System.Collections;
using UnityEngine;

public class FortuneWheelView : MonoBehaviour
{
    [SerializeField] private RectTransform wheelTransform;
    [SerializeField] private float spinDuration = 3f;
    [SerializeField] private AnimationCurve spinEase;
    [SerializeField] private WheelResultView resultView;

    public event Action OnSpinFinished;

    private bool _isSpinning;
    private float _disablerTime = 5f;
    public void Show(WheelResult result, Action onComplete)
    {
        gameObject.SetActive(true);
        SpinToSegment((int)result.Segment, result, onComplete);
    }

    public void HideWithDelay()
    {
        StartCoroutine(DelayedDisable(_disablerTime));
    }

    private void SpinToSegment(int segmentIndex, WheelResult result, Action onComplete)
    {
        if (_isSpinning) return;
        StartCoroutine(SpinRoutine(segmentIndex, result, onComplete));
    }

    private IEnumerator SpinRoutine(int segmentIndex, WheelResult result, Action onComplete)
    {
        float anglePerSegment = 360f / 5f;
        float targetAngle = 720f + segmentIndex * anglePerSegment;

        float startAngle = wheelTransform.eulerAngles.z;
        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / spinDuration);
            float eased = Mathf.SmoothStep(0, 1, t);

            float angle = Mathf.Lerp(startAngle, -targetAngle, eased);
            wheelTransform.eulerAngles = new Vector3(0, 0, angle);

            yield return null;
        }
        ShowResult(result);
        yield return new WaitForSeconds(3f);
        resultView.gameObject.SetActive(false);
        onComplete?.Invoke();
    }

    private void ShowResult(WheelResult result)
    {
        resultView.gameObject.SetActive(true);
        resultView.ShowResult(result);
        HideWithDelay();
    }

    private IEnumerator DelayedDisable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
