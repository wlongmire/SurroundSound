using System.Collections;
using System.IO;

using UnityEngine;

public class BeatDetector {
	protected int sample_amount;
	public float[] samples;

	public BeatDetector (int subdivs) {
		this.sample_amount = subdivs;
		this.samples = new float[sample_amount];
	}

	//Updates samples at the current time based on the user assigned sound channel
	public void updateSamples(int channel = 0) {
		AudioListener.GetOutputData(this.samples, channel);
	}

	//Extracts the average of the indexth sample cluster of division length
	//Assumes that the number of samples (sample_amount) is evenly divided by the cluster_amount)
	public float getSampleAvg(int index, int cluster_amount) {

		int cluster_length = this.sample_amount / cluster_amount;
		int start = cluster_length * index;
		float sum = 0;

		for (int i = start; i < start+ cluster_length; i++) {
			sum += this.samples [i];
		}

		return (sum / cluster_length);
	}
}

public class SpecAnalyzer : MonoBehaviour {

	private GameObject[] cubes;
	private BeatDetector bd;
	private SongData songData;

	public AudioSource audioSource;
	public float mulitplier = 20;

	void Start () {
		audioSource.Play();

		bd = new BeatDetector (1024);
		cubes = GameObject.FindGameObjectsWithTag ("cube");
	
		string songJson = System.IO.File.ReadAllText ("Assets/Resources/songData/" + audioSource.clip.name + ".dat");
		songData = JsonUtility.FromJson <SongData>(songJson);
	}

	void Update () {
		bd.updateSamples ();

		for(int i = 0; i< cubes.Length; i++) {
			float high = bd.getSampleAvg (i, cubes.Length);
			cubes [i].transform.localScale = new Vector3 (high * mulitplier, 1, 1);
		}
	}
}