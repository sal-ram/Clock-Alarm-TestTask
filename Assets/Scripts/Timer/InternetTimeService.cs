using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts
{
    public class InternetTimeService : MonoBehaviour
    {
        private readonly string _urlTimeAPI = "http://worldtimeapi.org/api/timezone/Europe/Moscow"; // TimeAPI.io
        private readonly string _urlNinjaTimeAPI = "https://api.api-ninjas.com/v1/worldtime?city=moscow"; // NinjaTimeAPI
        HttpClient clientTimeApi;
        HttpClient clientNinjaTimeAPI;

        private void Awake()
        {
            clientTimeApi = new HttpClient();
            clientNinjaTimeAPI = new HttpClient();
            clientNinjaTimeAPI.DefaultRequestHeaders.Add("X-Api-Key", "mnVoBT5UK8TiTM7bEY1KBA==SU2YbFrP6He8qPLB");
        }

        public async Task<TimeStruct> GetInternetTime(TimeAPIServices timeAPI)
        {
            string jsonData = string.Empty;

            switch (timeAPI)
            {
                case TimeAPIServices.WorldTimeAPI:
                    jsonData = await clientTimeApi.GetStringAsync(_urlTimeAPI);
                    break;

                case TimeAPIServices.NinjaTimeAPI:
                    jsonData = await clientNinjaTimeAPI.GetStringAsync(_urlNinjaTimeAPI);
                    break;
            }

            return ParseTime(jsonData);
        }

        public async Task<TimeStruct> GetFirstInternetTime()
        {
            var jsonData = await clientNinjaTimeAPI.GetStringAsync(_urlNinjaTimeAPI);
            return ParseTime(jsonData);
        }

        public async Task<TimeStruct> GetSecondInternetTime()
        {
            var jsonData = await clientNinjaTimeAPI.GetStringAsync(_urlTimeAPI);
            return ParseTime(jsonData);
        }

        private TimeStruct ParseTime(string jsonData)
        {
            JObject jObject = JObject.Parse(jsonData);
            TimeStruct timeStruct = new TimeStruct();

            var datetime = (string)jObject["datetime"];

            timeStruct.Hour = float.Parse(datetime.Substring(11, 2)); 
            timeStruct.Min = float.Parse(datetime.Substring(14, 2));
            timeStruct.Sec = float.Parse(datetime.Substring(17, 2));

            return timeStruct;
        }
    }
}
