using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Oculus.Interaction;
using UnityEngine;

namespace View
{
    public class Pizza : MonoBehaviour
    {
        [SerializeField] private Ingredient crust;
        [SerializeField] private Transform souseLayer;
        [SerializeField] private Transform toppingLayer;
        [SerializeField] private PointableUnityEventWrapper grabbableUnityEventWrapper;

        private float _bakingValue;
        private (int, Ingredient) _souse = (-1, null);
        private readonly Dictionary<int, List<Ingredient>> _toppingInstances = new();

        public Transform SouseLayer => souseLayer;
        public Transform ToppingLayer => toppingLayer;
        public float Result => 50.0f - Mathf.Abs(_bakingValue - 100.0f);

        public event Action<int> IngredientAdded;
        public event Action<int> IngredientRemoved;
        public event Action Grabbed;
        public event Action Released;

        private void Start()
        {
            grabbableUnityEventWrapper.WhenSelect.AddListener(_ => Grabbed?.Invoke());
            grabbableUnityEventWrapper.WhenUnselect.AddListener(_ => Released?.Invoke());
        }

        public void AddIngredient(int ingredientIndex, Vector3 position)
        {
            var ingredient = PizzaData.Instance.IngredientDataList.GetIngredientBy(ingredientIndex);
            if (ingredient.IsSouse)
            {
                if (_souse.Item1 != -1)
                {
                    IngredientRemoved?.Invoke(_souse.Item1);
                    Destroy(_souse.Item2);
                }
                var newSouse = Instantiate(ingredient.Model, souseLayer);
                _souse = (ingredientIndex, newSouse);
            }
            else
            {
                var newTopping = Instantiate(ingredient.Model, toppingLayer);
                position.y = toppingLayer.position.y;
                newTopping.transform.position = position;
                if (!_toppingInstances.ContainsKey(ingredientIndex))
                    _toppingInstances.Add(ingredientIndex, new List<Ingredient>());
                _toppingInstances[ingredientIndex].Add(newTopping);
            }
            IngredientAdded?.Invoke(ingredientIndex);
        }

        public void Bake(float increment)
        {
            _bakingValue += increment;
            var currentState = PizzaData.GetStateBy(_bakingValue);
            var nextState = currentState == BakingState.Burn ? currentState : currentState + 1;
            var value = (_bakingValue - PizzaData.BakingValues[currentState]) /
                        (PizzaData.BakingValues[nextState] - PizzaData.BakingValues[currentState]);
            crust.Bake(currentState, nextState, value);
            _souse.Item2?.Bake(currentState, nextState, value);
            foreach (var ingredient in _toppingInstances.Values.SelectMany(i => i))
                ingredient.Bake(currentState, nextState, value);
        }
    }
}