using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip destroyBlockSound;
	public AudioClip placeBlockSound;


	void PlayBlockSound(int blockType)
	{
		if (blockType == 0) {
			audio.PlayOneShot (destroyBlockSound);
		} else {
			audio.PlayOneShot (placeBlockSound);
		}
	}


	void OnEnable()
	{
		VoxelChunk.OnEventBlockChanged += PlayBlockSound;
	}
	void OnDisable()
	{
		VoxelChunk.OnEventBlockChanged -= PlayBlockSound;
	}

}
