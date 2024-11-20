using System;
using Model;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    private static RecipeController _instance;
    private Recipe _currentRecipe;
    
    public static RecipeController Instance => _instance;
    public Recipe CurrentRecipe => _currentRecipe;

    private void Awake()
    {
        _instance = this;
    }

    public void SetRecipe(Recipe recipe)
    {
        _currentRecipe = recipe;
    }
}
