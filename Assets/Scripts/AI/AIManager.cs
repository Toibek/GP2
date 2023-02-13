using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    #region singelton
    public static AIManager Instance;
    public static bool Exist => Instance != null;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            this.enabled = false;
    }
    #endregion

    public List<PebbleCreature> GetCreatures => _creatures;

    private List<PebbleCreature> _creatures = new List<PebbleCreature>();

    public void AddCreature(PebbleCreature creature)
    {
        _creatures.Add(creature);
    }

    public void RemoveCreature(PebbleCreature creature)
    {
        _creatures.Remove(creature);
    }
}
