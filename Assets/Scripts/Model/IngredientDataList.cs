using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class IngredientDataList : ScriptableObject
    {
        [SerializeField] private IngredientData[] ingredients;

        public IReadOnlyList<IngredientData> Ingredients => ingredients;

        public IngredientData GetIngredientBy(int index) => Ingredients[index];
    }
}