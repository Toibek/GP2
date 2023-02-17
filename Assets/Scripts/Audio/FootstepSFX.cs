using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance druidFootsteps;
    private FMOD.Studio.EventInstance lapideFootsteps;
    [SerializeField] private FMODUnity.EventReference druidFootstepSound;
    [SerializeField] private FMODUnity.EventReference lapideFootstepSound;

    void PlayDruidFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(druidFootstepSound, this.gameObject);
        //druidFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps/Footsteps(Druid)");
        //druidFootsteps.start();
        //druidFootsteps.release();
    }

    void PlayLapideFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(lapideFootstepSound, this.gameObject);
        //lapideFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps/Footsteps(Druid)");
        //lapideFootsteps.start();
        //lapideFootsteps.release();
    }
}
