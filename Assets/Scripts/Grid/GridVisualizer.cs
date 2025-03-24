using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public static GridVisualizer Instance {get; private set;}
    [SerializeField] private Transform gridSystemVisualPrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;

    private GridVisualSingle [,] gridVisualSingleArray;
    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }
    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        RedSoft,
        Yellow
    }

    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GridVisualizer " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        gridVisualSingleArray = new GridVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);

                Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridVisualSingleArray[x,z] = gridSystemVisualSingleTransform.GetComponent<GridVisualSingle>();
            }   
        }

        UpdateGridVisual();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGridVisual();     //Make calls by events instead of Update
    }

    public void UpdateGridVisual()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnitActionController.Instance.GetSelectedUnit();
        BaseAction selectedAction = UnitActionController.Instance.GetSelectedAction();
        
        GridVisualType gridVisualType = GridVisualType.White;
        switch (selectedAction)
        {
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SideArcAction sideArcAction:
                gridVisualType = GridVisualType.Red;
                break;
            case DirectArcAction directArcAction:
                gridVisualType = GridVisualType.Blue;
                break;
        }
        ShowGridPositionList(selectedAction.GetValidActionGridPositionList(), gridVisualType);
    }

    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridVisualSingleArray[x,z].Hide();
            }   
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridVisualSingleArray[gridPosition.x,gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
        }
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
        {
            if(gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }
        Debug.Log("Couldn't find material");
        return null;
    }
}
