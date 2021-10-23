using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PokemonWebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PokemonWebApp.Pages
{
    public class PokemonModel : PageModel
    {

        public List<Pokemon> Pokemons { get; private set; } = new List<Pokemon>();
        string baseUrl = "https://localhost:44315";

        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Pokemons");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(result);
                }
            }
        }
    }
}
