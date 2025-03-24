using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    public static UnitActionController Instance {get; private set;}

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    private static UnitActionController instance;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    
    private BaseAction selectedAction;
    private bool isBusy;
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UnitActionController " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetSelectedUnit(selectedUnit);
    }

   
    // Update is called once per frame
    void Update()
    {
        if(isBusy)
        {
            return;
        }
        if( TryHandleUnitSelection()) 
        {
            return;
        }
        
        HandleSelectedAction();
    }
    private void HandleSelectedAction()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMouseWorldPosition());

            if(!selectedAction.IsValidGridPosition(mouseGridPosition))
            {
                return;
            }
            
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool TryHandleUnitSelection()
    {
        //LayerMask unitLayerMask = MouseService.GetFirstLayerMask();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.unitLayerMask))
        {
            /*if(selectedUnit != null)
            {
                selectedUnit.Deselect();
            }*/

            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                /*if(unit == selectedUnit)
                {
                    selectedUnit.Deselect();
                    selectedUnit = null;
                    return true;
                }*/
                selectedUnit = unit;
                //selectedUnit.Select();
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());
        Debug.Log(unit);
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        Debug.Log(baseAction);
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty); 
    }

    private void SetBusy()
    {
        isBusy = true;

        OnBusyChanged?.Invoke(this, isBusy);
    }
    private void ClearBusy()
    {
        isBusy = false;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    public Unit GetSelectedUnit(){
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
