using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

/// <summary>
/// Tracks the location of each grid slot in the crafting grid
/// </summary>
public class CraftingGrid : MonoBehaviour
{
    public GraphicRaycaster Raycaster;

    private GridSlot[,] gridObjects = new GridSlot[5, 5];

    public static CraftingGrid Instance;

    private int lastSelectedX = -1;
    private int lastSelectedY = -1;

    void Awake()
    {
        for(int i = 0; i < gridObjects.GetLength(0); i++)
        {
            for(int j = 0; j < gridObjects.GetLength(1); j++)
            {
                gridObjects[i, j] = transform.GetChild(i).GetChild(j).GetComponent<GridSlot>();
                gridObjects[i, j].X = i;
                gridObjects[i, j].Y = j;
            }
        }

        Instance = this;
    }

    public GridSlot GetFreeGridSlot()
    {
        GridSlot[] freeSlots = gridObjects.Cast<GridSlot>().Where(g => g.HasItem() == false).ToArray();
        return freeSlots[Random.Range(0, freeSlots.Length)];
    }

    public IEnumerator CheckForCombo(GridSlot slot)
    {
        yield return null;
        lastSelectedX = slot.X;
        lastSelectedY = slot.Y;
        CraftType craft = slot.Item.Craft;
        ItemType item = slot.Item.Item;
        GridItem craftItem = slot.Item.CraftItem;
        int x = slot.X;
        int y = slot.Y;

        if (craft == CraftType.Ore)
        {
            List<GridSlot> validSlots = new();
            validSlots.Add(slot);
            validSlots = CheckNeighboringLocations(x, y, item, validSlots, true);

            int minComboCount = 3;

            if(validSlots.Count >= minComboCount)
            {
                foreach(GridSlot s in validSlots)
                {
                    Debug.Log($"Slot x:{s.X}, Slot y:{s.Y}");
                }

                for(int i = minComboCount - 1; i >=0; i--)
                {
                    validSlots[i].RemoveItem();
                }
                gridObjects[lastSelectedX, lastSelectedY].SetItem(craftItem);
            }
        }

    }

    private List<GridSlot> CheckNeighboringLocations(int x, int y, ItemType item, List<GridSlot> currentSlots, bool initialLocation = false)
    {
        List<GridSlot> validSlots = new();
        if (initialLocation)
        {
            validSlots.AddRange(currentSlots);
        }

        // check left
        if (IsValidLocation(x - 1))
        {
            GridSlot slot = gridObjects[x - 1, y];
            if (slot.HasItem() && slot.Item.Item == item && !validSlots.Contains(slot))
            {
                validSlots.Add(slot);
                if (initialLocation)
                {
                    validSlots.AddRange(CheckNeighboringLocations(x - 1, y, item, validSlots));
                }
            }
        }
        // check up
        if (IsValidLocation(y - 1))
        {
            GridSlot slot = gridObjects[x, y - 1];
            if (slot.HasItem() && slot.Item.Item == item && !validSlots.Contains(slot))
            {
                validSlots.Add(slot);
                if (initialLocation)
                {
                    validSlots.AddRange(CheckNeighboringLocations(x, y - 1, item, validSlots));
                }
            }
        }
        // check down
        if (IsValidLocation(y + 1))
        {
            GridSlot slot = gridObjects[x, y + 1];
            if (slot.HasItem() && slot.Item.Item == item && !validSlots.Contains(slot))
            {
                validSlots.Add(slot);
                if (initialLocation)
                {
                    validSlots.AddRange(CheckNeighboringLocations(x, y + 1, item, validSlots));
                }
            }
        }
        // check right
        if (IsValidLocation(x + 1))
        {
            GridSlot slot = gridObjects[x + 1, y];
            if (slot.HasItem() && slot.Item.Item == item && !validSlots.Contains(slot))
            {
                validSlots.Add(slot);
                if (initialLocation)
                {
                    validSlots.AddRange(CheckNeighboringLocations(x + 1, y, item, validSlots));
                }
            }
        }

        if(initialLocation)
        {
            // remove any duplicates
            for(int i = validSlots.Count - 1; i >= 0; i--)
            {
                GridSlot slot = validSlots[i];
                for(int j = validSlots.Count - 2; j >= 0; j--)
                {
                    if (slot.X == validSlots[j].X && slot.Y == validSlots[j].Y && i != j)
                    {
                        validSlots.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        return validSlots;
    }

    private bool IsValidLocation(int value)
    {
        return value >= 0 && value < 5;
    }
}
