using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EventComp
{
    [SerializeField] private string name;
    [SerializeField] private FMOD.Studio.EventInstance eventInstance;
    private FMODUnity.EventReference soundEventRef;
    [SerializeField] private List<SoundComp> eventInstances = new List<SoundComp>();
    [HideInInspector]
    public bool playOnStart = false;
    public void PlaySound(Transform loc)
    {
        bool hasCreatedInstance = false;
        foreach (var item in eventInstances)
        {
            if(item.getLocation() == loc)
            {
                item.PlaySound();
                hasCreatedInstance = true;
            }
        }
        if(!hasCreatedInstance)
        {
            SoundComp newSoundComp = new SoundComp();                       //Create a new event (using the ID for performance)
            newSoundComp.SetEventInstance(FMODUnity.RuntimeManager.CreateInstance(soundEventRef.Guid));
            eventInstances.Add(newSoundComp);
            newSoundComp.SetLocation(loc);
            newSoundComp.PlaySound();
            
        }
    }

    public void SetEvent(FMODUnity.EventReference sound) => soundEventRef = sound;

    public string getName()
    {
        return name;
    }
    public void StartInitialising( )
    {
        FMODUnity.RuntimeManager.GetEventDescription(soundEventRef).getPath(out name);
    }

    public void stopSound()
    {
        foreach (var item in eventInstances)
        {
            item.StopSound();
        }
    }
}
