using UnityEngine;

public class Bowl : MonoBehaviour
{
    [SerializeField] private Transform ingredientsContainer;
    
    private GameObject _ingredientModel;
    
    public void Fill()
    {
        const int countIngredients = 50;
        const float spawnRadius = 0.08f;
        var rotationVariance = new Vector3(10f, 10f, 10f);

        ClearFilling();
        for (var i = 0; i < countIngredients; i++)
        {
            var ingredient = Instantiate(_ingredientModel, ingredientsContainer);
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

    public void ClearFilling()
    {
        foreach (GameObject ingredient in ingredientsContainer) Destroy(ingredient);
    }
}
