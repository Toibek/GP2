using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new SoundDatabase",menuName ="Sound/Database")]
public class SoundDatabase : ScriptableObject
{
    public Sound[] sounds;

    public Sound GetSound(Sound.Names nameOfSound)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == nameOfSound)
            {
                return sounds[i];
            }
        }
        return null;
    }

}
