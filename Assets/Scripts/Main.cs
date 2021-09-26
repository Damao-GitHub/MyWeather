using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;
    public string Provinces { get { return PlayerPrefs.GetString("Provinces"); } set { PlayerPrefs.SetString("Provinces" , value); } }
    public string CityName { get { return PlayerPrefs.GetString("CityName"); } set { PlayerPrefs.SetString("CityName" , value); } }
    public string RegionName { get { return PlayerPrefs.GetString("RegionName"); } set { PlayerPrefs.SetString("RegionName" , value); } }
    public string CityCode = "";
    public resp weather;
    public void Awake()
    {
        Init();
        if(string.IsNullOrEmpty(CityCode = ParsingCityCodeXML.GetCityCode(Provinces , CityName , RegionName)))
        {
            Provinces = "北京";
            CityName = "北京";
            RegionName = "北京";
            CityCode = ParsingCityCodeXML.GetCityCode(Provinces , CityName , RegionName);
            //CityCode = ParsingCityCodeJson.GetCityCode(Provinces , CityName);
        }
    }
    public void Start()
    {
        weather = HttpGetWeather.GetInstance().GetWeather(CityCode);
        HEventDispatcher.GetInstance().DispatchEvent(new BaseHEvent(BaseHEventType.GET_WEATHER , this));
    }
    // 加载必要资源
    private void Init()
    {
        instance = this;
        UISystem.GetInstance();
        
        ParsingCityCodeXML.Init();
        Loom.Initialize();
        // 改用XML解析 json解析遗弃
        // ParsingCityCodeJson.Init();
    }
    public void SetCityCode(string Provinces,string CityName,string RegionName)
    {
        this.Provinces = Provinces;
        this.CityName = CityName;
        this.RegionName = RegionName;
        CityCode = ParsingCityCodeXML.GetCityCode(Provinces , CityName , RegionName);
        weather = HttpGetWeather.GetInstance().GetWeather(CityCode);
        HEventDispatcher.GetInstance().DispatchEvent(new BaseHEvent(BaseHEventType.GET_WEATHER , this));
    }
}
