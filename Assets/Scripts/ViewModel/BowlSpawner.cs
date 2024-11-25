using System.Collections.Generic;
using System.Linq;
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
            var ingredients = _bowlInstances.Keys.ToList();
            for (var i = 0; i < ingredients.Count; i++) RemoveBowl(ingredients[i]);
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
            Destroy(bowl.gameObject);
            _bowlInstances.Remove(ingredientIndex);
        }

        private Vector3 GetSpawnPosition()
        {
            const float step = 0.3f;
            return bowlContainer.position - Vector3.right * (step * _bowlInstances.Count);
        }
    }
}