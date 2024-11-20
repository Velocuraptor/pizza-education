using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View
{
    public class Bowl : MonoBehaviour
    {
        [SerializeField] private Transform ingredientsContainer;
        [SerializeField] private IngredientGrabObject[] ingredientGrabObjects;
        [SerializeField] private Shader maskedShader;
        
        private IngredientData _ingredient;

        public void Initialize(int ingredientIndex)
        {
            _ingredient = PizzaData.Instance.IngredientDataList.GetIngredientBy(ingredientIndex);
            foreach (var ingredientGrab in ingredientGrabObjects) 
                ingredientGrab.Initialize(ingredientIndex);
            Fill();
        }
        
        private void Fill()
        {
            var model = _ingredient.Model;
            if (_ingredient.IsSouse)
            {
                var ingredient = Instantiate(model, ingredientsContainer);
                ingredient.Material.shader = maskedShader;
                ingredient.transform.localScale *= 2;
            }
            else
            {
                const int countIngredients = 50;
                const float spawnRadius = 0.08f;
                var rotationVariance = new Vector3(10f, 10f, 10f);

                ClearFilling();
                for (var i = 0; i < countIngredients; i++)
                {
                    var ingredient = Instantiate(model, ingredientsContainer);
                    ingredient.Material.shader = maskedShader;
                    ingredient.transform.localPosition = new Vector3(
                        Random.Range(-spawnRadius, spawnRadius),
                        0,
                        Random.Range(-spawnRadius, spawnRadius)
                    );
                    ingredient.transform.localRotation = Quaternion.Euler(
                        Random.Range(-rotationVariance.x, rotationVariance.x),
                        Random.Range(-rotationVariance.y, rotationVariance.y),
                        Random.Range(-rotationVariance.z, rotationVariance.z)
                    );
                }
            }
        }

        private void ClearFilling()
        {
            foreach (GameObject ingredient in ingredientsContainer) Destroy(ingredient);
        }
    }
}
