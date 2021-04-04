using System;
using System.Collections.Generic;

namespace Kartuves
{
    class Program
    {
        static void Main(string[] args) {

            //ZodziuKonvertavimas();


            Console.Clear();

            string[] zodziuSarasas;
            string spejamasZodis;
            string tavoSpejimas;

            int bandymuSkaicius = 15;

            bool zodisAtspetas = false;
            bool zaidimasZaidziamas = true;

            List<char> jauAtspetosRaides = new List<char>();

            //-----Pradines zaidimo salygos-----

            //Nuskaitom faila su zodziais (kelias iki failo nurodytas dviem skirtingais budais)
            if (!System.IO.File.Exists(@"..\..\..\zodziai.txt")) {
                Console.WriteLine("KLAIDA: failas \"zodziai.txt\" nerastas!");
                return;
            }
            else {
                zodziuSarasas = System.IO.File.ReadAllLines("..\\..\\..\\zodziai.txt");
            }//Failo skaitymo pabaiga

            /*'\Kartuves\bin\Debug\netcoreapp3.1\zodziai.txt' is original root dir, where program searches for file if
             no path is defined: System.IO.File.ReadAllLines("zodziai.txt")*/

            spejamasZodis = GenerateRandomWord(zodziuSarasas);
            //Console.WriteLine("Debug: " + spejamasZodis);

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
            Console.WriteLine("Liko bandymu: " + bandymuSkaicius);
            for (int i = 0; i < jauAtspetosRaides.Count; i++) {
                Console.Write(jauAtspetosRaides[i] + " ");
            }
            Console.WriteLine();//New Line
            Console.Write("Tavo spejimas: ");
        }

        static string GenerateRandomWord(string[] zodziuSarasas) {
            Random skaicius = new Random();
            return zodziuSarasas[skaicius.Next(0, zodziuSarasas.Length)].ToUpper();
        }

        static void TikrinamSpejima(string spejamasZodis, List<char> jauAtspetosRaides, string tavoSpejimas, ref int bandymuSkaicius) {
            if (tavoSpejimas.Length == 1) {//Spejama vienta raide
                char zodzioRaide = tavoSpejimas.ToCharArray()[0];

                for (int i = 0; i < jauAtspetosRaides.Count; i++) {
                    
                    if (zodzioRaide == spejamasZodis[i]) {
                        jauAtspetosRaides[i] = zodzioRaide;
                    }
                }

                bandymuSkaicius--;
            }
            else if (tavoSpejimas.Length > 1 && tavoSpejimas.Length == spejamasZodis.Length) {//Spejamas visas zodis
                char[] tavoSpejimoRaide = tavoSpejimas.ToCharArray();
                
                for (int i = 0; i < jauAtspetosRaides.Count; i++) {
                    jauAtspetosRaides[i] = tavoSpejimoRaide[i];
                }

                bandymuSkaicius = 0;
            }
            else {//Netinkamas zodzio ilgis
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

        /*static void ZodziuKonvertavimas() {

        //Cia purvinas kodas, greitai reikejo pakeisti lietuviskas raides, kad nereiktu ju naudoti zaidime...

            string[] zodziuSarasas = System.IO.File.ReadAllLines("..\\..\\..\\visasSarasas.txt");
            char[] blogosRaides = { 'ą', 'č', 'ę', 'ė', 'į', 'š', 'ų', 'ū', 'ž' };
            char[] gerosRaides = { 'a', 'c', 'e', 'e', 'i', 's', 'u', 'u', 'z' };
            string[] naujasZodziuSarasas = new string[zodziuSarasas.Length];
            string naujasZodis;
            bool raideYraBloga = false;

            for (int i = 0; i < zodziuSarasas.Length; i++) {//Imam kiekviena zodi masyve
                naujasZodis = "";
                
                foreach (char tikrinamaRaide in zodziuSarasas[i]) {//Imam kiekvieno masyve esancio zodzio raide
                    raideYraBloga = false;
                    for (int j = 0; j < blogosRaides.Length; j++) {//Imam kiekviena lietuviska raide tikrinimui
                        
                        if (tikrinamaRaide == blogosRaides[j]) {//Pagaliau, lyginimas...

                            naujasZodis = naujasZodis + gerosRaides[j];
                            raideYraBloga = true;
                            continue;
                        }
                    }

                    if (!raideYraBloga) {
                        naujasZodis = naujasZodis + tikrinamaRaide;
                    }
                }

                naujasZodziuSarasas[i] = naujasZodis;
            }

            //Sita patikrinti, ar tikrai gerai nurodytas file path, kur issaugoti faila (ar nereikia relative path)
            System.IO.File.WriteAllLines("sutvarkytasSarasas.txt",naujasZodziuSarasas);

        }*/



    }
}
