using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Conductor : MonoBehaviour
{
    [Header("Setup")]
    public float audioDelayMS;
    public float scriptDelayMS;
    AudioSource music;
    public GameObject note;
    public Text scoreText;
    public float noteOffset;
    public Transform playhead;
    int score;

    [Space(20)]
    public Song song;

    //debug vars
    Collider2D col;


    // temporary prolly
    void Start(){music=GetComponent<AudioSource>();col = GetComponent<Collider2D>();Conduct(song);}
    
    // alright let's figure out how the fuck this is gonna work
    void Conduct(Song song) {
        UnityEvent beatCall = new UnityEvent();
        
        float songPosition = 0.0f;
        int songPositionBeats=0;
        int songPositionEighths=0;
        float nextBeat = songPosition + song.cib;

        int bar = 1;
        int beat = 1;
        int eighth = 1;

        float startTime = (float)AudioSettings.dspTime;
        music.clip = song.song;
        music.PlayDelayed(audioDelayMS/1000);
        spawn();

        IEnumerator clock() {
            float tm = Time.time;
            float atm = (float)AudioSettings.dspTime;
            print(tm-atm);
            while(music.isPlaying) {
                tm = Time.time;
                atm = (float)AudioSettings.dspTime;

                //yield return new WaitUntil(() => ((float)AudioSettings.dspTime-startTime)==music.time;)
                //print(song.map[songPositionBeats]);
                //print(bar+" "+beat+" ");
                //Beat(beat,songPositionBeats);               
                songPosition = music.time;
                nextBeat = songPosition+song.spb;
                songPositionBeats++;
                beat++;
                if (beat==song.cib+1) {beat=1;bar++;}
                
                //Eighth(songPositionEighths);
                //print(songPositionEighths);
                //spawn(song.map[songPositionEighths+8]);
                songPositionEighths++;

                //print(((float)AudioSettings.dspTime-startTime)-music.time);
                yield return new WaitForSeconds(((song.spb)/2));

                //spawn(song.map[songPositionEighths+8]);
                //print(songPositionEighths);
                songPositionEighths++;


                yield return new WaitForSeconds(((song.spb)/2));
                
                //yield return new WaitForSecondsRealtime(scriptDelayMS/1000);


            }
        }
        //StartCoroutine(clock());
    }

    void Beat(int beat,int songPositionBeats) {;
        //circle.enabled = !circle.enabled;
        //spawn(song.map[songPositionBeats+4]);
    }

    void Eighth(int songPositionEighths) {
        //print(songPositionEighths+8);
        //spawn(song.map[songPositionEighths+10]);
    }

    void spawn() {
        float noteVelocity = 4.9f;
        //Transform playhead = new GameObject("Playhead").transform;
        float distance = noteVelocity*song.spb;
        foreach (notePosition n in song.map) {
            
            float offset = 0;
            switch (n) {
                case notePosition.left:
                    offset -= 2;
                    break;

                case notePosition.right:
                    offset += 2;
                    break;
            }

            playhead.Translate(new Vector3(offset,distance,0));
            
            if (n != notePosition.none) {
                Instantiate(note,playhead.position,playhead.rotation);
            }

            playhead.Translate(new Vector3(-offset,0,0));
        }
    }

    public void Play(InputAction.CallbackContext context) {
        Collider2D[] notes = new Collider2D[1];
        col.OverlapCollider(new ContactFilter2D(),notes);
        if (notes[0] != null) {
            score=score+2;
            //print(score);
            scoreText.text=score.ToString();
            Destroy(notes[0].gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.activeSelf) {
            Destroy(col.gameObject);
            score--;
            scoreText.text=score.ToString();
        }
    }
}
