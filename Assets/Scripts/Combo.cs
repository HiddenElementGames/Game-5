using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combo")]
public class Combo : ScriptableObject
{
    public List<ItemType> ComboItems;
    public GridItem CraftedItem;
}
