using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonUIList;

    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += Instance_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += Instance_OnActionStarted;
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    //private void Instance_OnTurnChanged(object sender, System.EventArgs e)
    //{
    //    UpdateActionPoints();
    //}

    private void Instance_OnActionStarted(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    private void Instance_OnSelectedActionChanged(object sender, System.EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void Instance_OnSelectedUnitChanged(object sender, System.EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);

            actionButtonUIList.Add(actionButtonUI);
        }
    }

    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }
}
