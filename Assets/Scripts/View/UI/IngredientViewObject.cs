using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class IngredientViewObject : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        private int _currentCount;
        private int _maxCount;
        
        public void Initialize(Sprite sprite, int maxCount)
        {
            image.sprite = sprite;
            _maxCount = maxCount;
            UpdateText();
        }

        public void SetCount(int value)
        {
            _currentCount = value;
            UpdateText();
        }

        private void UpdateText() => text.text = $"{_currentCount}/{_maxCount}";
    }
}