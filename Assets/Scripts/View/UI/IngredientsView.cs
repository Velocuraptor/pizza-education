using System.Collections.Generic;
using System.Linq;
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
            var ingredientObjects = _ingredientObjectInstances.Values.ToList();
            for (var i = 0; i < ingredientObjects.Count; i++)
                Destroy(ingredientObjects[i].gameObject);
            _ingredientObjectInstances.Clear();
        }

        public void UpdateIngredientView(int ingredient, int count) => 
            _ingredientObjectInstances[ingredient].SetCount(count);
    }
}