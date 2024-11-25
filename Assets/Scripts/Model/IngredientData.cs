using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public struct IngredientData
    {
        public string Name;
        public Ingredient Model;
        public bool IsSouse;
        public Sprite Thumbnail;
    }
}