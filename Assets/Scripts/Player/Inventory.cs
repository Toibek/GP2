using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private InteractableFinder Interactables;
    private void Start()
    {
        Interactables = GetComponentInChildren<InteractableFinder>();
    }
    #region Input
    private void OnPrimary(InputValue value)
    {
        if (value.Get<float>() == 0)
            OnPrimaryUp();
        else
            OnPrimaryDown();
    }
    private void OnSecondary(InputValue value)
    {
        if (value.Get<float>() == 0)
            OnSecondaryUp();
        else
            OnSecondaryDown();
    }

    private void OnDrop()
    {

    }
    private void OnPrimaryDown()
    {
        if (Interactables.Closest != null)
        {
            Debug.Log("Interacting with: " + Interactables.Closest);
        }
    }
    private void OnPrimaryUp()
    {

    }
    private void OnSecondaryDown()
    {

    }
    private void OnSecondaryUp()
    {

    }
    #endregion
}
