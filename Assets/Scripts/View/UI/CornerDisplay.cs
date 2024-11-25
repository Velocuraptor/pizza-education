using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class CornerDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject informationPage;
        [SerializeField] private GameObject resultPage;
        [SerializeField] private TMP_Text congratulationsText;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Button tryAgainButton;

        public event Action TryAgainButtonClicked;
        
        private void Start()
        {
            tryAgainButton.onClick.AddListener(() => TryAgainButtonClicked?.Invoke());
        }

        public void ShowResultPage()
        {
            informationPage.SetActive(false);
            resultPage.SetActive(true);
        }

        public void ShowInformationPage()
        {
            informationPage.SetActive(true);
            resultPage.SetActive(false);
        }

        public void UpdateCongratulationText(string recipeName) =>
            congratulationsText.text = $"Congratulations you have cooked {recipeName}";
        
        public void UpdateResultText(float resultValue) =>
            resultText.text = $"Result\n{resultValue:N0}%";
        
        public void UpdateTimeText(float cookingTime) => 
            timeText.text = $"Time\n{(cookingTime / 60):00}:{(cookingTime % 60):00}";
    }
}