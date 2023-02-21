using UnityEngine.Audio;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD;

[System.Serializable]
public class Sound
{

    public Names name;
    public EventReference eventRef;

    public void PlayOneShot(Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventRef,position);
    }

    public void PlayOneShot(GameObject target)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventRef, target);
    }

    public enum Names
    {
        Unassigned,
        Music_AmbientPads,
        Music_MainMusic,
        SFX_Ambient_Cavern,
        SFX_Ambient_Main,
        SFX_Ambient_SnowyPeak,
        SFX_Druids_DruidJump,
        SFX_Druids_RockPush,
        SFX_Environment_DrawBridgeSlam,
        SFX_Environment_MagicalChimes,
        SFX_Environment_River,
        SFX_Environment_StoneDoorOpening,
        SFX_Environment_StoneDoorClosing,
        SFX_Environment_waterSplash,
        SFX_Environment_StonePlatformMove,
        SFX_Footsteps_FootstepsDruid,
        SFX_Footsteps_FootstepsLapide,
        SFX_Lapide_LapidCollision,
        SFX_Lapide_LapideHitReaction,
        SFX_Lapide_LapideOneShot,
        SFX_Respawn,
        UI_SelectNegative,
        UI_SelectPositive
    }

    public enum Type
    {
        Unassigned,
        Master,
        Ambient,
        EnviromentFX,
        Footsteps,
        UI
    }
}