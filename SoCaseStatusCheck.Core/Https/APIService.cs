using SoCaseStatusCheck.Core.Models;
using SoCaseStatusCheck.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace SoCaseStatusCheck.Core.Https
{
    public class APIService : APIBaseService
    {
        public async Task<List<SoCase>> GetThemes()
        {
            try
            {
                if (NetworkManager.Current.Network == 4)  //无网络连接
                {
                    List<SoCase> list = new List<SoCase>();
                    for (var i = 0; i < 100; i++)
                    {
                        list.Add(new SoCase() { ID = i.ToString(), Statue = CaseStatue.Normal, Title = "ZhuMingHao....." });
                    }

                    return list;
                }
                else
                {
                    JsonObject json = await GetJson(ServiceURL.SOCaseAPI);
                    if (json != null)
                    {
                        List<SoCase> list = new List<SoCase>();
                        if (json.ContainsKey("subscribed"))
                        {
                            var subscribed = json["subscribed"];
                            if (subscribed != null)
                            {
                                JsonArray ja = subscribed.GetArray();
                                foreach (var j in ja)
                                {
                                    //  list.Add(new SoCase { ID = (j.GetObject())["id"].GetNumber().ToString(), Title = (j.GetObject())["name"].GetString(), Description = (j.GetObject())["description"].GetString(), Thumbnail = (j.GetObject())["thumbnail"].GetString() });
                                }
                            }
                        }
                        var others = json["others"];
                        if (others != null)
                        {
                            JsonArray ja = others.GetArray();
                            foreach (var j in ja)
                            {
                                //  list.Add(new SoCase { ID = (j.GetObject())["id"].GetNumber().ToString(), Title = (j.GetObject())["name"].GetString(), Description = (j.GetObject())["description"].GetString(), Thumbnail = (j.GetObject())["thumbnail"].GetString() });
                            }
                        }
                        await FileHelper.Current.WriteObjectAsync<List<SoCase>>(list, "themes.json");
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
