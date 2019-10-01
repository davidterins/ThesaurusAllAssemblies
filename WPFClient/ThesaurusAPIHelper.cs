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
  /// <summary>
  /// A class with methods to communicate the Thesaurus API.
  /// </summary>
  public class ThesaurusAPIHelper
  {
    private static readonly string BaseURL = "https://localhost:44349/api/";

    /// <summary>
    /// Gets all the words existing in the Thesaurus API.
    /// </summary>
    public static async Task<IEnumerable<string>> GetWordsAsync()
    {
      try
      {
        using (HttpResponseMessage response = await App.Client.GetAsync($"{BaseURL}thesaurus"))
        {
          if (response.StatusCode == HttpStatusCode.OK)
          {
            var result = await response.Content.ReadAsAsync<IEnumerable<string>>();

            if (result != null)
            {
              return result;
            }
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }

      return new List<string>();
    }

    /// <summary>
    /// Gets all synonyms for the specified word from the Thesaurus API.
    /// </summary>
    /// <param name="word"></param>
    public static async Task<IEnumerable<string>> GetSynonymsAsync(string word)
    {
      try
      {
        using (HttpResponseMessage response = await App.Client.GetAsync($"{BaseURL}thesaurus/{word}"))
        {
          if (response.StatusCode == HttpStatusCode.OK)
          {
            var result = await response.Content.ReadAsAsync<IEnumerable<string>>();
            if (result != null)
            {
              return result;
            }
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }

      return new List<string>();
    }

    /// <summary>
    /// Add synonyms to the a Theasurus API
    /// </summary>
    /// <param name="synonyms">Synonyms to be added</param>
    public static async Task AddSynonymsAsync(IEnumerable<string> synonyms)
    {
      try
      {
        using (HttpResponseMessage response = await App.Client.PostAsJsonAsync($"{BaseURL}thesaurus", synonyms))
        {
          if (response.StatusCode == HttpStatusCode.OK)
          {
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
