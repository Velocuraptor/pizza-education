using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using View.UI;

namespace ViewModel
{
    public class RecipeController : MonoBehaviour
    {
        [SerializeField] private BakingDisplay bakingDisplay;
        [SerializeField] private RecipesView recipesView;
        [SerializeField] private IngredientsView ingredientsView;
        [SerializeField] private BowlSpawner bowlSpawner;
        [SerializeField] private PizzaSpawner pizzaSpawner;
        [SerializeField] private IngredientIndicatorController indicatorController;
    
        private static RecipeController _instance;
        private Recipe _currentRecipe;
        private int _currentIndicator;
        private readonly Dictionary<int, (int, int)> _ingredientsCount = new();
    
        public static RecipeController Instance => _instance;
        public Recipe CurrentRecipe => _currentRecipe;
    

        private void Awake() => _instance = this;

        private void Start()
        {
            recipesView.Initialize(PizzaData.Instance.Recipes);
            recipesView.RecipeSelected += SetRecipe;
        }

        private void SetRecipe(Recipe recipe)
        {
            _currentRecipe = recipe;
            bakingDisplay.ShowInformation(true);
            bakingDisplay.UpdateInformation(recipe.Temperature, recipe.Time);
            pizzaSpawner.CreatePizza();
            pizzaSpawner.PizzaInstance.IngredientAdded += IngredientAdded;
            bowlSpawner.ClearBowls();
            bowlSpawner.AddBowls(CurrentRecipe.Ingredients);
            _currentIndicator = 0;
            indicatorController.SetIndicator(CurrentRecipe.Ingredients[0], pizzaSpawner.PizzaInstance);
            InitCounter();
        }

        private void IngredientAdded(int ingredient)
        {
            var (current, max) = _ingredientsCount[ingredient];
            current += 1;
            _ingredientsCount[ingredient] = (current, max);
            ingredientsView.UpdateIngredientView(ingredient, current);
            if (_currentIndicator == CurrentRecipe.Ingredients.Length ||
                ingredient != CurrentRecipe.Ingredients[_currentIndicator]) return;
            if (current < max) return;
            _currentIndicator += 1;
            indicatorController.ClearIndicators();
            if (_currentIndicator == CurrentRecipe.Ingredients.Length) return;
            var index = CurrentRecipe.Ingredients[_currentIndicator];
            indicatorController.SetIndicator(index, pizzaSpawner.PizzaInstance);
        }

        private void InitCounter()
        {
            _ingredientsCount.Clear();
            ingredientsView.Clear();
            foreach (var ingredient in CurrentRecipe.Ingredients)
            {
                var isSouse = PizzaData.Instance.IsSouse(ingredient);
                var maxCount = isSouse ? 1 : PizzaData.Instance.GetPositionsBy(ingredient).Count();
                _ingredientsCount.Add(ingredient, (0,maxCount));
                ingredientsView.AddIngredient(ingredient, maxCount);
            }
        }

        public float GetResult()
        {
            const float maxResult = 50.0f;
            var maxPerIngredient = maxResult / _ingredientsCount.Values.Count;
            var result = 0.0f;
            foreach (var (current, max) in _ingredientsCount.Values)
            {
                var wrongIngredients = Mathf.Abs(current - max);
                result += maxPerIngredient - wrongIngredients * maxPerIngredient / max;
            }

            return result;
        }

        public void Restart()
        {
            recipesView.Restart();
            ingredientsView.Clear();
            bowlSpawner.ClearBowls();
            pizzaSpawner.Restart();
            indicatorController.ClearIndicators();
            bakingDisplay.ShowInformation(false);
        }
    }
}
