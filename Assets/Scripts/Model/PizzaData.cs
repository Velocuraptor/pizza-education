using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class PizzaData : MonoBehaviour
    {
        private static PizzaData _instance;

        [SerializeField] private IngredientDataList ingredientDataList;
        [SerializeField] private PlacementData[] placements;
        [SerializeField] private Recipe[] recipes;

        private readonly Dictionary<int, IEnumerable<Vector3>> positions = new();
        
        public static PizzaData Instance => _instance;
        public IngredientDataList IngredientDataList => ingredientDataList;
        public IReadOnlyList<Recipe> Recipes => recipes;
        
        private void Awake()
        {
            _instance = this;
            foreach (var placement in placements)
                positions.Add(placement.Ingredient, placement.Positions);
        }

        public bool IsSouse(int ingredientIndex) => IngredientDataList.Ingredients[ingredientIndex].IsSouse;
        
        public IEnumerable<Vector3> GetPositionsBy(int ingredientIndex) => positions[ingredientIndex];
    }
}