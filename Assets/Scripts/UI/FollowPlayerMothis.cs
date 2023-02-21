using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayerMothis : MonoBehaviour
{
    private GameObject Player2 => GameManager.Instance.Player2;
    private GameObject Player1 => GameManager.Instance.Player1;

    private GameObject Target;

    private void Start()
    {
        GameManager.Instance.OnGameStart += () => SetTarget();
    }

    private void SetTarget()
    {
        if (Player2.transform.GetChild(1).GetComponentInChildren<Montis>() != null)
        {
            Target = Player2;
        }

        else if (Player1.transform.GetChild(0).GetComponentInChildren<Montis>() != null)
        {
            Target = Player1;
        }
    }

    void Update()
    {
        if (Target == null)
            return;
        transform.position = Target.transform.position;
        Debug.Log(Target);
    }
}