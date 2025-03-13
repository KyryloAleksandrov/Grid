using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    private object gridObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        textMeshPro.text = gridObject.ToString();
    }

    public virtual void SetGridObject(object gridObject)

    {
        this.gridObject = gridObject;
        if(gridObject==null) Debug.Log($"Attempting to setup a GridDebugObject with a null GridObject");
        if(textMeshPro==null) Debug.Log($"TextmeshPro is not setup in the inspector");
    }
}
