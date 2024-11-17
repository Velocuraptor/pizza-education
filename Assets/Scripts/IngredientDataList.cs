using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "IngredientList", menuName = "Pizza/IngredientList", order = 0)]
    public class IngredientDataList : ScriptableObject
    {
        [SerializeField] private IngredientData[] ingredients;

        public IReadOnlyList<IngredientData> Ingredients => ingredients;
        
        [Serializable]
        public struct IngredientData
        {
            public string Name;
            public GameObject Model;
        }
    }
}