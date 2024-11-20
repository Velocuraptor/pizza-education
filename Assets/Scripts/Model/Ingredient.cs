using UnityEngine;

namespace Model
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;
        [SerializeField] private Color rawColor;
        [SerializeField] private Color doneColor;
        [SerializeField] private Color burnColor;
        
        public Material Material => renderer.material;
        public Material SharedMaterial => renderer.sharedMaterial;

#if UNITY_EDITOR
        [ContextMenu("InitData")]
        private void InitData()
        {
            renderer = GetComponent<Renderer>();
        }
#endif

        public Color GetColorBy(BakingState state) =>
            state switch
            {
                BakingState.Raw => rawColor,
                BakingState.Done => doneColor,
                BakingState.Burn => burnColor,
            };

        public void Bake(BakingState currentState, BakingState nextState, float value)
        {
            var color = Color.Lerp(GetColorBy(currentState), GetColorBy(nextState), value);
            Material.color = color;
        }
    }
}