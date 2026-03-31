using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ContainerLoading.Models;
using ContainerLoading.Algorithm;

public class ContainerManager : MonoBehaviour
{
    public ContainerData containerData;
    public List<DeliveryOrder> deliveryOrders;
    public Button packButton;

    private ProductSpawner _spawner;

    void Start()
    {
        CreateContainerVisual();
        _spawner = GetComponent<ProductSpawner>();
        packButton.onClick.AddListener(Pack);
    }

    void CreateContainerVisual()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(go.GetComponent<Collider>());
        go.transform.localScale = new Vector3(containerData.width, containerData.height, containerData.depth);
        go.transform.position = new Vector3(containerData.width / 2f, containerData.height / 2f, containerData.depth / 2f);

        var mat = new Material(Shader.Find("Standard"));
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
        mat.color = new Color(1f, 1f, 1f, 0.2f);
        go.GetComponent<Renderer>().material = mat;
    }

    void Pack()
    {
        var packingResult = ContainerPacker.Pack(containerData, deliveryOrders);
        foreach (var pr in packingResult.placements)
            Debug.Log($"{pr.product.productName}: pos={pr.position}, loadOrder={pr.loadOrder}");
        _spawner.SpawnProducts(packingResult);
    }
}
