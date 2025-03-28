using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseWorld : MonoBehaviour
{   
    private static MouseWorld instance;
    [SerializeField] private LayerMask mousePlaneLayerMask;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);

        /*Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mousePlaneLayerMask);
        transform.position = raycastHit.point;*/

        transform.position = MouseWorld.GetMouseWorldPosition();
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
