using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid Item")]
public class GridItem : ScriptableObject
{
	public Sprite ItemSprite;
	public ItemType Item;
	public CraftType Craft;
	public GridItem CraftItem;
}