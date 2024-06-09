using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.ECS.Skill.Mono
{
    public class SkillUI : MonoBehaviour
    {
        [Header("Skills Icons")] 
        [SerializeField] private GameObject pyroIcon;
        [SerializeField] private GameObject hydroIcon;
        [SerializeField] private GameObject ventoIcon;
        [SerializeField] private GameObject geoIcon;
        [SerializeField] private float duration = .1f;
        
        private readonly Vector3 _defaultSize = Vector3.one;
        private readonly Vector3 _maxSize = new(x: .8f, y: .8f, z: 1f);

        private void Start()
        {
            pyroIcon.transform.localScale = _defaultSize;
            hydroIcon.transform.localScale = _defaultSize;
            ventoIcon.transform.localScale = _defaultSize;
            geoIcon.transform.localScale = _defaultSize;
        }

        public void PyroPres(bool value) => IconAnimation(transformIcon: pyroIcon.transform, value: value);
        public void HydroPres(bool value) => IconAnimation(transformIcon: hydroIcon.transform, value: value);
        public void VentoPres(bool value) => IconAnimation(transformIcon: ventoIcon.transform, value: value);
        public void GeoPres(bool value) => IconAnimation(transformIcon: geoIcon.transform, value: value);
        private void IconAnimation(Transform transformIcon, bool value)
        {
            if (!value || transformIcon.localScale != _defaultSize)
            {
                if (!value && transformIcon.localScale == _maxSize)
                    transformIcon.DOScale(endValue: _defaultSize, duration: duration);
            }
            else
                transformIcon.DOScale(endValue: _maxSize, duration: duration);
        }
    }
}
