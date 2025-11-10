using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    private GridPosition gridPosition;
    private Action onInteractComplete;
    private bool isGreen;
    private bool isActive;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        SetColorGreen();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        onInteractComplete();
        isActive = false;
    }

    private void SetColorGreen()
    {
        isGreen = true;
        meshRenderer.material = greenMaterial;
    }

    private void SetColorRed()
    {
        isGreen = false;
        meshRenderer.material = redMaterial;
    }

    public void Interact(Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;

        if (isGreen)
        {
            SetColorRed();
        }
        else
        {
            SetColorGreen();
        }
    }
}
