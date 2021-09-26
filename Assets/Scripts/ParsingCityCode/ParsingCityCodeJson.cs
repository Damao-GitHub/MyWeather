using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.IO;
public class ParsingCityCodeJson
{
    public static List<Province> m_CityCode;
    public static void Init()
    {
        string jsonData = Resources.Load<TextAsset>("CityCodeJson").text;
        var cityCode = JsonConvert.DeserializeObject<CityCodeData>(jsonData);
        m_CityCode = cityCode.CityCode;
    }
    public static string GetCityCode(string province, string cityName)
    {
        for (int i = 0; i < m_CityCode.Count; i++)
        {
            if (m_CityCode[i].省 == province)
            {
                for (int j = 0; j < m_CityCode[i].市.Count; j++)
                {
                    if(m_CityCode[i].市[i].市名 == cityName)
                    {
                        return m_CityCode[i].市[i].编码;
                    }
                }
            }
        }
        return "";
    }
    public class CityCodeData
    {
        public List<Province> CityCode;
    }
    public class Province
    {
        public string 省;
        public List<Code> 市;
    }
    public class Code
    {
        public string 市名;
        public string 编码;
    }
}
