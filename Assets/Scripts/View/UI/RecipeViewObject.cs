using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View.UI
{
    public class RecipeViewObject : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image image;

        public void Initialize(Sprite sprite, string name, UnityAction onClickAction = null)
        {
            image.sprite = sprite;
            text.text = name;
            button?.onClick.AddListener(onClickAction);
        }
    }
}