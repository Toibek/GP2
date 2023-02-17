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

    internal EmptyDelegate OnToggleReady;
    internal IntDelegate OnCharacterChange;
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
    private void OnChangeCharacter(InputValue value)
    {
        int val = Mathf.RoundToInt(value.Get<float>());
        if (val != 0)
            OnCharacterChange?.Invoke(val);
    }
    private void OnSetReady()
    {
        OnToggleReady?.Invoke();
    }
    private void OnPause()
    {
        GameManager.Instance.Pause();
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
public delegate void EmptyDelegate();
public delegate void IntDelegate(int value);
