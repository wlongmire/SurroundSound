using UnityEngine;
using System.Collections;

public class SpecAnalyzer : MonoBehaviour {

	private int subdivs = 1024;
	private float[] samples;
	private GameObject[] cubes;
	public AudioSource audio_source;


	float sumSamples(int idx, int div) {
		int start = div * idx;
		int end = start + div;
		float sum = 0;

		for (int i = start; i < end; i++) {
			sum += samples [i];
		}

		return (sum / div);
	}

	// Use this for initialization
	void Start () {
		samples = new float[subdivs];
		cubes = GameObject.FindGameObjectsWithTag ("cube");
	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.GetOutputData(samples, 0);

		string s = "";
		for(int i=0; i<cubes.Length; i++){
			Debug.Log (sumSamples (i, samples.Length / cubes.Length));

			cubes [i].transform.localScale = new Vector3 (sumSamples(i, samples.Length/cubes.Length)*20, 1, 1);
//			cubes [i].transform.localPosition = new Vector3 (samples[i]*10, cubes[i].transform.position.y, cubes[i].transform.position.z);
		}
	}
}
