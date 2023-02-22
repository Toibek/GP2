using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayerMothis : MonoBehaviour
{
    private GameObject Player2 => GameManager.Instance.Player2;
    private GameObject Player1 => GameManager.Instance.Player1;

    private GameObject Target;

    private void OnEnable()
    {
        //GameManager.Instance.OnPlayerJoined += SetTarget;
        SetTarget();
    }

    private void SetTarget()
    {
        if (Player2.transform.GetChild(0).GetComponentsInChildren<Montis>() != null)
        {
            Debug.Log("1");
            Target = Player2.transform.GetChild(0).gameObject;
        }

        else if (Player1.transform.GetChild(0).GetComponentsInChildren<Montis>() != null)
        {
            Debug.Log("2");
            Target = Player1.transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.Log("Didn't get Target");
        }
    }

    void Update()
    {
        if (Target == null)
            return;
        transform.position = Target.transform.position;
    }
}