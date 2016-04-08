using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryScript : MonoBehaviour {

	public Transform parentPanel;
	bool invOpen;
	public List<Sprite> blockSprites;
	public List<int> blockAmounts;
	public List<string> blockNames;
	public GameObject startItem;

	List<InventoryItemScript> inventoryList;

	public Image quickInvImage;
	public int currentBlock;
	GameObject inventoryItem;
	// Use this for initialization
	void Start () {

		parentPanel.gameObject.SetActive (false);
		invOpen = false;
		inventoryList = new List<InventoryItemScript> ();
		for (int i = 0; i < blockNames.Count;i++)
		{
			inventoryItem = (GameObject)Instantiate(startItem);
			inventoryItem.transform.SetParent(parentPanel);
			inventoryItem.SetActive(true);

			InventoryItemScript iis = inventoryItem.GetComponent<InventoryItemScript>();
			iis.itemSprite.sprite = blockSprites[i];
			iis.itemNameText.text = blockNames[i];
			iis.itemName = blockNames[i];
			iis.itemAmountText.text = blockAmounts[i].ToString();
			iis.itemAmount = blockAmounts[i];
			// Keep a list of the inventory items
			inventoryList.Add(iis);
		}
		DisplayListInOrder ();
		currentBlock = 1;
		quickInvImage.sprite = blockSprites[0];
		print (currentBlock.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInventory ();
	
		if (Input.GetKeyDown (KeyCode.I)) {
			if (invOpen == false) {
				parentPanel.gameObject.SetActive (true);
				invOpen = true;
			}
			else  {
				parentPanel.gameObject.SetActive (false);
				invOpen = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.E)) {

			SwitchBlock();
		}
	}

	void UpdateInventory()
	{
		for (int i = 0; i < blockNames.Count;i++)
		{
			inventoryList[i].itemAmountText.text = blockAmounts[i].ToString();
			inventoryList[i].itemAmount = blockAmounts[i];
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

	void DisplayListInOrder()
	{
		float yOffSet = 75f;

		Vector3 startPosition = startItem.transform.position;
		foreach (InventoryItemScript iis in inventoryList) 
		{
			iis.transform.position = startPosition;
			startPosition.y -= yOffSet;
		}

	}
}
