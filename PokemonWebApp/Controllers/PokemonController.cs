using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PokemonWebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PokemonWebApp.Controllers
{
    public class PokemonController : Controller
    {
        string baseUrl = "https://localhost:44315";
        public async Task<ActionResult> Index()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/jason"));

                HttpResponseMessage response = await client.GetAsync("api/Pokemons");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(result);
                }
            }

            return View(pokemons);
        }
        public void OnGet()
        {

        }
    }
}
