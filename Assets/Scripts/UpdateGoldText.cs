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
        EventManager.StartListening<float>(EventTypes.UpdateGoldText, OnUpdateGoldText);
    }

    private void OnDisable()
    {
        EventManager.StopListening<float>(EventTypes.UpdateGoldText, OnUpdateGoldText);
    }

    private void OnUpdateGoldText(float amount)
    {
        goldText.text = $"Gold: {amount}";
    }
}
