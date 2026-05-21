using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// An individual grid slot in the crafting grid
/// </summary>
public class GridSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            itemImage.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        List<RaycastResult> results = new();
        GraphicRaycaster raycaster = FindAnyObjectByType<GraphicRaycaster>();
        raycaster.Raycast(eventData, results);
        if(results.Count > 0 && results.Where(r => r.gameObject.CompareTag("GridSlot")).Any())
        {
            GridSlot slot = results.Where(r => r.gameObject.CompareTag("GridSlot")).First().gameObject.GetComponent<GridSlot>();
            if(slot.HasItem())
            {
                slot.SwapItem(this, currentItem);
            }
            else
            {
                slot.SetItem(currentItem);
                RemoveItem();
            }
        }
		itemImage.transform.localPosition = Vector2.zero;
	}

}
