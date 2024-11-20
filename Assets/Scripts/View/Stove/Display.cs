using TMPro;
using UnityEngine;

namespace View.Stove
{
    public class Display : MonoBehaviour
    {
        [SerializeField] private TMP_Text temperatureText;
        [SerializeField] private TMP_Text timeText;

        public void SetTemperature(int temperature)
        {
            temperatureText.SetText($"Temp: {temperature} ºC");
        }

        public void SetTime(int time)
        {
            timeText.SetText($"Time: {time} s");
        }
    }
}