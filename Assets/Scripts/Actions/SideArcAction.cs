using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArcAction : BaseAction
{
    [SerializeField] private int sideRange = 4;

    private float totalSpinAmount;

    public override string GetActionName()
    {
        return "Side Arcs";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> sideArcsPositions = new List<GridPosition>();
        
        for(int i = 1; i <= sideRange; i++)
        {
            //Going Right
            GridPosition testPositionRight = new GridPosition(unit.GetGridPosition().x + i, unit.GetGridPosition().z);
            if(!LevelGrid.Instance.IsValidGridPosition(testPositionRight))
            {
                continue;  //can't go further to the right
            }
            else
            {
                sideArcsPositions.Add(testPositionRight);
                for(int j = 1; j <= i; j++) 
                {
                    GridPosition up = new GridPosition(testPositionRight.x, testPositionRight.z + j);   //move it to separate method to avoid code repetition
                    GridPosition down = new GridPosition(testPositionRight.x, testPositionRight.z - j);
                    if(LevelGrid.Instance.IsValidGridPosition(up) && !sideArcsPositions.Contains(up))
                    {
                        sideArcsPositions.Add(up);
                    }
                    if(LevelGrid.Instance.IsValidGridPosition(down) && !sideArcsPositions.Contains(down))
                    {
                        sideArcsPositions.Add(down);
                    }
                }
            }

            //Going Left
            GridPosition testPositionLeft = new GridPosition(unit.GetGridPosition().x - i, unit.GetGridPosition().z);
            if(!LevelGrid.Instance.IsValidGridPosition(testPositionLeft))
            {
                continue;  //can't go further to the left
            }
            else
            {
                sideArcsPositions.Add(testPositionLeft);
                for(int k = 1; k <= i; k++)
                {
                    GridPosition up = new GridPosition(testPositionLeft.x, testPositionRight.z + k);
                    GridPosition down = new GridPosition(testPositionLeft.x, testPositionRight.z - k);
                    if(LevelGrid.Instance.IsValidGridPosition(up) && !sideArcsPositions.Contains(up))
                    {
                        sideArcsPositions.Add(up);
                    }
                    if(LevelGrid.Instance.IsValidGridPosition(down) && !sideArcsPositions.Contains(down))
                    {
                        sideArcsPositions.Add(down);
                    }
                }
            }
        }
        return sideArcsPositions;
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
