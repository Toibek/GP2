using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<CharacterSO> Characters;
    [SerializeField] List<Vector3> spawnPoints;
    List<GameObject> players;
    void OnPlayerJoined(PlayerInput joined)
    {
        if (players == null) players = new();
        GameObject go = joined.gameObject;
        go.transform.position = spawnPoints[players.Count];
        ApplyCharacter(go, Characters[players.Count]);
        players.Add(go);
        GameManager.Instance.PlayerJoined(go);

        if (players.Count == 2)
            GetComponent<PlayerInputManager>().DisableJoining();
    }
    void OnPlayerLeft(PlayerInput left)
    {
        GameObject go = left.gameObject;
        if (players.Contains(go))
            players.Remove(go);
    }
    void ApplyCharacter(GameObject target, CharacterSO character)
    {
        for (int i = target.transform.childCount - 1; i >= 0; i--)
            Destroy(target.transform.GetChild(i).gameObject);

        Instantiate(character.prefab, target.transform.position, Quaternion.identity, target.transform);
        target.GetComponent<Player>().ApplySettings(character);
    }
}
