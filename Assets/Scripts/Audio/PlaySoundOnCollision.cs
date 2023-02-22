using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    [SerializeField]
    private UseSound _useSound;
    [SerializeField]
    private Sound.Names _sound;
    [SerializeField]
    private FMODUnity.EventReference _refSound;
    [Header("Leave them empty if no sound should play on them")]

    [SerializeField]
    private string[] _tagsToPlaySoundOn = new string[0];

    [SerializeField]
    private LayerMask _layersToPlay = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _layersToPlay)
        {
            if (_useSound == UseSound.dropDownList)
                AudioManager.S_PlayOneShotSound(_sound);
            else
                AudioManager.S_PlayOneShotSound(_refSound);
            return;
        }
        if (_tagsToPlaySoundOn.Length != 0)
        {
            for (int i = 0; i < _tagsToPlaySoundOn.Length; i++)
            {
                if (collision.transform.CompareTag(_tagsToPlaySoundOn[i]))
                {
                    if (_useSound == UseSound.dropDownList)
                        AudioManager.S_PlayOneShotSound(_sound);
                    else
                        AudioManager.S_PlayOneShotSound(_refSound);
                    return;
                }
            }
        }
    }

    private enum UseSound
    {
        dropDownList,
        EventRef
    }
}
