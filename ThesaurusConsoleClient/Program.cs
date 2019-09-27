using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThesaurusConsoleClient
{
  class Program
  {
    /*private static string apiURL = "https://localhost:5001/api/thesaurus";*/

    private static string apiURL = "https://localhost:5001/api/thesaurus";
    //192.168.10.108
    static void Main(string[] args)
    {
      GetAsync().Wait();
      //var obj = client.UploadString(apiURL);

      //var s = client.OpenRead(apiURL);

      //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
      //request.Method = "GET";
      //var test = string.Empty;

      //try
      //{
      //  using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
      //  {
      //    Stream dataStream = response.GetResponseStream();
      //    StreamReader reader = new StreamReader(dataStream);
      //    test = reader.ReadToEnd();
      //    reader.Close();
      //    dataStream.Close();
      //  }
      //}
      //catch (Exception e)
      //{
      //  Console.WriteLine(e);
      //  //throw;
      //}

      //Console.WriteLine(test);

      Console.ReadKey();
      //DeserializeObject(test...)
    }


    private static async Task GetAsync()
    {
      HttpClient client = new HttpClient();


      client.BaseAddress = new Uri(apiURL);
      client.DefaultRequestHeaders.Accept.Clear();
      //client.de

      try
      {
        HttpResponseMessage response = await client.GetAsync(apiURL);
        if (response.StatusCode == HttpStatusCode.OK)
        {
          var list = await response.Content.ReadAsStringAsync();

          Console.WriteLine(list);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
     

    }
  }
}
