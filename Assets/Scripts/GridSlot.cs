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
    public GridItem Item = null;

    private Image itemImage;

    private Button gridButton;

    public int X;
    public int Y;

	private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        gridButton = GetComponent<Button>();
	}

    public bool HasItem()
    {
        return Item != null;
    }

    public void SetItem(GridItem item)
    {
        Item = item;

		// set the sprite
		itemImage.enabled = true;
		itemImage.sprite = Item.ItemSprite;
        if(itemImage.type == Image.Type.Simple)
        {
			itemImage.preserveAspect = true;
        }

        // enable the grid button
        gridButton.enabled = true;
        StartCoroutine(CraftingGrid.Instance.CheckForCombo(this));
	}

    public void SwapItem(GridSlot slot, GridItem item)
    {
        slot.SetItem(Item);
        SetItem(item);
    }

    public void RemoveItem()
    {
        Item = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        gridButton.enabled = false;
	}

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            itemImage.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item == null) return;

        List<RaycastResult> results = new();
        GraphicRaycaster raycaster = FindAnyObjectByType<GraphicRaycaster>();
        raycaster.Raycast(eventData, results);
        if(results.Count > 0 && results.Where(r => r.gameObject.CompareTag("GridSlot")).Any())
        {
            GridSlot slot = results.Where(r => r.gameObject.CompareTag("GridSlot")).First().gameObject.GetComponent<GridSlot>();
            if(slot.HasItem())
            {
                slot.SwapItem(this, Item);
            }
            else
            {
                slot.SetItem(Item);
                RemoveItem();
            }
        }
		itemImage.transform.localPosition = Vector2.zero;
	}

}
