using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Pizza/Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        public string Name;
        public int[] Ingredients;
    }
}