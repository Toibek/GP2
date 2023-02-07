using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Movement movement;
    Ability ability;
    PlayerManager manager;
    CharacterSO settings;
    public void Init(PlayerManager man)
    {
        manager = man;
    }
    public void ApplySettings(CharacterSO settings)
    {
        this.settings = settings;
        movement = GetComponent<Movement>();
        movement.settings = settings;

        GetComponent<Rigidbody>().mass = settings.Mass;

        ability = GetComponentInChildren<Ability>();
    }

    private void OnMove(InputValue value)
    {
        movement.Move = value.Get<Vector2>();
    }
    private void OnPrimary(InputValue value)
    {
        float input = value.Get<float>();
        if (input == 1) ability.AbilityStart();
        else ability.AbilityStop();
    }
    private void OnPause()
    {
        ApplySettings(settings);
    }
}
