using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance druidFootstepSFX;
    private FMOD.Studio.EventInstance druidFootsteps;
    private FMOD.Studio.EventInstance lapideFootsteps;
    //[SerializeField] private FMODUnity.EventReference druidFootstepSound;
    //[SerializeField] private FMODUnity.EventReference lapideFootstepSound;

    void PlayDruidFootstep()
    {
        //FMODUnity.RuntimeManager.PlayOneShotAttached(druidFootstepSound, this.gameObject);

        druidFootstepSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps/Footsteps(Druid)");
        druidFootstepSFX.set3DAttributes((FMODUnity.RuntimeUtils.To3DAttributes(gameObject)));
        druidFootstepSFX.start();
        druidFootstepSFX.release();

    }

    void PlayLapideFootstep()
    {
        //FMODUnity.RuntimeManager.PlayOneShotAttached(lapideFootstepSound, this.gameObject);
        druidFootstepSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps/Footsteps(Lapide)");
        druidFootstepSFX.set3DAttributes((FMODUnity.RuntimeUtils.To3DAttributes(gameObject)));
        druidFootstepSFX.start();
        druidFootstepSFX.release();
    }
}
