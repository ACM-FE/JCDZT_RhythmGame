using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
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
    public float beatsTotal;

    void OnValidate() {
        spb = 60/bpm;
        length = song.length;
        beatsTotal = (int)length/spb;
    }
}