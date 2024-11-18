using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class Pizza : MonoBehaviour
    {
        [SerializeField] private Transform souseLayer;
        [SerializeField] private Transform toppingLayer;

        private readonly int _souseIndex;
        private readonly Dictionary<int, List<GameObject>> _toppingInstances = new();

        public Transform SouseLayer => souseLayer;
        public Transform ToppingLayer => toppingLayer;
    }
}