using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelGrid : MonoBehaviour
   
{
    public static LevelGrid Instance {get; private set;}


    [SerializeField]private Transform gridDebugObjectPrefab;
    private GridSystem<GridObject> gridSystem;

    public event EventHandler OnAnyUnitMovedGridPosition;
    
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    
    private void Awake()
    {
        gridSystem = new GridSystem<GridObject>(width, height, cellSize, (GridSystem<GridObject> gridSystem, GridPosition gridPosition) => new GridObject(gridSystem, gridPosition));
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        if (Instance != null)
        {
            Debug.LogError("More than one LevelGrid " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this; 

        
    }

    private void Start()
    {
        Pathfinding.Instance.Setup(width, height, cellSize);
    }
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject= gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    /*public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }*/

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition); //=> means return, it is lambda expression

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);


    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidth();

    public int getHeight() => gridSystem.GetHeight();

    
}
