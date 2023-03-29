#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Composing;
using UnityEditor;


public static class midiToBM{

    // Start is called before the first frame update
    public static notePosition[] MakeBeatmap(Object midiFile)
    {      
        var file = MidiFile.Read(AssetDatabase.GetAssetPath(midiFile));
        var tempoMap = file.GetTempoMap();
        
        IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes = file.GetNotes();
        List<notePosition> newMap = new List<notePosition>();


        int quaver = -1;
        foreach (var note in notes) {
            if (note.Time/240 != quaver+1) {
                //int difference = (int)(note.Time/240) - quaver+1;
                //print(difference);
                //print("gap");
                for (int i = 0; i<((int)(note.Time/240)-(quaver+1)); i++) {
                    newMap.Add(notePosition.none);
                }
            } else {
                //print("no gap");
            }
            //quaver
            newMap.Add(notePosition.both);
            quaver= (int)(note.Time/240);
        }
        foreach (notePosition n in newMap) {
            //print (n);
        }

        return newMap.ToArray();
    }
}
#endif