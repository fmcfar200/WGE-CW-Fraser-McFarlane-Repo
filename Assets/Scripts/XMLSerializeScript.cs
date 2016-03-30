using System.Xml.Serialization;
using System.Collections;
using UnityEngine;

public class XMLSerializeScript: MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			TestDataClass tdc = new TestDataClass("","This is the serialised item");
			XmlSerializer x = new XmlSerializer(tdc.GetType());

			System.IO.FileStream file = System.IO.File.Create("TestFile.xml");

			x.Serialize(file,tdc);
			file.Close();

		}

		if (Input.GetKeyDown (KeyCode.F2)) 
		{
			TestDataClass tdc = new TestDataClass("","");
			XmlSerializer x = new XmlSerializer(tdc.GetType());

			System.IO.FileStream file = System.IO.File.OpenRead("TestFile.xml");

			tdc = (TestDataClass)x.Deserialize(file);
			file.Close();
			print(tdc.name + ": " + tdc.description);



		}
	}

}
