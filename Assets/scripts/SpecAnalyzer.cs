using UnityEngine;
using System.Collections;


public class SpecAnalyzer : MonoBehaviour {

	private GameObject[] cubes;
	private BeatData bd;
	public AudioSource audio_source;

	void Start () {
		bd = new BeatData (audio_source, 1024);
		cubes = GameObject.FindGameObjectsWithTag ("cube");
	}

	void Update () {
		bd.updateSamples ();

		for(int i=0; i<cubes.Length; i++){
			cubes [i].transform.localScale = new Vector3 (bd.getSampleAvg(i, cubes.Length)*20, 1, 1);
		}
	}
}