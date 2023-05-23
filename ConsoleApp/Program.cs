using System;

namespace ConsoleApp
{
     class Program
     {
          static void Main(string[] args)
          {
               Console.WriteLine("            Cereri HTTP");
               Console.WriteLine("                                        ");

               var methods = new Methods();

               while (true)
               {
                    Console.WriteLine("1. Obtineti lista de categorii");
                    Console.WriteLine("2. Creati o noua categorie");
                    Console.WriteLine("3. Afisati detalii despre o categorie");
                    Console.WriteLine("4. Sterge o categorie");
                    Console.WriteLine("5. Modifică titlul unei categorii");
                    Console.WriteLine("6. Creaza un nou produs");
                    Console.WriteLine("7. Afisati produsele pentru o anumita categorie");
                    Console.WriteLine("8. Iesiti din program");

                    Console.Write("Introduceti optiunea: ");
                    var option = Console.ReadLine();

                    switch (option)
                    {
                         case "1":
                              methods.GetCategories();
                              break;
                         case "2":
                              methods.PostCategory();
                              break;
                         case "3":
                              methods.CategoryDetail();
                              break;
                         case "4":
                              methods.CategoryDelete();
                              break;
                         case "5":
                              methods.CategoryPut();
                              break;
                         case "6":
                              methods.PostProducts();
                              break;
                         case "7":
                              methods.GetProducts().Wait();
                              break;
                         case "8":
                              return;
                         default:
                              Console.WriteLine("Optiunea introdusa nu este valida. Va rugam sa reintroduceti optiunea.");
                              break;
                    }
               }
          }
     }
}
