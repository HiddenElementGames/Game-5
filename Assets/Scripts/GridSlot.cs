using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An individual grid slot in the crafting grid
/// </summary>
public class GridSlot : MonoBehaviour
{
    private GridItem currentItem = null;

    private Image itemImage;

    private Button gridButton;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        gridButton = GetComponent<Button>();
	}

    public bool HasItem()
    {
        return currentItem != null;
    }

    public void SetItem(GridItem item)
    {
        currentItem = item;

		// set the sprite
		itemImage.enabled = true;
		itemImage.sprite = currentItem.itemSprite;
        if(itemImage.type == Image.Type.Simple)
        {
			itemImage.preserveAspect = true;
        }

        // enable the grid button
        gridButton.enabled = true;

	}

    public void SwapItem(GridSlot slot, GridItem item)
    {
        slot.SetItem(currentItem);
        SetItem(item);
    }

    public void RemoveItem()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        gridButton.enabled = false;
	}
}
