using System;
using DG.Tweening;
using Model;
using UnityEngine;
using View;
using View.UI;

namespace ViewModel
{
    public class CookingManager : MonoBehaviour
    {
        [SerializeField] private PizzaSpawner pizzaSpawner;
        [SerializeField] private RecipesView recipesView;
        [SerializeField] private Tray tray;
        [SerializeField] private CornerDisplay cornerDisplay;

        private Recipe _recipe;
        private float _startCooking;
        private float _endCooking;

        private void Start()
        {
            recipesView.RecipeSelected += StartCooking;
            tray.PizzaOnTray += FinishCooking;
            cornerDisplay.TryAgainButtonClicked += Restart;
        }

        private void StartCooking(Recipe recipe)
        {
            _recipe = recipe;
            _startCooking = Time.time;
        }
        
        private void FinishCooking()
        {
            _endCooking = Time.time;
            var time = _endCooking - _startCooking;
            var result = pizzaSpawner.PizzaInstance.Result + RecipeController.Instance.GetResult();
            cornerDisplay.ShowResultPage();
            cornerDisplay.UpdateCongratulationText(_recipe.Name);
            cornerDisplay.UpdateResultText(result);
            cornerDisplay.UpdateTimeText(time);
        }
        
        private void Restart()
        {
            tray.Restart();
            cornerDisplay.ShowInformationPage();
            RecipeController.Instance.Restart();
        }
    }
}