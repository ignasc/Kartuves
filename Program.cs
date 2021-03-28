using System;
using System.Collections.Generic;

namespace Kartuves
{
    class Program
    {
        static void Main(string[] args) {
            Console.Clear();

            List<string> zodziuSarasas = new List<string>() { "namas", "mama", "plaktukas" };
            string spejamasZodis;
            string tavoSpejimas;
            int spejimoRezultatas;
            int bandymuSkaicius = 5;

            bool zodisAtspetas = false;
            bool zaidimasZaidziamas = true;

            List<char> jauAtspetosRaides = new List<char>();
            
            //-----Pradines zaidimo salygos-----
            spejamasZodis = GenerateRandomWord(zodziuSarasas);
            Console.WriteLine(spejamasZodis);

            //Generate _ symbols
            for(int i = 0; i < spejamasZodis.Length; i++) {
                jauAtspetosRaides.Add('_');
            }

            //----------Game Begins----------
            do {
                Meniu(spejamasZodis, jauAtspetosRaides, ref bandymuSkaicius);

                tavoSpejimas = Console.ReadLine();
                Console.Clear();//Clear console screen


                spejimoRezultatas = TikrinamSpejima(spejamasZodis, jauAtspetosRaides, tavoSpejimas.ToUpper(), ref bandymuSkaicius);
                Console.WriteLine(spejimoRezultatas);

                //Tikrinam galutines zaidimo salygas (dar nepilnos)
                Console.WriteLine("Tikrinam galutines zaidimo salygas");
                Console.WriteLine( jauAtspetosRaides.Equals(spejamasZodis) );
                if (bandymuSkaicius == 0 || zodisAtspetas == true) {
                    zaidimasZaidziamas = false;
                    Console.WriteLine("Zaidimas baigas");
                }
            } while (zaidimasZaidziamas);

            //--------------------------MAIN ENDS--------------------------
        }

        static void Meniu(string spejamasZodis, List<char> jauAtspetosRaides, ref int bandymuSkaicius) {
            Console.WriteLine("----------Atspek Zodi----------");
            Console.WriteLine("DEBUG: " + spejamasZodis + " ,Liko bandymu: " + bandymuSkaicius);
            for (int i = 0; i < jauAtspetosRaides.Count; i++) {
                Console.Write(jauAtspetosRaides[i] + " ");
            }
            Console.WriteLine();//New Line
            Console.Write("Tavo spejimas: ");
        }
        static string GenerateRandomWord(List<string> zodziuSarasas) {
            Random skaicius = new Random();
            return zodziuSarasas[skaicius.Next(0, zodziuSarasas.Count)].ToUpper();
        }

        static void TikrinamSpejima(string spejamasZodis, List<char> jauAtspetosRaides, string tavoSpejimas, ref int bandymuSkaicius) {
            // 1 - spejom raide
            // 2 - spejom zodi
            // 3 - netinkamas zodzio ilgis
            if (tavoSpejimas.Length == 1) {
                char zodzioRaide= tavoSpejimas.ToCharArray()[0];

                for (int i = 0; i < jauAtspetosRaides.Count; i++) {
                    
                    if (zodzioRaide == spejamasZodis[i]) {
                        jauAtspetosRaides[i] = zodzioRaide;
                    }
                }

                bandymuSkaicius--;
            }
            else if (tavoSpejimas.Length > 1 && tavoSpejimas.Length == spejamasZodis.Length) {
                Console.WriteLine("Spejai zodi: " + tavoSpejimas);
                bandymuSkaicius = 0;
            }
            else {
                Console.WriteLine("Per trumpas zodis, bandyk dar karta.");
            }
        }

        
    }
}
