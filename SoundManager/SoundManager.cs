using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class PlaySoundEvent : UnityEvent<string, Transform> { }

public class SoundManager : DAE.Unity.Commons.SingletonMonoBehaviour<SoundManager>
{
    public List<EventComp> sounds = new List<EventComp>();
    private int _musicIntensity;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (EventComp comp in sounds)
        {
            comp.StartInitialising();
        }
        LoadEvents();
        sounds.Sort((a,b) => a.getName().CompareTo(b.getName()));
    }
    void Start()
    {
        foreach (EventComp _sound in sounds)
        {
            if (_sound.playOnStart) _sound.PlaySound(this.gameObject.transform);
        }
    }

    public static void PlaySound(string name, Transform t) => GetSound(name)?.PlaySound(t);
    public void StopSound(string name, Transform t)
    {
        foreach (var item in sounds)
        {
            if (item.getName() == name)
            {
                item.stopSound();
            }
        }
    }
        private static EventComp GetSound(string name)
    {
        if (!(name == "None"))
        {
            foreach (EventComp sound in Instance.sounds)
            {
                if (sound.getName() == name)
                {
                    return sound;
                }
            }     
        }
        return null;
    }

    private static void LoadEvents()
    {
        FMOD.Studio.Bank[] banks;
        FMODUnity.RuntimeManager.StudioSystem.getBankList(out banks); //get all the banks

        int eventCount;
        banks[1].getEventCount(out eventCount);                       // check how many events there are in bank 1 (not sure why its bank 1 and not 0

        FMOD.Studio.EventDescription[] events;
        banks[1].getEventList(out events);                            // Get all the events



        for (int i = 0; i < eventCount; i++)                          // Go over all the events and add them
        {

            string path;
            bool exists = false;
            events[i].getPath(out path);

            foreach (var item in Instance.sounds)
            {
                if(item.getName() == path)
                {
                    exists = true;
                }
            }
            if(!exists) { 
                EventComp comp = new EventComp();
                comp.SetEvent(FMODUnity.RuntimeManager.PathToEventReference(path));
                comp.StartInitialising();
                Instance.sounds.Add(comp);
            }
        }
    }
}
