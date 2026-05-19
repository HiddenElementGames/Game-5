using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GridItem[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SpawnItem);
    }

    private void SpawnItem()
    {
        GridSlot slot = CraftingGrid.Instance.GetFreeGridSlot();

        GridItem randomItem = items[Random.Range(0, items.Length)];

        slot.SetItem(randomItem);
    }
}
