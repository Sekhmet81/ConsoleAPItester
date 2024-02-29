using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static async Task Main(string[] args)
    {
        Newtonsoft.Json.Linq.JArray json = null;
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.restful-api.dev/objects");

                // Check if the response is successful (status code between 200 and 299)
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response Body:");
                    //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                    json = Newtonsoft.Json.Linq.JArray.Parse(responseBody.ToString());
                    Console.WriteLine(responseBody);
                    Console.WriteLine(json);
                    for (int i=0; i< json.Count; i++)
                    {
                        Console.WriteLine("ID: " + json[i]["id"]);
                        Console.WriteLine("Name: " + json[i]["name"]);
                        Console.WriteLine("Data: " + json[i]["data"]);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Request failed: " + e.Message);
            }
        }
    }
}

