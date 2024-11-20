using Model;
using UnityEngine;
using View;

namespace ViewModel
{
    public class PizzaSpawner : MonoBehaviour
    {
        [SerializeField] private Transform pizzaContainer;
        [SerializeField] private Pizza pizzaPrefab;
        
        private Pizza _pizzaInstance;

        public void CreatePizza()
        {
            _pizzaInstance = Instantiate(pizzaPrefab, pizzaContainer);
        }
    }
}