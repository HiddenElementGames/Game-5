using NUnit.Framework;
using System.Linq;
using UnityEngine;

/// <summary>
/// Tracks the location of each grid slot in the crafting grid
/// </summary>
public class CraftingGrid : MonoBehaviour
{
    private GridSlot[,] gridObjects = new GridSlot[5, 5];

    public static CraftingGrid Instance;

    void Awake()
    {
        for(int i = 0; i < gridObjects.GetLength(0); i++)
        {
            for(int j = 0; j < gridObjects.GetLength(1); j++)
            {
                gridObjects[i, j] = transform.GetChild(i).GetChild(j).GetComponent<GridSlot>();
            }
        }

        Instance = this;
    }

    public GridSlot GetFreeGridSlot()
    {
        GridSlot[] freeSlots = gridObjects.Cast<GridSlot>().Where(g => g.HasItem() == false).ToArray();
        return freeSlots[Random.Range(0, gridObjects.Length)];
    }
}
