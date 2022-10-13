using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ParameterComp
{
    [SerializeField] private string name;

    public ParameterComp(string n) {name = n; }

    public void updateParameter(float value, FMOD.Studio.EventInstance eventRef)
    {
        eventRef.setParameterByName(name, value);
    }

    public string getName()
    {
        return name;
    }
}
