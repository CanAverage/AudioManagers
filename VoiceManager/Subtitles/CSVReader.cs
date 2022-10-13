using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;
    public C_TextAppeare subtitler;
    [System.Serializable]
    public class VoiceLine
    {
        public string Character;
        public string Actor;
        public string Text;
        public string FileName;
    }

    [System.Serializable]
    public class VoiceLineList
    {
        public VoiceLine[] vcs;
    }


    public VoiceLineList myVoiceLineList = new VoiceLineList();

    private void Start()
    {
        ReadCsv();
        subtitler.AddText(myVoiceLineList.vcs[0].Text);
    }
    void ReadCsv()
    {
        string[] data = textAssetData.text.Split(new string[] {";", "\n" }, StringSplitOptions.None);           //Get all lines from the excel

        int tableSize = data.Length / 4 - 1;
        myVoiceLineList.vcs = new VoiceLine[tableSize];

        for (int i = 0; i < tableSize; i++)                                                                     //Go over them and split them according to various variables
        {
            myVoiceLineList.vcs[i] = new VoiceLine();
            myVoiceLineList.vcs[i].Character = data[4 * (i + 1)];
            myVoiceLineList.vcs[i].Actor = data[4 * (i + 1) + 1];
            myVoiceLineList.vcs[i].Text = data[4 * (i + 1) + 2];
            myVoiceLineList.vcs[i].FileName = data[4 * (i + 1) + 3];

        }
    }
}
