using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Brings up the sell menu when the player selects this grid button
/// </summary>
public class GridButton : MonoBehaviour
{
    private const string SELL_MENU_PATH = "Sell Menu";

    private readonly Vector2 sellMenuOffset = new Vector2(0, 90);
    private GridSlot slot;

    private GameObject sellMenuInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenSellMenu);
        slot = GetComponent<GridSlot>();
    }

    private void OpenSellMenu()
    {
        sellMenuInstance = Instantiate(Resources.Load<GameObject>(SELL_MENU_PATH), transform);
        sellMenuInstance.transform.localPosition = sellMenuOffset;

        Button sellButton = sellMenuInstance.GetComponent<Button>();
        sellButton.onClick.AddListener(SellItem);
    }

    private void SellItem()
    {
        EventManager.Invoke(EventTypes.AddGold, slot.Item.BaseSellValue);
        slot.RemoveItem();
        Destroy(sellMenuInstance);
    }
}
