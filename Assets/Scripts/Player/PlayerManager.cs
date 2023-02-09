using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnAllPlayersJoined;
    [SerializeField] private List<CharacterSO> Characters;
    [SerializeField] private Vector3[] spawnPoints = new Vector3[2];
    private List<PlayerInput> PlayerInputs;
    private List<GameObject> players;
    private void Start()
    {
        GetComponent<GameManager>().OnGameStart += SetUpCharacters;
    }
    private void OnPlayerJoined(PlayerInput joined)
    {
        if (PlayerInputs == null) PlayerInputs = new();
        PlayerInputs.Add(joined);
        if (PlayerInputs.Count == 2)
        {
            OnAllPlayersJoined?.Invoke();
            GetComponent<PlayerInputManager>().DisableJoining();
        }
    }
    private void OnPlayerLeft(PlayerInput left)
    {
        GameObject go = left.gameObject;
        if (players.Contains(go))
            players.Remove(go);
    }
    private void ApplyCharacter(GameObject target, CharacterSO character)
    {
        for (int i = target.transform.childCount - 1; i >= 0; i--)
            Destroy(target.transform.GetChild(i).gameObject);

        Instantiate(character.prefab, spawnPoints[players.Count], Quaternion.identity, target.transform);
        target.GetComponent<Player>().SetCharacter(character);
    }
    private void SetUpCharacters()
    {
        if (players == null) players = new();
        GetComponent<PlayerInputManager>().DisableJoining();
        for (int i = 0; i < PlayerInputs.Count; i++)
        {
            GameObject go = PlayerInputs[i].gameObject;
            go.name = Characters[players.Count].name;
            ApplyCharacter(go, Characters[players.Count]);
            players.Add(go);
            GameManager.Instance.PlayerJoined(go.transform.GetChild(0).gameObject);
            PlayerInputs[i].SwitchCurrentActionMap("Player");
        }
    }
}
