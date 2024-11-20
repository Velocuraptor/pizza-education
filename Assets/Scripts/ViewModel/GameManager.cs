using Model;
using UnityEngine;

namespace ViewModel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private RecipeController recipeController;
        [SerializeField] private IngredientIndicatorController indicatorController;
        [SerializeField] private BowlSpawner bowlSpawner;
        [SerializeField] private PizzaSpawner pizzaSpawner;

        [SerializeField] private Recipe testRecipe;

        private void Start()
        {
            recipeController.SetRecipe(testRecipe);
            foreach (var ingredient in RecipeController.Instance.CurrentRecipe.Ingredients) bowlSpawner.AddBowl(ingredient);
            pizzaSpawner.CreatePizza();
        }
    }
}