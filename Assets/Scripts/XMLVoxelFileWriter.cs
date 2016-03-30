using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLVoxelFileWriter 
{

//	public static bool ReadStartAndEndPosition(out Vector3 start,out Vector3 end,string fileName)
//	{
//		bool foundStart = false;
//		bool foundEnd = false;
//
//		start = new Vector3 (-1, -1, -1);
//		end = new Vector3 (-1, -1, -1);
//
//		XmlReader xmlReader = XmlReader.Create(fileName+".xml");
//	}

	//write the voxel chunk to xml
	public static void SaveChunkToXMLFile(int[,,] voxelArray, string fileName)
	{
		XmlWriterSettings writerSettings = new XmlWriterSettings();
		writerSettings.Indent = true;

		XmlWriter xmlWriter = XmlWriter.Create (fileName + ".xml", writerSettings);
		xmlWriter.WriteStartDocument ();
		xmlWriter.WriteStartElement("VoxelChunk");

		for (int x = 0; x < voxelArray.GetLength(0); x++) 
		{
			for (int y = 0; y < voxelArray.GetLength(1);y++)
			{
				for (int z = 0;z < voxelArray.GetLength(1);z++)
				{
					if (voxelArray[x,y,z] != 0)
					{
						xmlWriter.WriteStartElement("Voxel");
						xmlWriter.WriteAttributeString("x",x.ToString());
						xmlWriter.WriteAttributeString("y",y.ToString());
						xmlWriter.WriteAttributeString("z",z.ToString());

						xmlWriter.WriteString(voxelArray[x,y,z].ToString());

						xmlWriter.WriteEndElement();

					}
				}
			}
		}

//		GameObject playerObj = GameObject.FindGameObjectWithTag("GameController");
//
//		if (playerObj = null) 
//		{
//			Debug.LogError("No player found!!");
//		}
//		else
//		{
//			xmlWriter.WriteStartElement("Player");
//			xmlWriter.WriteAttributeString("Px",playerObj.transform.position.x.ToString());
//			xmlWriter.WriteAttributeString("Py",playerObj.transform.position.y.ToString());
//			xmlWriter.WriteAttributeString("Pz",playerObj.transform.position.z.ToString());
//
//			xmlWriter.WriteString(playerObj.transform.position.ToString());
//
//			xmlWriter.WriteEndElement();
//		}


		xmlWriter.WriteEndElement ();
		xmlWriter.WriteEndDocument ();
		xmlWriter.Close ();


	}

	public static int[,,] LoadChunkFromXMLFile(int size, string fileName)
	{
		int [,,] voxelArray = new int[size, size, size];
		XmlReader xmlReader = XmlReader.Create(fileName + ".xml");

		while(xmlReader.Read())
		{
			if (xmlReader.IsStartElement("Voxel"))
			{
				int x = int.Parse(xmlReader["x"]);
				int y = int.Parse(xmlReader["y"]);
				int z = int.Parse(xmlReader["z"]);
				xmlReader.Read();
				int value = int.Parse(xmlReader.Value);

				voxelArray[x,y,z] = value;

			}

//			if (xmlReader.IsStartElement("Player"))
//			{
//				float Px = float.Parse(xmlReader["Px"]);
//				float Py = float.Parse(xmlReader["Py"]);
//				float Pz = float.Parse(xmlReader["Pz"]);
//				xmlReader.Read();
//				float pValue = float.Parse(xmlReader.Value);
//				playerObj.transform.position.x = pValue;
//				playerObj.transform.position.y = pValue;
//				playerObj.transform.position.z = pValue;
//
//
//			}
		
		}


		return voxelArray;
		

	}





}
