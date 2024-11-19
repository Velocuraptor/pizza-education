using System;
using Model;
using Oculus.Interaction;
using UnityEngine;

namespace View
{
    public class IngredientGrabObject : MonoBehaviour
    {
        [SerializeField] private Transform modelContainer;
        [SerializeField] private PointableUnityEventWrapper grabbableUnityEventWrapper;
        
        private Vector3 _modelStartPosition;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private int _ingredient;
        private Pizza _pizza;
    
        public void Initialize(int ingredientIndex)
        {
            _ingredient = ingredientIndex;
            InitModel();
        }

        private void InitModel()
        {
            var model = PizzaData.Instance.IngredientDataList.GetIngredientBy(_ingredient).Model;
            if (modelContainer.childCount > 0) Destroy(modelContainer.GetChild(0));
            Instantiate(model, modelContainer);
        }

        private void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            _modelStartPosition = modelContainer.localPosition;
            grabbableUnityEventWrapper.WhenSelect.AddListener(OnGrabbed);
            grabbableUnityEventWrapper.WhenRelease.AddListener(OnReleased);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.rigidbody.TryGetComponent<Pizza>(out var pizza)) return;
            _pizza = pizza;
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.rigidbody.TryGetComponent<Pizza>(out var pizza)) return;
            if (_pizza == pizza) _pizza = null;
        }

        private void OnGrabbed(PointerEvent pointerEvent)
        {
            modelContainer.position = pointerEvent.Pose.position;
        }
        
        private void OnReleased(PointerEvent pointerEvent)
        {
            if (_pizza)
            {
                _pizza.AddIngredient(_ingredient, modelContainer.position);
                _pizza = null;
            }
            
            modelContainer.localPosition = _modelStartPosition;
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
    }
}