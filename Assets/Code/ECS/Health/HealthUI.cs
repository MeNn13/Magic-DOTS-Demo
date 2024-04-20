using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.View
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public void UpdateHealth(float value)
        {
            _healthSlider.value = value;   
        }
    }
}
