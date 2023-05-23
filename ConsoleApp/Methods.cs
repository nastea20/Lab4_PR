using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
     public class Methods
     {
          static readonly HttpClient client = new HttpClient();
          static readonly string BASE_URL = "https://localhost:44370/api/";

          public void GetCategories()
          {
               var response = client.GetAsync(BASE_URL + "Category/categories").Result;
               if (response.IsSuccessStatusCode)
               {
                    var categories = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    foreach (var category in categories)
                    {
                         Console.WriteLine($"{category.id} - {category.name}");
                    }
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

          public void CategoryDetail()
          {
               Console.WriteLine("Introduceti ID-ul categoriei a cărei detalii vrei să vezi: ");
               var id = Console.ReadLine();
               var response = client.GetAsync(BASE_URL + $"Category/categories/{id}").Result;
               if (response.IsSuccessStatusCode)
               {
                    var categoryDetails = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(categoryDetails);
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

          public void PostCategory()
          {
               Console.WriteLine("Introduceti numele noii categorii: ");
               var category_name = Console.ReadLine();
               var data = new { title = category_name };
               var response = client.PostAsJsonAsync(BASE_URL + "Category/categories", data).Result;
               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Cererea a fost indeplinita cu succes!");
                    GetCategories();
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

          public void CategoryDelete()
          {
               Console.WriteLine("Introduceti ID-ul categoriei care vrei sa o stergi: ");
               var id = Console.ReadLine();
               var response = client.DeleteAsync(BASE_URL + $"Category/categories/{id}").Result;
               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Categoria a fost stearsa cu succes!");
               }
               else
               {
                    Console.WriteLine("Eroare la stergerea categoriei: " + response.StatusCode);
               }
          }

          public void CategoryPut()
          {
               Console.WriteLine("Introduceti ID-ul categoriei pe care vrei sa-l modifici: ");
               var id = Console.ReadLine();
               Console.WriteLine("Introduceti un nou nume pentru categorie: ");
               var category_name = Console.ReadLine();
               var data = new { title = category_name };
               var url = BASE_URL + $"Category/{id}";
               var response = client.PutAsJsonAsync(url, data).Result;
               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Categoria a fost modificata cu succes");
               }
               else
               {
                    Console.WriteLine("Eroare la modificarea categoriei: " + response.StatusCode);
               }
          }



          public void PostProducts()
          {
               Console.WriteLine("Introduceti id-ul pentru categoria dorita: ");
               string id = Console.ReadLine();

               Console.WriteLine("Introduceti numele noului produs: ");
               string prodName = Console.ReadLine();

               Console.WriteLine("Introduceti pretul: ");
               decimal pret = Decimal.Parse(Console.ReadLine());

               var data = new
               {
                    title = prodName,
                    price = pret,
                    categoryId = id
               };

               string url = "https://localhost:44370/api/Category/categories/" + id + "/products";

               client.DefaultRequestHeaders.Accept.Clear();
               client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

               HttpResponseMessage response = client.PostAsJsonAsync(url, data).Result;

               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Success");
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

          public async Task GetProducts()
          {
               Console.Write("Introduceti ID-ul pentru categoria dorita: ");
               string id = Console.ReadLine();

               string url = BASE_URL + $"Category/categories/{id}/products";
               using (var client = new HttpClient())
               {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                         dynamic products = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                         foreach (var product in products)
                         {
                              Console.WriteLine(product);
                         }
                    }
                    else
                    {
                         Console.WriteLine("Cererea a eșuat cu codul de stare: {0}", response.StatusCode);
                    }
               }
          }

     }
}
