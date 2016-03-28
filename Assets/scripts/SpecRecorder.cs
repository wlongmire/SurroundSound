using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BeatDetectorRecorder : BeatDetector {

	public BeatDetectorRecorder (int subdivs) : base(subdivs){
		
	}


	public void addSample(SongData sd) {
		sd.totalTimesteps++;

		float[] timeStep = new float[base.samples.Length];


		for (int i = 0; i < base.samples.Length; i++) {

			sd.sampleMax [i] = (sd.sampleMax [i] < base.samples [i]) ? base.samples [i] : sd.sampleMax [i];
			sd.sampleMin [i] = (sd.sampleMin [i] > base.samples [i]) ? base.samples [i] : sd.sampleMin [i];
			sd.sampleSums [i] += base.samples[i];
			sd.sampleAvg [i] = sd.sampleSums [i] / sd.totalTimesteps;
			timeStep[i] = base.samples[i];

			timeStep [i] = base.samples [i];
		}
	}
}
	
[System.Serializable]
public class SongData {

	public string name;
	public List<float[]> sampleBufferList;

	public float[][] sampleBuffer;
	public float[] sampleSums;
	public float[] sampleAvg;
	public float[] sampleMin;
	public float[] sampleMax;
	public int totalTimesteps = 0;
}

public class SpecRecorder : MonoBehaviour {

	private BeatDetectorRecorder bd;
	private SongData songData;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		int totalSamples = 8;
		bd = new BeatDetectorRecorder (totalSamples);

		songData = new SongData ();

		songData.sampleAvg = new float[totalSamples];
		songData.sampleMin = new float[totalSamples];
		songData.sampleMax = new float[totalSamples];
		songData.sampleSums = new float[totalSamples];
		songData.sampleBufferList = new List<float[]> ();

		for (int i = 0; i < totalSamples; i++) {
			songData.sampleAvg [i] = 0;
			songData.sampleMin [i] = 100;
			songData.sampleMax [i] = -100;
			songData.sampleSums[i] = 0;
		}

		Debug.Log (songData.sampleSums);
		songData.name = audioSource.clip.name;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying) {
			Debug.Log ("finished");

			songData.sampleBuffer = songData.sampleBufferList.ToArray ();

			//save data
			string songJson = JsonUtility.ToJson (songData, true);

			Debug.Log (songJson);

			System.IO.File.WriteAllText ("Assets/Resources/songData/" + audioSource.clip.name + ".dat", songJson);
		} else {
			bd.updateSamples ();
			bd.addSample (songData);
		}
	}
}
