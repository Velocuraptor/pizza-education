using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Pizza/Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        public float Temperature;
        public float Time;
        public int[] Ingredients;
    }
}