using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AndGate : MonoBehaviour
{
    bool input1;
    bool input2;
    bool output;

    [SerializeField] UnityEvent<bool> OnValueChanged;
    public void SetInput1(bool set) { input1 = set; Check(); }
    public void SetInput2(bool set) { input2 = set; Check(); }

    void Check()
    {
        output = input1 && input2;
        OnValueChanged?.Invoke(output);
    }
}
