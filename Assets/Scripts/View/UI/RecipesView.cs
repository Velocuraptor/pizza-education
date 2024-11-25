using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace View.UI
{
    public class RecipesView : MonoBehaviour
    {
        [SerializeField] private RecipeViewObject recipeObjectPrefab;
        [SerializeField] private Transform recipesContainer;
        [SerializeField] private RecipeViewObject selectedRecipe;
        
        public Action<Recipe> RecipeSelected;
        
        public void Initialize(IEnumerable<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                var newRecipeObject = Instantiate(recipeObjectPrefab, recipesContainer);
                newRecipeObject.Initialize(recipe.Sprite, recipe.Name, () => OnRecipeSelected(recipe));
            }
        }

        private void OnRecipeSelected(Recipe recipe)
        {
            selectedRecipe.Initialize(recipe.Sprite, recipe.Name);
            selectedRecipe.gameObject.SetActive(true);
            recipesContainer.gameObject.SetActive(false);
            RecipeSelected?.Invoke(recipe);
        }

        public void Restart()
        {
            selectedRecipe.gameObject.SetActive(false);
            recipesContainer.gameObject.SetActive(true);
        }
    }
}