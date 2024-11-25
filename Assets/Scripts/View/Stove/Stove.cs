using System;
using System.Collections;
using Oculus.Interaction;
using UnityEngine;
using ViewModel;

namespace View.Stove
{
    public class Stove : MonoBehaviour
    {
        [SerializeField] private Display display;
        [SerializeField] private Transform pizzaAnchor;

        [Header("Handles")]
        [SerializeField] private PointableUnityEventWrapper tempHandle;
        [SerializeField] private PointableUnityEventWrapper timeHandle;
        [SerializeField] private PointableUnityEventWrapper door;

        private bool _isDoorOpen;
        private float _temperature;
        private float _time;
        private Pizza _pizza;
        private Coroutine _baking;
        private readonly WaitForSeconds _waitOneSecond = new(1.0f);

        private void Start()
        {
            InitHandles();
            display.SetTemperature((int)_temperature);
            display.SetTime((int)_time);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null || !other.attachedRigidbody.transform.TryGetComponent<Pizza>(out var pizza)) return;
            _pizza = pizza;
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.LockKinematic();
            _pizza.Released += AttachPizza;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody == null || !other.attachedRigidbody.transform.TryGetComponent<Pizza>(out var pizza)) return;
            if (_pizza == null || _pizza != pizza) return;
            _pizza.Released -= AttachPizza;
            other.attachedRigidbody.useGravity = true;
            other.attachedRigidbody.UnlockKinematic();
            _pizza = null;
        }

        private void AttachPizza()
        {
            _pizza.transform.position = pizzaAnchor.position;
            _pizza.transform.rotation = Quaternion.identity;
            _pizza.Released -= AttachPizza;
        }

        private void InitHandles()
        {
            tempHandle.WhenMove.AddListener(UpdateTemperature);
            tempHandle.WhenUnselect.AddListener(UpdateTemperature);
            timeHandle.WhenMove.AddListener(UpdateTime);
            timeHandle.WhenUnselect.AddListener(UpdateTime);
            door.WhenUnselect.AddListener(UpdateDoorState);
        }

        private void UpdateTemperature(PointerEvent _)
        {
            _temperature = tempHandle.transform.localEulerAngles.z;
            display.SetTemperature((int)_temperature);
            TryStartBaking();
        }

        private void UpdateTime(PointerEvent _)
        {
            _time = timeHandle.transform.localEulerAngles.z;
            display.SetTime((int)_time);
            TryStartBaking();
        }

        private void UpdateDoorState(PointerEvent _)
        {
            const float doorCloseAngle = 5.0f; 
            _isDoorOpen = door.transform.localEulerAngles.x > doorCloseAngle;
            TryStartBaking();
        }

        private void TryStartBaking()
        {
            if (_baking != null) return;
            if (_pizza == null) return;
            if (_isDoorOpen || _temperature == 0 || _time == 0) return;
            _baking = StartCoroutine(Baking());
        }

        private void StopBaking()
        {
            if (_baking == null) return;
            StopCoroutine(_baking);
            _baking = null;
        }

        private IEnumerator Baking()
        {
            while (!_isDoorOpen && _time > 0.0f)
            {
                yield return _waitOneSecond;
                DecreaseTime(1.0f);
                var recipeTemperature = RecipeController.Instance.CurrentRecipe.Temperature;
                var recipeTime = RecipeController.Instance.CurrentRecipe.Time;
                var increment = 100.0f / recipeTime * (_temperature / recipeTemperature);
                _pizza.Bake(increment);
            }

            _baking = null;
        }

        private void DecreaseTime(float decrement)
        {
            _time -= decrement;
            if (_time < 0.0f) _time = 0.0f;
            display.SetTime((int)_time);
            var eulerAngles = timeHandle.transform.localEulerAngles;
            eulerAngles.z = _time;
            timeHandle.transform.localEulerAngles = eulerAngles;
        }
    }
}
