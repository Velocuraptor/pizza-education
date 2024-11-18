using Model;
using Oculus.Interaction;
using UnityEngine;

namespace View
{
    public class IngredientGrabObject : MonoBehaviour
    {
        [SerializeField] private Transform model;
        [SerializeField] private PointableUnityEventWrapper grabbableUnityEventWrapper;
        
        private Vector3 _modelStartPosition;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private IngredientData _ingredient;
    
        public void Initialize(int ingredientIndex)
        {
            _ingredient = PizzaData.Instance.IngredientDataList.GetIngredientBy(ingredientIndex);
            InitModel();
        }

        private void InitModel()
        {
            if (model.childCount > 0) Destroy(model.GetChild(0));
            Instantiate(_ingredient.Model, model);
        }

        private void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            _modelStartPosition = model.localPosition;
            grabbableUnityEventWrapper.WhenSelect.AddListener(OnGrabbed);
            grabbableUnityEventWrapper.WhenRelease.AddListener(OnReleased);
        }

        private void OnGrabbed(PointerEvent pointerEvent)
        {
            model.position = pointerEvent.Pose.position;
        }
        
        private void OnReleased(PointerEvent pointerEvent)
        {
            model.localPosition = _modelStartPosition;
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
    }
}