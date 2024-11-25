using System.Collections.Generic;
using Model;
using UnityEngine;

namespace View.UI
{
    public class IngredientsView : MonoBehaviour
    {
        [SerializeField] private IngredientViewObject ingredientObjectPrefab;
        [SerializeField] private Transform ingredientContainer;

        private readonly Dictionary<int, IngredientViewObject> _ingredientObjectInstances = new();

        public void AddIngredient(int ingredient, int maxCount)
        {
            var ingredientObject = Instantiate(ingredientObjectPrefab, ingredientContainer);
            var sprite = PizzaData.Instance.IngredientDataList.Ingredients[ingredient].Thumbnail;
            ingredientObject.Initialize(sprite, maxCount);
            _ingredientObjectInstances.Add(ingredient, ingredientObject);
        }

        public void Clear()
        {
            for (var i = 0; i < _ingredientObjectInstances.Count; i++)
                Destroy(_ingredientObjectInstances[i].gameObject);
            _ingredientObjectInstances.Clear();
        }

        public void UpdateIngredientView(int ingredient, int count) => 
            _ingredientObjectInstances[ingredient].SetCount(count);
    }
}