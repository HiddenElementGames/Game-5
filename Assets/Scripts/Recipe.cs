using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public List<RecipeRequirement> Requirements;
    public GridItem ResultItem;
    public int Priority;
}
