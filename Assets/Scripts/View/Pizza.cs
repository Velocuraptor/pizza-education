using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

namespace View
{
    public class Pizza : MonoBehaviour
    {
        [SerializeField] private Transform souseLayer;
        [SerializeField] private Transform toppingLayer;

        private (int, GameObject) _souseIndex = (-1, null);
        private readonly Dictionary<int, List<GameObject>> _toppingInstances = new();

        public Transform SouseLayer => souseLayer;
        public Transform ToppingLayer => toppingLayer;

        public event Action<int> IngredientAdded;
        public event Action<int> IngredientRemoved;

        public void AddIngredient(int ingredientIndex, Vector3 position)
        {
            var ingredient = PizzaData.Instance.IngredientDataList.GetIngredientBy(ingredientIndex);
            if (ingredient.IsSouse)
            {
                if (_souseIndex.Item1 != -1)
                {
                    IngredientRemoved?.Invoke(_souseIndex.Item1);
                    Destroy(_souseIndex.Item2);
                }
                var newSouse = Instantiate(ingredient.Model, souseLayer);
                _souseIndex = (ingredientIndex, newSouse);
            }
            else
            {
                var newTopping = Instantiate(ingredient.Model, toppingLayer);
                position.y = 0;
                newTopping.transform.position = position;
                if (!_toppingInstances.ContainsKey(ingredientIndex))
                    _toppingInstances.Add(ingredientIndex, new List<GameObject>());
                _toppingInstances[ingredientIndex].Add(newTopping);
            }
            IngredientAdded?.Invoke(ingredientIndex);
        }
    }
}