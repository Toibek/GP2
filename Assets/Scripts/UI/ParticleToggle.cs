using UnityEngine;
using UnityEngine.UI;

public class ParticleToggle : MonoBehaviour
{
    public GameObject particleSystemGO;
    public Toggle particleToggle;

    void Start()
    {
        particleToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool isOn)
    {
        particleSystemGO.SetActive(isOn);
    }
}
