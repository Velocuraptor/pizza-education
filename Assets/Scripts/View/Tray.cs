using System;
using Oculus.Interaction;
using UnityEngine;

namespace View
{
    public class Tray : MonoBehaviour
    {
        [SerializeField] private Transform pizzaAnchor;
        
        private Pizza _pizza;
        
        public event Action PizzaOnTray;
        
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
            PizzaOnTray?.Invoke();
        }

        public void Restart()
        {
            _pizza = null;
        }
    }
}