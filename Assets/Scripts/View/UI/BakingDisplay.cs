using TMPro;
using UnityEngine;

namespace View.UI
{
    public class BakingDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text information;

        public void ShowInformation(bool state) => information.gameObject.SetActive(state);

        public void UpdateInformation(float temperature, float time) => 
            information.text = $"Baking Information\n\nTemperature {temperature:N0} ºC\nBaking Time {time:N0} sec";
    }
}