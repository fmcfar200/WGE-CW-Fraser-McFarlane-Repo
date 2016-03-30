using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLPlayerFile {

	public static void SavePlayerToFile(Vector3 playerPos, string fileName)
	{
		XmlWriterSettings writerSettings = new XmlWriterSettings();
		writerSettings.Indent = true;
		
		XmlWriter xmlWriter = XmlWriter.Create (fileName + ".xml", writerSettings);
		xmlWriter.WriteStartDocument ();
		xmlWriter.WriteStartElement("Player");

		xmlWriter.WriteStartElement("Player");
		xmlWriter.WriteAttributeString("Px",playerPos.x.ToString());
		xmlWriter.WriteAttributeString("Py",playerPos.y.ToString());
		xmlWriter.WriteAttributeString("Pz",playerPos.z.ToString());
		
		xmlWriter.WriteString(playerPos.ToString());
		
		xmlWriter.WriteEndElement();
	
		
		
		xmlWriter.WriteEndElement ();
		xmlWriter.WriteEndDocument ();
		xmlWriter.Close ();
	}

	public static Vector3 LoadPlayerFromFile(string fileName)
	{
		Vector3 playerPos = new Vector3();

		if (System.IO.File.Exists (fileName + ".xml") == true) {
			XmlReader xmlReader = XmlReader.Create (fileName + ".xml");
			
			while (xmlReader.Read()) 
			{
				if (xmlReader.IsStartElement ("Player")) 
				{
					float Px = float.Parse (xmlReader ["Px"]);
					float Py = float.Parse (xmlReader ["Py"]);
					float Pz = float.Parse (xmlReader ["Pz"]);
					xmlReader.Read ();
					playerPos = new Vector3 (Px, Py, Pz);
					Debug.Log (playerPos.ToString ());
					
				}
				
			}

		} 
		else 
		{
			Debug.LogError("PLAYER FILE NOT FOUND!");
			return new Vector3(10,10,10);

		}

		return playerPos;

	}



}
