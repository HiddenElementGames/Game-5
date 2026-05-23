using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Tracks the location of each grid slot in the crafting grid
/// </summary>
public class CraftingGrid : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;

    private GridSlot[,] gridObjects = new GridSlot[5, 5];

    public static CraftingGrid Instance;

    private int lastSelectedX = -1;
    private int lastSelectedY = -1;

    private readonly Vector2Int[] Directions =
    {
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.right
    };

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

        if(!slot.HasItem())
        {
            yield break;
        }

        List<GridSlot> connectedSlots = GetConnectedSlots(slot.X, slot.Y);

        // temp code, combines 3 items
        int minComboCount = 3;
        if(connectedSlots.Count >= minComboCount)
        {
            GridItem craftedItem = slot.Item.CraftItem;

            for(int i = 0; i < minComboCount; i++)
            {
                connectedSlots[i].RemoveItem();
            }

            gridObjects[lastSelectedX, lastSelectedY].SetItem(craftedItem);
        }
    }

    private List<GridSlot> GetConnectedSlots(int startX, int startY)
    {
        List<GridSlot> connectedSlots = new();
        Queue<GridSlot> queuedSlots = new();
        HashSet<GridSlot> visitedSlots = new();

        GridSlot startSlot = gridObjects[startX, startY];

        if(!startSlot.HasItem())
        {
            return connectedSlots;
        }

        queuedSlots.Enqueue(startSlot);
        visitedSlots.Add(startSlot);

        while(queuedSlots.Count > 0)
        {
            GridSlot currentSlot = queuedSlots.Dequeue();
            connectedSlots.Add(currentSlot);

            foreach(Vector2Int dir in Directions)
            {
                int newX = currentSlot.X + dir.x;
                int newY = currentSlot.Y + dir.y;

                if(!IsValidLocation(newX, newY))
                {
                    continue;
                }

                GridSlot neighbor = gridObjects[newX, newY];

                if(!neighbor.HasItem())
                {
                    continue;
                }

                if(visitedSlots.Contains(neighbor))
                {
                    continue;
                }

                visitedSlots.Add(neighbor);
                queuedSlots.Enqueue(neighbor);
            }
        }

        return connectedSlots;
    }

    private bool IsValidLocation(int x, int y)
    {
        return x >= 0 && y >= 0 && x < gridObjects.GetLength(0) && y < gridObjects.GetLength(1);
    }

    private Recipe FindMatchingRecipe(List<GridSlot> connectedSlots)
    {
        Dictionary<ItemType, int> itemCounts = new();

        foreach(GridSlot slot in connectedSlots)
        {
            ItemType item = slot.Item.Item;

            if(!itemCounts.ContainsKey(item))
            {
                itemCounts[item] = 0;
            }

            itemCounts[item]++;
        }

        // check recipes based on priority of that recipe
        foreach(Recipe recipe in recipes.OrderByDescending(r => r.Priority))
        {
            bool valid = true;
            
            foreach(RecipeRequirement requirement in recipe.Requirements)
            {
                if(!itemCounts.ContainsKey(requirement.Item))
                {
                    valid = false;
                    break;
                }

                if (itemCounts[requirement.Item] < requirement.Amount)
                {
                    valid = false;
                    break;
                }
            }

            if(valid)
            {
                return recipe;
            }
        }
        return null;
    }
}
