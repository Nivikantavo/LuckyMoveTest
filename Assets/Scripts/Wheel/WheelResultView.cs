using UnityEngine;
using TMPro;

public class WheelResultView : MonoBehaviour
{
    [SerializeField] private WheelResults wheelResults;
    [SerializeField] private TextMeshProUGUI resultText;

    public void ShowResult(WheelResult result)
    {
        string message = result.Segment switch
        {
            WheelSegment.Success => wheelResults.SuccsessMessage,
            WheelSegment.Fail => wheelResults.FailMessage,
            WheelSegment.Bonus => wheelResults.BonusMessage,
            WheelSegment.SecondChance => wheelResults.SecondChanceMessage,
            _ => "Unknown Result"
        };
        resultText.text = string.Format(message, result.CoinsAfterResult);
        gameObject.SetActive(true);
    }
}
