using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {

	public VoxelChunk voxelChunk;
	GameObject cube;
	bool traversing = false;

	Vector3 startPosition = new Vector3(8, 4, 14);
	Vector3 endPosition = new Vector3(11, 4, 2);
	Vector3 offset = new Vector3(0.5f,0.5f,0.5f);


	void Update()
	{
		if (!traversing) 
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				Stack<Vector3> path = BreadthFirstSearch(startPosition,endPosition,voxelChunk);

				if (path.Count > 0)
				{

					StartCoroutine(LerpAlongPath(path));
				}
			}
		}
	}

	IEnumerator LerpAlongPath(Stack<Vector3> path)
	{
		traversing = true;
		float lerpTime = 1.0f;
		//if we have a cube , destroy it
		if (cube != null) {
			DestroyObject(cube);

		}
		cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		//pop first waypoint off as start pos
		Vector3 current = path.Pop ();
		cube.transform.position = current;

		while (path.Count > 0) {
			Vector3 target = path.Pop();
			float currentTime = 0.0f;
			while (currentTime < lerpTime)
			{
				currentTime += Time.deltaTime;
				cube.transform.position = Vector3.Lerp(current,target,currentTime/lerpTime);
				yield return 0;

			}
			cube.transform.position = target;
			current = target;
		}
		traversing = false;

	}

	Stack<Vector3> BreadthFirstSearch(Vector3 start, Vector3 end , VoxelChunk vc)
	{

		Stack<Vector3> waypoints = new Stack<Vector3>();
		Dictionary<Vector3,Vector3> visitedParents = new Dictionary<Vector3, Vector3> ();
		Queue<Vector3> q = new Queue<Vector3> ();
		bool found = false;
		Vector3 current = start;

		q.Enqueue (start);
		while (q.Count > 0 && !found) 
		{
			current = q.Dequeue();
			if (current != end)
			{
				// our adjacent nodes are x+1, x-1, z+1 and z-1
				List<Vector3> neighbourList = new List<Vector3>();
				neighbourList.Add(current+new Vector3(1,0,0));
				neighbourList.Add(current+new Vector3(-1,0,0));
				neighbourList.Add(current+new Vector3(0,0,1));
				neighbourList.Add(current+new Vector3(0,0,-1));

				foreach(Vector3 n in neighbourList)
				{
					if ((n.x >= 0 && n.x < vc.GetChunkSize()) 
					    && n.z >= 0 && n.z < vc.GetChunkSize())
					{
						if (vc.IsTraverable(n))
						{
							if (!visitedParents.ContainsKey(n))
							{
								visitedParents[n] = current;
								q.Enqueue(n);

							}
						}
					}
				}

			}
			else
			{
				found = true;
			}
		}
		if (found) 
		{
			while (current != start)
			{
				waypoints.Push(current+offset);
				current = visitedParents[current];

			}
			waypoints.Push(start+offset);
		}
		return waypoints;
	}
}
