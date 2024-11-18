using Model;
using UnityEngine;
using View;

namespace ViewModel
{
    public class PizzaMaker : MonoBehaviour
    {
        [SerializeField] private Transform pizzaContainer;
        [SerializeField] private Pizza pizzaPrefab;

        private Recipe _currentRecipe;
        private Pizza _pizzaInstance;

        public void CreatePizza(Recipe recipe)
        {
            _currentRecipe = recipe;
            _pizzaInstance = Instantiate(pizzaPrefab, pizzaContainer);
        }
    }
}