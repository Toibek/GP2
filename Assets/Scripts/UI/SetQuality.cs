using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

public class SetQuality : MonoBehaviour
{
    [SerializeField] private Dropdown qualityDropdown;

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }
}
