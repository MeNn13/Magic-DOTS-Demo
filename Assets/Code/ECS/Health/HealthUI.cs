using UnityEngine;
using UnityEngine.UI;

namespace Code.ECS.Health
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image background;
        [SerializeField] private Image fill;

        public void SetBackgroundColor(Color color) => background.color = color;

        public void SetFillColor(Color color) => fill.color = color;

        public void UpdateHealth(float value) => healthSlider.value = value;
    }
}
