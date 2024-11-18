using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = "PlacementData", menuName = "Pizza/PlacementData", order = 0)]
    public class PlacementData : ScriptableObject
    {
        public int Ingredient;
        public Vector3[] Positions;
    }
}