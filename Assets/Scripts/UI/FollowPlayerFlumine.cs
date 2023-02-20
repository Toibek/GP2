using UnityEngine;

public class FollowPlayerFlumine : MonoBehaviour
{
    private GameObject Player1 => GameManager.Instance.Player1;

    void Update()
    {
        transform.position = Player1.transform.position;
    }
}