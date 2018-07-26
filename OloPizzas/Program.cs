using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OloPizzas
{
    public class Program
    {
        #region Static Fields and Constants

        private const string Url = "http://files.olo.com/pizzas.json";

        #endregion

        #region Public Methods

        public static async Task Main(string[] args) => Output(await GroupAndOrderAsync(JsonConvert.DeserializeObject<List<Toppings>>(await GetJsonAsync())));

        #endregion

        #region Protected Methods

        protected static async Task<string> GetJsonAsync()
        {
            string json;
            using (var hc = new HttpClient())
            {
                json = await hc.GetStringAsync(Url);
            }

            return json;
        }

        protected static async Task<IOrderedEnumerable<IGrouping<string, Toppings>>> GroupAndOrderAsync(IEnumerable<Toppings> toppings)
        {
            return await Task.Run(() => toppings.GroupBy(t => t.ToppingString).OrderByDescending(grp => grp.Count()));
        }

        protected static void Output(IEnumerable<IGrouping<string, Toppings>> toppings)
        {
            var i = 1;
            foreach (var topping in toppings.Take(20))
            {
                Console.WriteLine($"{i++} {topping.Key} {topping.Count()}");
            }
        }

        #endregion
    }
}