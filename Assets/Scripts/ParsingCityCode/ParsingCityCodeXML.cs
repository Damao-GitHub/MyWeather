using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ParsingCityCodeXML
{
	public static Dictionary<string , Dictionary<string , Dictionary<string,string>>> m_CityCode;
    public static void Init()
    {
		m_CityCode = new Dictionary<string , Dictionary<string , Dictionary<string , string>>>();
		string xmlData = Resources.Load<TextAsset>("CityCodeXML").text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("China").ChildNodes;
		foreach (XmlElement xe in nodeList)
		{
			//Debug.Log("Attribute :" + xe.GetAttribute("name"));
			Dictionary<string , Dictionary<string , string>> shi = new Dictionary<string , Dictionary<string , string>>();
			foreach (XmlElement x1 in xe.ChildNodes)
			{
				//Debug.LogError("Attribute1 :" + x1.GetAttribute("name"));
				Dictionary<string , string> xian = new Dictionary<string , string>();
				foreach (XmlElement xc in x1.ChildNodes)
				{
					xian.Add(xc.GetAttribute("name") , xc.GetAttribute("weatherCode"));
					//Debug.LogWarning("Attribute1 :" + xc.GetAttribute("name"));
				}
				shi.Add(x1.GetAttribute("name") , xian);
			}
			m_CityCode.Add(xe.GetAttribute("name") , shi);
		}
	}
	public static string GetCityCode(string province,string cityName,string regionName)
	{
		if (!m_CityCode.ContainsKey(province)) return "";
		if (!m_CityCode[province].ContainsKey(cityName)) return "";
		if (!m_CityCode[province][cityName].ContainsKey(regionName)) return "";
		return m_CityCode[province][cityName][regionName];
	}
}