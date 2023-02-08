using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreeVariables
{
    /// <summary>
    /// if Creature is "Tamed"| Return: Bool
    /// </summary>
    public static string Tamed => "Tamed";

    /// <summary>
    /// Player after Check for player "Player" | Return: Transform 
    /// </summary>
    public static string Player = "Player";

    /// <summary>
    /// Player Dazed For Seconds "Dazed" | Return: float 
    /// </summary>
    public static string Dazed = "Dazed";

    /// <summary>
    /// Follow this Transform "FollowTransform" |Return Transform
    /// </summary>
    public static string FollowTransform = "FollowTransform";

    /// <summary>
    /// rigidbodys currentSmoothVelocity ref "CurrentVelocity" |Return Vector3
    /// </summary>
    public static string CurrentVelocity = "CurrentVelocity";

}
