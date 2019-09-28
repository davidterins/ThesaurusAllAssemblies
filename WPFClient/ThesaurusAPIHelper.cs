using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient
{
  public class ThesaurusAPIHelper
  {
    private static string BaseURL = "https://localhost:44349/api/";

    public static async Task<IEnumerable<string>> GetWordsAsync()
    {
      try
      {
        using (HttpResponseMessage response = await App.Client.GetAsync($"{BaseURL}thesaurus"))
        {
          if (response.StatusCode == HttpStatusCode.OK)
          {
            var result = await response.Content.ReadAsAsync<IEnumerable<string>>();

            return result;
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }

      return null;
    }


    public static async Task<IEnumerable<string>> GetSynonymsAsync(string word)
    {
      try
      {
        using (HttpResponseMessage response = await App.Client.GetAsync($"{BaseURL}thesaurus/{word}"))
        {
          if (response.StatusCode == HttpStatusCode.OK)
          {
            var result = await response.Content.ReadAsAsync<IEnumerable<string>>();

            return result;
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }

      return null;
    }


    public static async Task AddSynonymsAsync(IEnumerable<string> synonyms)
    {
      try
      {
        using (HttpResponseMessage response = await App.Client.PostAsJsonAsync($"{BaseURL}thesaurus", synonyms))
        {
          if (response.StatusCode == HttpStatusCode.OK)
          {
            Console.WriteLine("Posted OK");
          }
          else
          {
            Console.WriteLine(response.StatusCode);
          }
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
