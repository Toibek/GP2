using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    List<GameObject> players;
    void OnPlayerJoined(PlayerInput joined)
    {
        Debug.Log("player joined: " + joined);
        Debug.Log(joined.gameObject.GetComponent<Movement>());

        if (players == null) players = new();
        players.Add(joined.gameObject);
        if (players.Count == 1) joined.gameObject.AddComponent<Pitcher>();
        if (players.Count == 2) joined.gameObject.AddComponent<Catcher>();
    }
}
