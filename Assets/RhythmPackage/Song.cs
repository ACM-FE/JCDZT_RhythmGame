using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
#endif

public enum notePosition {
    none,
    left,
    right,
    both
}

#if UNITY_EDITOR
[System.Serializable]
[CustomEditor(typeof(Song))]
public class beatMapper : Editor {
    public override VisualElement CreateInspectorGUI()
    {   
        VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/RhythmPackage/beatMapper.uxml");
        VisualElement ui = uiAsset.Instantiate();
        return ui;
    }
}
#endif
[CreateAssetMenu(fileName = "Song", menuName = "Song", order = 1)]
public class Song : ScriptableObject
{
    [Header("Info")]
    public string Title;
    public string Artist;
    public string Album;


    [Space(20)]
    [Header("Tempo")]
    public float bpm;
    public float spb;

    [Space(20)]
    [Header("Time Signature")]
    // (crotchets in a bar)
    public int cib;

    [Space(20)]
    [Header("Audio")]
    public AudioClip song;
    public float length;
    public int eighthsTotal;
    public Object mapFile;
    public notePosition[] map;

    #if UNITY_EDITOR
    void OnValidate() {
        if (mapFile!=null) {map = midiToBM.MakeBeatmap(mapFile);}
        spb = 60/bpm;
        length = song.length;
        eighthsTotal = (int)(length/spb)*2;
    }
    #endif
}

