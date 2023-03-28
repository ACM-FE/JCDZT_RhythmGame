using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Conductor : MonoBehaviour
{
    [Header("Setup")]
    public float audioDelayMS;
    AudioSource music;

    [Space(20)]
    public Song song;
    
    [Space(20)][Header("Debug")]
    public Image circle;
    // constructor for an actual song, easy to read from script
    class songData{
        public float bpm;
        public float spb;
        // (crotchets in a bar)
        public int cib;
        public songData(float Bpm, int cInBar=4) {
            bpm = Bpm;
            spb = 60/bpm;
            cib = cInBar;
        }
    }
    
    // parse basic numbers into our new class
    songData ParseSong(int bpm, int cInBar) {
        return new songData(bpm,cInBar);
    }

    // temporary prolly
    void Start(){music=GetComponent<AudioSource>();Conduct(song);}
    
    // alright let's figure out how the fuck this is gonna work
    void Conduct(Song song) {
        UnityEvent beatCall = new UnityEvent();
        beatCall.AddListener(Beat);
        float songPosition = 0.0f;
        float songPositionBeats=0.0f;
        float nextBeat = songPosition + song.cib;
        int bar = 1;
        int beat = 0;
        float startTime = (float)AudioSettings.dspTime;
        music.Play();
        
        IEnumerator clock() {
            while(music.isPlaying) {
                //yield return new WaitUntil(() => ((float)AudioSettings.dspTime-startTime)==music.time;)
                songPosition = music.time;
                nextBeat = songPosition+song.spb;
                songPositionBeats++;
                beat++;
                if (beat==song.cib+1) {beat=1;bar++;}
                print(bar+" "+beat);
                beatCall.Invoke();
                print(((float)AudioSettings.dspTime-startTime)-music.time);
                yield return new WaitForSecondsRealtime(song.spb-Time.deltaTime+(audioDelayMS/1000));
            }
        }
        StartCoroutine(clock());
    }

    void Beat() {;
        circle.enabled = !circle.enabled;
    }
}
