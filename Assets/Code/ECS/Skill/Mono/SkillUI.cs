using DG.Tweening;
using UnityEngine;

namespace Assets.Code.ECS.Skill.Mono
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pyroIcon;
        [SerializeField] private GameObject _hydroIcon;
        [SerializeField] private GameObject _ventoIcon;
        [SerializeField] private GameObject _geoIcon;

        private readonly Vector3 _defaultSize = Vector3.one;
        private readonly Vector3 _maxSize = new(.8f, .8f, 1f);
        private readonly float _duration = .1f;

        private void Start()
        {
            _pyroIcon.transform.localScale = _defaultSize;
            _hydroIcon.transform.localScale = _defaultSize;
            _ventoIcon.transform.localScale = _defaultSize;
            _geoIcon.transform.localScale = _defaultSize;
        }

        public void PyroPres(bool value) => IconAnimation(_pyroIcon.transform, value);
        public void HydroPres(bool value) => IconAnimation(_hydroIcon.transform, value);
        public void VentoPres(bool value) => IconAnimation(_ventoIcon.transform, value);
        public void GeoPres(bool value) => IconAnimation(_geoIcon.transform, value);

        private void IconAnimation(Transform transform, bool value)
        {
            if (value && transform.localScale == _defaultSize)
                transform.DOScale(_maxSize, _duration);
            else if (!value && transform.localScale == _maxSize)
                transform.DOScale(_defaultSize, _duration);
        }
    }
}
