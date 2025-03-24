using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    // Start is called before the first frame update
    void Start()
    {
        UnitActionController.Instance.OnSelectedUnitChanged += UnitActionController_OnSelectedUnitChanged;
        CreateActionButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreateActionButtons()
    {
        foreach(Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        Unit selectedUnit = UnitActionController.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitActionController_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateActionButtons();
    }
}
