using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
