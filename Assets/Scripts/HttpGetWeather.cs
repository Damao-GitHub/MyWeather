using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class HttpGetWeather : MonoBehaviour
{
    private static HttpGetWeather instance;
    public static HttpGetWeather GetInstance()
    {
        if(instance == null)
        {
            GameObject obj = new GameObject("GetWeather");
            obj.AddComponent<HttpGetWeather>();
            instance = obj.GetComponent<HttpGetWeather>();
            instance.Init();
        }
        return instance;
    }
    private void Init()
    {

    }
    public resp GetWeather(string cityCode)
    {
        string weatherInfoUrl = "http://wthrcdn.etouch.cn/WeatherApi?citykey=" + cityCode;
        string weatherstr = GetHtml(weatherInfoUrl);
        return XmlDeSeralizer<resp>(weatherstr);
    }
    private string GetHtml(string url)
    {
        StringBuilder s = new StringBuilder(102400);
        HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        wr.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
        HttpWebResponse response = (HttpWebResponse)wr.GetResponse();
        //Head(response);
        GZipStream g = new GZipStream(response.GetResponseStream() , CompressionMode.Decompress);
        byte[] d = new byte[20480];
        int l = g.Read(d , 0 , 20480);
        while (l > 0)
        {
            s.Append(Encoding.UTF8.GetString(d , 0 , l));
            l = g.Read(d , 0 , 20480);
        }
        return s.ToString();
    }

    private void Head(HttpWebResponse r)
    {
        string[] keys = r.Headers.AllKeys;
        //for (int i = 0; i < keys.Length; ++i)
        //{
        //    Debug.Log(keys[i] + "   " + r.Headers[keys[i]]);
        //}
    }

    public T XmlDeSeralizer<T>(string xmlStr) where T : class, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(T));
        using (StringReader reader = new StringReader(xmlStr))
        {
            return xs.Deserialize(reader) as T;
        }
    }
}

#region 天气实体类
[System.Serializable]
public class resp
{
    public string city { get; set; }                // 城市
    public string updatetime { get; set; }          // 更新时间
    public string wendu { get; set; }               // 温度
    public string fengli { get; set; }              // 风力
    public string shidu { get; set; }               // 湿度
    public string fengxiang { get; set; }           // 风向
    public yesterday yesterday{ get; set; }         // 昨日天气
    public List<weather> forecast{ get; set; }      // 未来几天
    public List<zhishu> zhishus{ get; set; }        // 指数
}
[System.Serializable]
public class yesterday
{
    public string date_1{ get; set; }
    public string high_1{ get; set; }
    public string low_1{ get; set; }
    public yesterday_1 day_1{ get; set; }
    public yesterday_1 night_1 { get; set; }
    [System.Serializable]
    public class yesterday_1
    {
        public string type_1{ get; set; }
        public string fx_1{ get; set; }
        public string fl_1{ get; set; }
    }
}
[System.Serializable]
public class weather
{
    public string date { get; set; }
    public string high { get; set; }
    public string low { get; set; }
    public dayWeather day { get; set; }
    public dayWeather night { get; set; }
    [System.Serializable]
    public class dayWeather
    {
        public string type { get; set; }
        public string fengxiang { get; set; }
        public string fengli { get; set; }
    }
}
[System.Serializable]
public class zhishu
{
    public string name{ get; set; }
    public string value{ get; set; }
    public string detail{ get; set; }
}
#endregion