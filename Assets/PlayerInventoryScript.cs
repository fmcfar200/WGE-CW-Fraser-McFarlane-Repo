using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryScript : MonoBehaviour {

	public List<Sprite> blockSprites;
	public Image quickInvImage;
	public int currentBlock;

	// Use this for initialization
	void Start () {
		currentBlock = 1;
		quickInvImage.sprite = blockSprites[0];
		print (currentBlock.ToString ());
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.E)) {

			SwitchBlock();
		}
	
	}

	void SwitchBlock()
	{
		currentBlock++;
		if (currentBlock >= 5)
		{
			currentBlock = 1;
		}

		switch (currentBlock) {
		case 4:
			quickInvImage.sprite = blockSprites[3];
			break;
		case 3:
			quickInvImage.sprite = blockSprites[2];
			break;
		case 2:
			quickInvImage.sprite = blockSprites[1];
			break;
		case 1:
			quickInvImage.sprite = blockSprites[0];
			break;



		}

		print (currentBlock.ToString ());

	}
}
