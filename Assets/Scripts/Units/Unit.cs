using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 9;
    private GridPosition gridPosition;
    //private HealthSystem healthSystem;

    //private BaseAction[] baseActionArray;
    private int actionPoints = ACTION_POINTS_MAX;

    public static event EventHandler OnAnyActionPointsChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    [SerializeField] private bool isEnemy;

    private void Awake()
    {
        //baseActionArray = GetComponents<BaseAction>();
        //healthSystem = GetComponent<HealthSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        //TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        //healthSystem.OnDead += HealthSystem_OnDead;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);
        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }



    // Update is called once per frame
    void Update()
    {
        
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            //Unit changed GridPosition
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        } 

    }
    /*public T GetAction<T>() where T : BaseAction
    {
        foreach(BaseAction baseAction in baseActionArray)
        {
            if(baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }*/
    public override string ToString()
    {
        return name;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    /*public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }*/

    /*public bool TrySpendActionPoints(BaseAction baseAction)
    {
        if(CanSpendActionPoints(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }*/
    /*public bool CanSpendActionPoints(BaseAction baseAction)
    {
        if(actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }*/

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    /*private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        if((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
        
    }*/

    public bool IsEnemy()
    {
        return isEnemy;
    }

    /*internal void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }*/

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }


    /*public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }*/
}
