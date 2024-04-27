using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.View
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Image _background;
        [SerializeField] private Image _fill;

        public void SetBackgroundColor(Color color) => _background.color = color;

        public void SetFillColor(Color color) => _fill.color = color;

        public void UpdateHealth(float value) => _healthSlider.value = value;
    }
}
