using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using View;

namespace ViewModel
{
    public class IngredientIndicatorController : MonoBehaviour
    {
        [SerializeField] private IngredientIndicator ingredientIndicator;
        
        private readonly List<GameObject> _indicatorInstances = new();

        public void SetIndicator(int ingredient, Pizza pizza)
        {
            var isSouse = PizzaData.Instance.IsSouse(ingredient);
            if (isSouse)
            {
                var indicator = Instantiate(ingredientIndicator, pizza.SouseLayer);
                indicator.Initialize(ingredient);
                _indicatorInstances.Add(indicator.gameObject);
            }
            else
            {
                var placements = PizzaData.Instance.GetPositionsBy(ingredient);
                foreach (var placement in placements)
                {
                    var indicator = Instantiate(ingredientIndicator, pizza.ToppingLayer);
                    indicator.transform.localPosition = placement;
                    indicator.Initialize(ingredient);
                    _indicatorInstances.Add(indicator.gameObject);
                }
            }
        }

        public void ClearIndicators()
        {
            for (var i = 0; i < _indicatorInstances.Count; i++) Destroy(_indicatorInstances[i]);
            _indicatorInstances.Clear();
        }
    }
}