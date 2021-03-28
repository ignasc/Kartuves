using System;
using System.Collections.Generic;

namespace Kartuves
{
    class Program
    {
        static void Main(string[] args) {
            List<string> zodziuSarasas = new List<string>() { "namas", "mama", "plaktukas" };
            string spejamasZodis;
            int spejimoRezultatas;

            List<char> jauAtspetosRaides = new List<char>();
            

            spejamasZodis = GenerateRandomWord(zodziuSarasas);
            Console.WriteLine(spejamasZodis);

            //Generate _ symbols
            for(int i = 0; i < spejamasZodis.Length; i++) {
                jauAtspetosRaides.Add('_');
            }
            
            string tavoSpejimas;

            //----------Game Begins----------
            Meniu(spejamasZodis, jauAtspetosRaides);

            tavoSpejimas = Console.ReadLine();

            
            spejimoRezultatas = TikrinamSpejima(spejamasZodis, jauAtspetosRaides, tavoSpejimas);
            Console.WriteLine(spejimoRezultatas);



        }

        static void Meniu(string spejamasZodis, List<char> jauAtspetosRaides) {
            Console.WriteLine("----------Atspek Zodi----------");
            Console.WriteLine("DEBUG: " + spejamasZodis);
            for (int i = 0; i < jauAtspetosRaides.Count; i++) {
                Console.Write(jauAtspetosRaides[i] + " ");
            }
            Console.WriteLine();//New Line
            Console.Write("Tavo spejimas: ");
        }
        static string GenerateRandomWord(List<string> zodziuSarasas) {
            Random skaicius = new Random();
            return zodziuSarasas[skaicius.Next(0, zodziuSarasas.Count)];
        }

        static int TikrinamSpejima(string spejamasZodis, List<char> jauAtspetosRaides, string tavoSpejimas) {
            // 1 - spejo raide
            // 2 - spejo zodi
            // 3 - netinkamas zodzio ilgis
            if (tavoSpejimas.Length == 1) {
                Console.WriteLine("Spejai raide: " + tavoSpejimas);
                return 1;
            }
            else if (tavoSpejimas.Length > 1 && tavoSpejimas.Length == spejamasZodis.Length) {
                Console.WriteLine("Spejai zodi: " + tavoSpejimas);
                return 2;
            }
            else {
                Console.WriteLine("Per trumpas zodis, bandyk dar karta.");
                return 3;
            }
        }
        
    }
}
