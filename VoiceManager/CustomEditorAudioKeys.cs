using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameDesign.DVV_FloraDomina;

[CustomEditor(typeof(LoadAudioTableKeys))]
public class CustomEditorAudioKeys : Editor
{
    public List<string> _choices = new List<string> { };
    int choiceIdx = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        var keys = target as LoadAudioTableKeys;
        foreach (var key in keys.myVoiceLineList.vcs)
        {
            _choices.Add(key._key);
        }
        keys.currentKey = _choices[choiceIdx];

        string[] temp = new string[_choices.Count];
        for (int i = 0; i < _choices.Count-1; i++)
        {
            temp[i] = _choices[i];
        }
        choiceIdx = EditorGUILayout.Popup(choiceIdx, temp);
        EditorUtility.SetDirty(keys);
    }
}
