using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BeatMapper : EditorWindow
{
    bool songSelected;
    Song song;
    private GUIStyle style;

    notePosition np;

    void OnEnable() {
        style = new GUIStyle();
        style.normal = new GUIStyleState();
        style.normal.textColor = Color.white;
        style.normal.background = Texture2D.grayTexture;
        style.alignment = TextAnchor.MiddleCenter;
        style.margin = new RectOffset(20,20,10,0);
    }


    [MenuItem("Rosa/Beat Mapper")]
    static void Init() {
        BeatMapper window = (BeatMapper)EditorWindow.GetWindow(typeof(BeatMapper));
        window.Show(); 
    }
    private void OnSelectionChange() {
        // get type  of asset selected
        string guid = Selection.assetGUIDs[0];
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
        Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
        if (asset == null) {songSelected=false;return;}
        System.Type assetType = asset.GetType();
        if (assetType == typeof(Song)) {songSelected=true;song=(Song)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Song));}
        else {songSelected=false;}
    }

    void OnGUI(){
        if (!songSelected) {
            GUILayout.Label("Select a song to start Mapping!");
        } else {
            GUILayout.Label("Rosa's Super Cool Mapping Tool v0.1",style);
            EditorGUILayout.Space();
            GUILayout.Label("Current Song: "+song.Title,EditorStyles.boldLabel);
            EditorGUILayout.Space();
            notePosition beat = (notePosition)EditorGUILayout.EnumPopup(np);
            for (int i = 0; i <= song.beatsTotal; i++) {
                
                song.map[i] = beat;
            }
        }
    }
}
