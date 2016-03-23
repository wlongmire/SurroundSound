using UnityEngine;

namespace Application
{
	public class BeatData
	{
		private int sample_amount;
		private float[] samples;

		public BeatData (AudioSource audio_source, int subdivs) {
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

}

