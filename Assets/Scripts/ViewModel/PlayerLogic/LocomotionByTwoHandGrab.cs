using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace ViewModel.PlayerLogic
{
    public class LocomotionByTwoHandGrab : MonoBehaviour
    {
        [SerializeField] private Transform cameraRig;

        [Header("Hands")]
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        [Header("GrabInteractors")] 
        [SerializeField] private HandGrabInteractor leftGrabInteractor;
        [SerializeField] private HandGrabInteractor rightGrabInteractor;

        private Vector3 _startGrabbingPoint;
        private bool _isRightHandGrabbing;
        private bool _isLeftHandGrabbing;
        private bool _isGrabbed;

        private void Update()
        {
            if (!_isGrabbed) return;
            var grabPosition = GetLocomotionPoint();
            var direction = grabPosition - _startGrabbingPoint;
            var newPositionCalculated = cameraRig.position - direction;
            var newPosition = new Vector3(newPositionCalculated.x, newPositionCalculated.y, newPositionCalculated.z);
            cameraRig.position = newPosition;
            _startGrabbingPoint = GetLocomotionPoint();
        }
    
        public void OnRightHandEmptyGrab(bool isActive)
        {
            _isRightHandGrabbing = isActive;
            CheckStartEndLocomotion();
        }

        public void OnLeftHandEmptyGrab(bool isActive)
        {
            _isLeftHandGrabbing = isActive;
            CheckStartEndLocomotion();
        }

        private void CheckStartEndLocomotion()
        {
            if (leftGrabInteractor.HasSelectedInteractable || rightGrabInteractor.HasSelectedInteractable) return;
            if (_isRightHandGrabbing && _isLeftHandGrabbing)
            {
                BeginLocomotionGrab();
                return;
            }

            if (!_isRightHandGrabbing && !_isLeftHandGrabbing) EndLocomotionGrab();
        }

        private void BeginLocomotionGrab()
        {
            _isGrabbed = true;
            _startGrabbingPoint = GetLocomotionPoint();
        }
    
        private void EndLocomotionGrab() => _isGrabbed = false;

        private Vector3 GetLocomotionPoint()
        {
            var leftHandPosition = leftHand.transform.position;
            var rightHandPosition = rightHand.transform.position;
            return Vector3.Lerp(leftHandPosition, rightHandPosition, 0.5f);
        }
    }
}