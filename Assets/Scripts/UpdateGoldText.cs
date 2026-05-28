using TMPro;
using UnityEngine;

public class UpdateGoldText : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    private void Awake()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventManager.StartListening<int>(EventTypes.UpdateGoldText, OnUpdateGoldText);
    }

    private void OnDisable()
    {
        EventManager.StopListening<int>(EventTypes.UpdateGoldText, OnUpdateGoldText);
    }

    private void OnUpdateGoldText(int amount)
    {
        goldText.text = $"Gold: {amount}";
    }
}
