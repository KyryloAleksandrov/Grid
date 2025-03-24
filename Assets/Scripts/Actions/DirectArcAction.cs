using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectArcAction : BaseAction
{
    [SerializeField] private int directRange = 4;

    private float totalSpinAmount;
    public override string GetActionName()
    {
        return "Direct Arcs";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> directArcsPositions = new List<GridPosition>();
        for(int i = 1; i <= directRange; i++)
        {
            //Going Up
            GridPosition testPositionUp = new GridPosition(unit.GetGridPosition().x, unit.GetGridPosition().z + i);
            if(!LevelGrid.Instance.IsValidGridPosition(testPositionUp))
            {
                continue;  //can't go further to the up
            }
            else
            {
                directArcsPositions.Add(testPositionUp);
                for(int j = 0; j < i; j++) 
                {
                    GridPosition right = new GridPosition(testPositionUp.x + j, testPositionUp.z);   //move it to separate method to avoid code repetition
                    GridPosition left = new GridPosition(testPositionUp.x - j, testPositionUp.z);
                    if(LevelGrid.Instance.IsValidGridPosition(right) && !directArcsPositions.Contains(right))
                    {
                        directArcsPositions.Add(right);
                    }
                    if(LevelGrid.Instance.IsValidGridPosition(left) && !directArcsPositions.Contains(left))
                    {
                        directArcsPositions.Add(left);
                    }
                }
            }

            //Going Down
            GridPosition testPositionDown = new GridPosition(unit.GetGridPosition().x, unit.GetGridPosition().z - i);
            if(!LevelGrid.Instance.IsValidGridPosition(testPositionDown))
            {
                continue;  //can't go further to the down
            }
            else
            {
                directArcsPositions.Add(testPositionDown);
                for(int j = 0; j < i; j++) 
                {
                    GridPosition right = new GridPosition(testPositionDown.x + j, testPositionDown.z);   //move it to separate method to avoid code repetition
                    GridPosition left = new GridPosition(testPositionDown.x - j, testPositionDown.z);
                    if(LevelGrid.Instance.IsValidGridPosition(right) && !directArcsPositions.Contains(right))
                    {
                        directArcsPositions.Add(right);
                    }
                    if(LevelGrid.Instance.IsValidGridPosition(left) && !directArcsPositions.Contains(left))
                    {
                        directArcsPositions.Add(left);
                    }
                }
            }
        }


        return directArcsPositions;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) //currently it's the same as SpinAction for test purposes
    {
        totalSpinAmount = 0f;
        ActionStart(onActionComplete);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   //currently it's the same as SpinAction for test purposes
    {
        if(!isActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;
        if(totalSpinAmount >= 360f)
        {
            ActionComplete();
        }
    }
}
