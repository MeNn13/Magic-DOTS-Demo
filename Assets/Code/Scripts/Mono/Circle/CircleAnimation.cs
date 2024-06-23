using UnityEngine;

namespace Code.Scripts.Mono.Circle
{
    public class CircleAnimation : MonoBehaviour
    {
        [SerializeField] private float duration = 200f;
        [SerializeField] private CircleSideRotate side = CircleSideRotate.Left;

        private bool _canRotating;

        private void OnEnable()
        {
            _canRotating = true;
            transform.Rotate(Vector3.zero);
        }

        private void OnDisable() => _canRotating = false;

        private void Update()
        {
            if (_canRotating)
                transform.Rotate(SetRotateSide(side), duration * Time.deltaTime);
        }

        private Vector3 SetRotateSide(CircleSideRotate sideRotate)
        {
            return sideRotate == CircleSideRotate.Left
                ? new Vector3(0, 0, 360)
                : new Vector3(0, 0, -360);
        }
    }

    internal enum CircleSideRotate
    {
        Left,
        Right
    }
}
