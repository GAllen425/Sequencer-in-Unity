using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public int Base = 4;
    public int Step = 4;
    public float BPM = 120;
    public float nextBPM = 120;
    public int CurrentStep = 1;
    public int CurrentMeasure = 0;

    private float interval;
    private float nextTime;

    public List<Block> blocks;
    public AudioClip[] clips = new AudioClip[2];
    private AudioSource[] audioSources = new AudioSource[2];

    public void StartMetronome()
    {
        GetBlockRowList();
        StopCoroutine("DoTick"); // stop any existing coroutine of the metronome
        CurrentStep = 1;
        var multiplier = Base / Step;
        var tmpInterval = 60f / BPM;
        interval = tmpInterval / multiplier;
        nextTime = Time.time;
        SetupAudioClips();
        StartCoroutine("DoTick");
    }

    private void GetBlockRowList()
    {
        blocks = new List<Block>();

        if (this.gameObject.GetComponent<BetterBlockRow>() != null)
        {
            for (int i=0; i < this.gameObject.GetComponent<BetterBlockRow>().columns.Count; i++)
            {
                blocks.Add(this.gameObject.GetComponent<BetterBlockRow>().columns[i].GetComponent<Block>());
            }
        }
    }

    IEnumerator DoTick()
    {
        for( ; ; )
        {
            //Debug.Log(CurrentStep);
            if (CurrentStep == 1)
                audioSources[0].Play();
            else
                audioSources[1].Play();
            blocks[CurrentStep - 1].BounceAnimation();

            nextTime += interval;
            yield return new WaitForSeconds(interval);
            CurrentStep++;
            if (CurrentStep > Step)
            {
                CurrentStep = 1;
                BPM = nextBPM;
                var multiplier = Base / Step;
                var tmpInterval = 60f / BPM;
                interval = tmpInterval / multiplier;
                CurrentMeasure++;
            }
        }
    }

    private void SetupAudioClips ()
    {
        clips[0] = (AudioClip)Resources.Load("DD_SmPerc5");
        clips[1] = (AudioClip)Resources.Load("DD_SmPerc10");
        audioSources[0] = this.gameObject.AddComponent<AudioSource>();
        audioSources[1] = this.gameObject.AddComponent<AudioSource>();
        audioSources[0].clip = clips[0];
        audioSources[1].clip = clips[1];
    }

}
