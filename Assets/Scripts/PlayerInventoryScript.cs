using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryScript : MonoBehaviour {

	public Transform parentPanel;
	public bool invOpen;
	public List<Sprite> blockSprites;
	public List<int> blockAmounts;
	public List<string> blockNames;
	public GameObject startItem;

	List<InventoryItemScript> inventoryList;

	public Image quickInvImage;
	public Text quickInvAmount;
	public int currentBlock;
	GameObject inventoryItem;
	public Camera mainCamera;
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
		//TEMP// UpdateInventory ();
	
		if (Input.GetKeyDown (KeyCode.I)) {
			if (invOpen == false) {

				invOpen = true;

			}
			else  {

				invOpen = false;

			}
		}

		if (Input.GetKeyDown(KeyCode.E)) {

			SwitchBlock();
		}

		if (invOpen) 
		{
			parentPanel.gameObject.SetActive (true);
		}
		else 
		{
			parentPanel.gameObject.SetActive (false);

		}
	}

	public void UpdateInventory()
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


	

	public void StartMergeSortAmountLowHigh()
	{
		inventoryList = MergeSortAmountLowHigh (inventoryList);
		DisplayListInOrder ();
	}

	public void StartMergeSortAmountHighLow()
	{
		inventoryList = MergeSortAmountHighLow (inventoryList);
		DisplayListInOrder();
	}

	List<InventoryItemScript> MergeSortAmountLowHigh(List<InventoryItemScript> listIn)
	{
		if (listIn.Count <= 1) 
		{
			return listIn;
		}

		List<InventoryItemScript> leftList = new List<InventoryItemScript>();
		List<InventoryItemScript> rightList = new List<InventoryItemScript>();
		int mid = listIn.Count / 2;

		for (int i = 0; i < mid; i++) 
		{
			leftList.Add(listIn[i]);
		}
		for (int i = mid; i < listIn.Count; i++) {
			rightList.Add(listIn[i]);
		}


		MergeSortAmountLowHigh (leftList);
		MergeSortAmountLowHigh (rightList);

		//merge left and right
		listIn = MergeAmountLowHigh (leftList, rightList);

		return listIn;
	}

	List<InventoryItemScript> MergeAmountLowHigh(List<InventoryItemScript> l , List<InventoryItemScript> r)
	{
		List<InventoryItemScript> m = new List<InventoryItemScript> (); //list to hold merged elements
		int i = 0;
		int j = 0;

		while (i < l.Count && j < r.Count) 
		{
			if (l[i].itemAmount <= r[j].itemAmount)
			{
				m.Add(l[i]);
				i++;
			}
			else
			{
				m.Add(r[j]);
				j++;
			}

		}
		if (i < l.Count)
		{
			m.AddRange(l);
		}
		else
		{
			m.AddRange(r);
		}
		return m;
	}
	List<InventoryItemScript> MergeSortAmountHighLow(List<InventoryItemScript> listIn)
	{
		if (listIn.Count <= 1) 
		{
			return listIn;
		}
		
		List<InventoryItemScript> leftList = new List<InventoryItemScript>();
		List<InventoryItemScript> rightList = new List<InventoryItemScript>();
		int mid = listIn.Count / 2;
		
		for (int i = 0; i < mid; i++) 
		{
			leftList.Add(listIn[i]);
		}
		for (int i = mid; i < listIn.Count; i++) {
			rightList.Add(listIn[i]);
		}
		
		
		MergeSortAmountHighLow (leftList);
		MergeSortAmountHighLow (rightList);
		
		//merge left and right
		listIn = MergeAmountHighLow (leftList, rightList);
		
		return listIn;
	}
	
	List<InventoryItemScript> MergeAmountHighLow(List<InventoryItemScript> l , List<InventoryItemScript> r)
	{
		List<InventoryItemScript> m = new List<InventoryItemScript> (); //list to hold merged elements
		int i = 0;
		int j = 0;
		
		while (i < l.Count && j < r.Count) 
		{
			if (l[i].itemAmount >= r[j].itemAmount)
			{
				m.Add(l[i]);
				i++;
			}
			else
			{
				m.Add(r[j]);
				j++;
			}
			
		}
		if (i < l.Count)
		{
			m.AddRange(l);
		}
		else
		{
			m.AddRange(r);
		}
		return m;
	}



}
