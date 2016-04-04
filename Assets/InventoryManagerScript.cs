using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManagerScript : MonoBehaviour {

	public Transform parentPanel;

	public List<Sprite> itemSprites;
	public List<string> itemNames;
	public List<int> itemAmounts;

	public GameObject startItem;

	List<InventoryItemScript> inventoryList;

	// Use this for initialization
	void Start () 
	{
		inventoryList = new List<InventoryItemScript> ();

		for (int i = 0; i < itemNames.Count; i++) 
		{
			//creates dublicate of the starter item
			GameObject inventoryItem = (GameObject)Instantiate(startItem);
			inventoryItem.transform.SetParent(parentPanel);
			inventoryItem.SetActive(true);

			//get inventory item script component so we can set the data
			InventoryItemScript iis = inventoryItem.GetComponent<InventoryItemScript>();
			iis.itemSprite.sprite = itemSprites[i];
			iis.itemNameText.text = itemNames[i];
			iis.itemName = itemNames[i];
			iis.itemAmountText.text = itemAmounts[i].ToString();
			iis.itemAmount = itemAmounts[i];

			//keep a list of the inventory items
			inventoryList.Add(iis);

		}
		DisplayListInOrder ();


	}


	public void SelectionSortInventory()
	{
		//iterate through every item in the list except from the last
		for (int i = 0; i < inventoryList.Count-1; i++) 
		{
			int minIndex = i;
			//iterate through unsorted portions of the list
			for (int j = i; j < inventoryList.Count;j++)
			{
				if (inventoryList[j].itemAmount < inventoryList[minIndex].itemAmount)
				{
					minIndex = j;

				}
			}
			//swap min index into pos
			if (minIndex!=i)
			{
				InventoryItemScript iis = inventoryList[i];
				inventoryList[i] = inventoryList[minIndex];
				inventoryList[minIndex] = iis;

			}
		}
		DisplayListInOrder ();
	}

	public void InsertionSortInventory()
	{
		for (int i = 1; i < inventoryList.Count; i++) 
		{
			int j = i;
			while (j > 0 && inventoryList[j-1].itemAmount > inventoryList[j].itemAmount)
			{
				InventoryItemScript iis = inventoryList[j];
				inventoryList[j] = inventoryList[j-1];
				inventoryList[j-1] = iis;
				j--;
			}
		}
		DisplayListInOrder ();
	}
	public void StartQuickSort()
	{
		inventoryList = QuickSort (inventoryList);
		DisplayListInOrder ();
	}
	List<InventoryItemScript> QuickSort(List<InventoryItemScript> listIn)
	{
		if (listIn.Count <= 1)
		{
			return listIn;

		}
		//Niave pivot selection
		int pivotIndex = 0;

		//left and right lists
		List<InventoryItemScript> leftList = new List<InventoryItemScript>();
		List<InventoryItemScript> rightList = new List<InventoryItemScript>();
		for(int i = 1; i < listIn.Count;i++)
		{
			if (listIn[i].itemAmount > listIn[pivotIndex].itemAmount)
			{
				leftList.Add(listIn[i]);

			}
			else
			{
				rightList.Add(listIn[i]);
			}
		}
		//recurse left and right list
		leftList = QuickSort(leftList);
		rightList = QuickSort(rightList);

		//concatenate list : list+pivot+right
		leftList.Add(listIn[pivotIndex]);
		leftList.AddRange(rightList);

		return leftList;


	}


	void DisplayListInOrder()
	{
		//height and space between item
		float yOffset = 55f;
		//use the start position for the first item
		Vector3 startPosition = startItem.transform.position;
		foreach (InventoryItemScript iis in inventoryList)
		{
			iis.transform.position = startPosition;
			//set pos of next object using offset
			startPosition.y -= yOffset;

		}
	}

}
