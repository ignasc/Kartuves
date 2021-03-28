using System;
using System.Collections.Generic;

namespace Kartuves
{
    class Program
    {
        static void Main(string[] args) {
            Console.Clear();

            List<string> zodziuSarasas = new List<string>() { "namas", "mama", "medis" };
            string spejamasZodis;
            string tavoSpejimas;
            int bandymuSkaicius = 5;

            bool zodisAtspetas = false;
            bool zaidimasZaidziamas = true;

            List<char> jauAtspetosRaides = new List<char>();
            
            //-----Pradines zaidimo salygos-----
            spejamasZodis = GenerateRandomWord(zodziuSarasas);
            Console.WriteLine("Debug: " + spejamasZodis);

            //Generate _ symbols
            for(int i = 0; i < spejamasZodis.Length; i++) {
                jauAtspetosRaides.Add('_');
            }

            //----------Game Begins----------
            do {
                Meniu(spejamasZodis, jauAtspetosRaides, ref bandymuSkaicius);

                tavoSpejimas = Console.ReadLine();
                Console.Clear();//Clear console screen


                TikrinamSpejima(spejamasZodis, jauAtspetosRaides, tavoSpejimas.ToUpper(), ref bandymuSkaicius);


                //Tikrinam galutines zaidimo salygas (dar nepilnos)
                TikrinamGalutinesZaidimoSalygas(ref zodisAtspetas, ref zaidimasZaidziamas, ref jauAtspetosRaides, ref spejamasZodis, ref bandymuSkaicius);
                if (bandymuSkaicius == 0 || zodisAtspetas == true) {
                    ZaidimoPabaiga(ref zodisAtspetas, jauAtspetosRaides, ref spejamasZodis, ref bandymuSkaicius);
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
            if (tavoSpejimas.Length == 1) {
                char zodzioRaide = tavoSpejimas.ToCharArray()[0];

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

        static void TikrinamGalutinesZaidimoSalygas(ref bool zodisAtspetas, ref bool zaidimasZaidziamas, ref List<char> jauAtspetosRaides, ref string spejamasZodis, ref int bandymuSkaicius) {
            char tikrinamaKiekvienaRaide;
            bool arVisosRaidesSutampa = true;
            bool arTesiamZaidimaToliau = true;

            for (int i = 0; i < spejamasZodis.Length; i++) {
                tikrinamaKiekvienaRaide = spejamasZodis[i];
                if (tikrinamaKiekvienaRaide != jauAtspetosRaides[i]) {
                    arVisosRaidesSutampa = false;
                }
            }

            if (arVisosRaidesSutampa || bandymuSkaicius == 0) {
                arTesiamZaidimaToliau = false;
            }

            zodisAtspetas = arVisosRaidesSutampa;
            zaidimasZaidziamas = arTesiamZaidimaToliau;
        }

        static void ZaidimoPabaiga(ref bool zodisAtspetas, List<char> jauAtspetosRaides, ref string spejamasZodis, ref int bandymuSkaicius) {
            Console.Clear();
            if (zodisAtspetas) {
                Console.WriteLine("Tu atspejai zodi!"); 
                Console.WriteLine("Spejamas zodis buvo " + spejamasZodis);
                Console.WriteLine("Bandymu liko: " + bandymuSkaicius);
                return;
            }
            else if (!zodisAtspetas) {
                Console.WriteLine("Tu neatspejai zodzio!");
                Console.WriteLine("Spejamas zodis buvo " + spejamasZodis);
                Console.Write("Tavo spejimu rezultatas: ");
                foreach (char raide in jauAtspetosRaides) {
                    Console.Write(raide + " ");
                }
                Console.WriteLine();//New line
            }


        }



    }
}
