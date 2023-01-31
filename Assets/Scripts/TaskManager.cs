using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static bool CreateTask()
    {
        if (false)
        {
            return true;
        }

        return false;
    }

    public static bool SendInTask()
    {
        if (false)
        {
            return true;
        }

        return false;
    }
}
