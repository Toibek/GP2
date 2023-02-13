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
    public void SetCharacter(CharacterSO settings)
    {
        this.settings = settings;
        movement = GetComponentInChildren<Movement>();
        movement.settings = settings;

        GetComponentInChildren<Rigidbody>().mass = settings.Mass;

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
        SetCharacter(settings);
    }
}
