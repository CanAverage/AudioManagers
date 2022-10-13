using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundComp
{
    private FMOD.Studio.EventInstance eventInstance;
    [SerializeField] private List<ParameterComp> Parameters = new List<ParameterComp>();
    [SerializeField] private Transform location;
    
    public void PlaySound()
    {
        eventInstance.start();
    }

    public FMOD.Studio.EventInstance getEventInstance()
    {
        return eventInstance;
    }

    public void SetEventInstance(FMOD.Studio.EventInstance ei)
    {
        eventInstance = ei;
        LoadParameters();
    }

    public Transform getLocation()
    {
        return location;
    }

    public void SetLocation(Transform t)
    {
        location = t;
        eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(location));
    }


    public void StopSound()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    private void LoadParameters()
    {
        FMOD.Studio.EventDescription desc;
        eventInstance.getDescription(out desc);
        int amount;
        desc.getInstanceCount(out amount);              //Get the amount of parameters
        for (int i = 0; i < amount; i++)
        {
            FMOD.Studio.PARAMETER_DESCRIPTION parameterDesc;
            desc.getParameterDescriptionByIndex(i, out parameterDesc);
            Parameters.Add(new ParameterComp((string)parameterDesc.name));              //Add them to the list
        }
    }
}
