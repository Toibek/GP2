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


        ability = GetComponentInChildren<Ability>();
    }
    private void OnPause()
    {
        SetCharacter(settings);
    }
    private void OnMove(InputValue value)
    {
        movement.Move = value.Get<Vector2>();
    }
    private void OnJump()
    {
        movement.Jump();
    }
    private void OnPrimary()
    {
        ability.Primary();
    }
    private void OnSecondary()
    {
        ability.Secondary();
    }
    private void OnTertiary()
    {
        ability.Tertiary();
    }
}
