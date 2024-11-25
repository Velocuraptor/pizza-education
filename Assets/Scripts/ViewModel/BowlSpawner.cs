using System.Collections.Generic;
using UnityEngine;
using View;

namespace ViewModel
{
    public class BowlSpawner : MonoBehaviour
    {
        [SerializeField] private Transform bowlContainer;
        [SerializeField] private Bowl bowlPrefab;

        private readonly Dictionary<int, Bowl> _bowlInstances = new();

        public void AddBowls(IEnumerable<int> ingredients)
        {
            foreach (var ingredient in ingredients) AddBowl(ingredient);
        }

        public void ClearBowls()
        {
            foreach (var ingredient in _bowlInstances.Keys) RemoveBowl(ingredient);
        }
        
        private void AddBowl(int ingredientIndex)
        {
            if (_bowlInstances.ContainsKey(ingredientIndex)) return;
            var bowl = Instantiate(bowlPrefab, bowlContainer);
            bowl.transform.position = GetSpawnPosition();
            bowl.Initialize(ingredientIndex);
            _bowlInstances.Add(ingredientIndex, bowl);
        }
        
        private void RemoveBowl(int ingredientIndex)
        {
            if (!_bowlInstances.TryGetValue(ingredientIndex, out var bowl)) return;
            Destroy(bowl);
            _bowlInstances.Remove(ingredientIndex);
        }

        private Vector3 GetSpawnPosition()
        {
            const float step = 0.3f;
            return bowlContainer.position - Vector3.right * (step * _bowlInstances.Count);
        }
    }
}