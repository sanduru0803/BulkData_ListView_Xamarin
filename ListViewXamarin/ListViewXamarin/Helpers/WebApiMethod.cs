using ListViewXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ListViewXamarin.Helpers
{
    public class WebAPIService
    {
        #region Fields 

        System.Net.Http.HttpClient client;

        #endregion

        #region Properties 

        public ObservableCollection<Order> Items
        {
            get; private set;
        }

        public string WebAPIUrl
        {
            get; private set;
        }

        #endregion

        #region Constructor
        public WebAPIService()
        {
            client = new System.Net.Http.HttpClient();
        }

        #endregion

        #region Methods
        public async System.Threading.Tasks.Task<ObservableCollection<Order>> RefreshDataAsync()
        {

            string WebAPIUrl = "https://portalmobapi.rsgt.com:8443/api/DataSource/getBayanListphase2?fstrAPIToken=&fstrMailID=test@gmail.com&fstrFilter=fstrAnyWhere:;fstrBD_CONSIGNEEDESC1:;fstrBD_VESSELNAME1:;fstrBL_SHIPPINGLINEID:;fstrBD_Bayanstatus:101,102,103;fstrContainerCategories:;&fstrPageNumber=1&fstrPageSize=100000";

            try
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(WebAPIUrl),
                    Method = HttpMethod.Get,
                    Headers = {
                        { "X-Version", "1" }, // HERE IS HOW TO ADD HEADERS,
                       // { HttpRequestHeader.Authorization.ToString(), ApiKey },
                        { HttpRequestHeader.Authorization.ToString(), "5fc756ed33eebe7e6001b8b5709b257f91de4e989501a90ab805ad72" },
                        { HttpRequestHeader.Accept.ToString(), "application/json, application/xml" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" },  //use this content type if you want to send more than one content type
                    },
                };
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("ApiKey", "hl4bA4nB4yI0fC8fH7eT6");
                client.DefaultRequestHeaders.Add("Authorization", "5fc756ed33eebe7e6001b8b5709b257f91de4e989501a90ab805ad72");
                var response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Order>>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion
    }
}
