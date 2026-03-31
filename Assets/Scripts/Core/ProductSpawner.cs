using System.Collections.Generic;
using UnityEngine;
using ContainerLoading.Algorithm;

public class ProductSpawner : MonoBehaviour
{
    private readonly List<GameObject> _spawnedObjects = new List<GameObject>();

    public void SpawnProducts(PackingResult result)
    {
        foreach (var go in _spawnedObjects)
            Destroy(go);
        _spawnedObjects.Clear();

        foreach (var placement in result.placements)
        {
            if (!placement.isValid) continue;

            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = placement.product.productName;
            go.transform.position = new Vector3(placement.position.x, placement.position.y + placement.product.height * 0.5f, placement.position.z);
            go.transform.rotation = placement.rotation;
            go.transform.localScale = new Vector3(placement.product.width, placement.product.height, placement.product.depth);

            var mat = new Material(Shader.Find("Standard"));
            mat.color = placement.product.gizmoColor;
            go.GetComponent<Renderer>().material = mat;

            _spawnedObjects.Add(go);
        }
    }
}
