using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameDesign.DVV_FloraDomina
{

    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "KeyLoader", menuName = "Loader", order = 1)]
    public class LoadAudioTableKeys : ScriptableObject
    {
        public string currentKey;
        public TextAsset textAssetData;
        [System.Serializable]

        public class Key
        {
            public string _key;
        }

        [System.Serializable]
        public class Keys
        {
            public Key[] vcs;
        }
        public void setCurrentKey(string key)
        {
            currentKey = key;
        }


        public Keys myVoiceLineList = new Keys();

        private void Awake()
        {
            ReadCsv();
        }

        private void OnValidate()
        {
            ReadCsv();
        }

        private void Reset()
        {
            ReadCsv();
        }
        void ReadCsv()
        {
            string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);          //Get all lines from the excel

            int tableSize = data.Length / 2 - 1;
            myVoiceLineList.vcs = new Key[tableSize];

            for (int i = 0; i < tableSize; i++)                                                                     //Go over them and split them according to various variables
            {
                myVoiceLineList.vcs[i] = new Key();
                myVoiceLineList.vcs[i]._key = data[2 * (i)];

            }

            
        }
    }
}
