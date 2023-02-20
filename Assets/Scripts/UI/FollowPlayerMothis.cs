using UnityEngine;
using UnityEngine.InputSystem;
public class FollowPlayerMothis : MonoBehaviour
{
    private GameObject Player2 => GameManager.Instance.Player2;


    void Update()
    {
        transform.position = Player2.transform.position;
    }
}