using UnityEngine;

namespace Code.ECS.Skill.Mono
{
    public class SkillsCircle : MonoBehaviour
    {
        [SerializeField] private GameObject aetherCircle;
        [SerializeField] private GameObject fireCircle;
        [SerializeField] private GameObject hydroCircle;
        [SerializeField] private GameObject ventoCircle;
        [SerializeField] private GameObject geoCircle;

        private void Start() => SetAllCircles(false);

        public void SetAllCircles(bool value)
        {
            SetFireCircle(value);
            SetHydroCircle(value);
            SetVentoCircle(value);
            SetGeoCircle(value);
            aetherCircle.SetActive(value);
        }

        public void SetFireCircle(bool value)
        {
            aetherCircle.SetActive(value);
            fireCircle.SetActive(value);
        }

        public void SetHydroCircle(bool value)
        {
            aetherCircle.SetActive(value);
            hydroCircle.SetActive(value);
        }

        public void SetVentoCircle(bool value)
        {
            aetherCircle.SetActive(value);
            ventoCircle.SetActive(value);
        }

        public void SetGeoCircle(bool value)
        {
            aetherCircle.SetActive(value);
            geoCircle.SetActive(value);
        }
    }
}
