using TMPro;
using UnityEngine;

public class EndLevelPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text stepsText;
    [SerializeField] private TMP_Text moneyText;

    private string stepsFormat;
    private string moneyFormat;

    private void Awake()
    {
        stepsFormat = stepsText.text;
        moneyFormat = moneyText.text;
    }

    public void Init(int steps, int money)
    {
        stepsText.text = string.Format(stepsFormat, steps);
        moneyText.text = string.Format(moneyFormat, money);
    }
}
