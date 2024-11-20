using Model;
using UnityEngine;

namespace View
{
    public class IngredientIndicator : MonoBehaviour
    {
        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");

        public void Initialize(int ingredient)
        {
            var modelPrefab = PizzaData.Instance.IngredientDataList.GetIngredientBy(ingredient).Model;
            var model = Instantiate(modelPrefab, transform);
            var material = model.Material;
            material.SetInt(SrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt(DstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt(ZWrite, 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            var color = model.Material.color;
            color.a = 0.5f;
            model.Material.color = color;
        }
    }
}