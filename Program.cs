using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace Bloggen
{
    class Program
    {
        static List<string[]> bloggen = new List<string[]>(); // här lagrar jag mina blogginlägg i min lista som ska innehålla array
        static void Main(string[] args)
        {
            while (true) // loop för att upprepa meny tills program avslutas
            {                
                int val;
                Console.Clear();
                Console.WriteLine("Välkommen till menyn! Var god gör ditt val: ");
                Console.WriteLine("[1] Skapa ett nytt inlägg: ");
                Console.WriteLine("[2] Skriv ut dina inlägg: ");
                Console.WriteLine("[3] Sortera mina inlägg efter datum: ");
                Console.WriteLine("[4] Sök efter titel linjär: ");
                Console.WriteLine("[5] Extra Sortera efter författare: ");
                Console.WriteLine("[6] Extra Sök efter författare binär: ");
                Console.WriteLine("[7] Extra Redigera i ett inlägg: ");
                Console.WriteLine("[8] Extra Radera ett inlägg: ");
                Console.WriteLine("[9] Stäng ner bloggen: ");            
            
                if (int.TryParse(Console.ReadLine(), out val)) // felhantering som kollar att användaren anger en siffra
                {
                    if (val >= 1 && val <= 9) // Kontrollera att inmatningen är inom intervallet 1-9
                    {
                        switch (val)
                        {
                            case 1:
                                bloggInlägg();
                                break;
                            case 2:
                                antalInlägg();
                                break;
                            case 3:
                                sorteraInlägg();
                                antalInlägg(); // skriver ut inläggen efter sortering
                                break;
                            case 4:
                                sökInlägg();
                                break;
                            case 5: 
                                sorteraFörfattare();
                                antalInlägg(); // skriver ut inläggen efter sortering
                                break;
                            case 6:
                                sorteraFörfattare();
                                antalInlägg();
                                sökFörfattare();
                                break;
                            case 7: 
                                redigeraInlägg();
                                break;
                            case 8:
                                raderaInlägg();
                                break;
                            case 9:
                                stängBloggen();
                                return;
                            default:
                                Console.WriteLine("Var god ange ett nummer mellan 1-9");
                                break;
                        }
                        Console.WriteLine("Tryck enter för att återgå!"); // lägger den utanför min switch för att gälla för alla
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Ogiltig inmatning. Ange ett heltal.");
                    }
                }
            }
        }
        // Nedan kommer mina metoder jag byggt för mitt program
         static void bloggInlägg() // skapar min metod för att skriva och spara inlägg
        {
            bool isRunning = true;
            while (isRunning) // låter användaren vara kvar i att skapa fler inlägg tills den avbryts och återvänder till huvudmeny
            {
                string[] bloggInlägg = new string[4]; // min vektor med 3 element. Skapas en ny vektor för varje inlägg
                Console.WriteLine("Skriv in din titel:");
                bloggInlägg[0] = Console.ReadLine(); // sparar titel i element 0
                Console.WriteLine("Skriv inlägget:");
                bloggInlägg[1] = Console.ReadLine(); // sparar text i element 1
                bloggInlägg[2] = DateTime.Now.ToString(); // sparar datum i element 2
                Console.WriteLine("Ange vem som skrivit inlägget");
                bloggInlägg[3] = Console.ReadLine(); // sparar vem som skrivit inlägget i element 3
                bloggen.Add(bloggInlägg); // sparar mitt blogginlägg i min lista bloggen.
                Console.WriteLine("Ditt inlägg har sparats!");
                Console.Write("Vill du skapa ett nytt inlägg (J/N)? ");
                string svar = Console.ReadLine().ToLower(); // ToLower för omvandla svar till små bokstäver
                if (svar != "j")
                {
                    Console.Clear();
                    Console.WriteLine("Återvänder till menyn.");
                    isRunning = false;
                }
            }
        }

        static void antalInlägg() // skriver ut och räknar mina inlägg
        {
            Console.WriteLine($"Antal inlägg: {bloggen.Count}\n"); // räknar totala inlägg och skriver ut siffran
            for (int i = 0; i < bloggen.Count; i++) // loopar igenom min lista 
            {
            Console.WriteLine($"Inlägg {i + 1}\nTitel: {bloggen[i][0]}\nInlägg: {bloggen[i][1]}\nSparat: {bloggen[i][2]}\nFörfattare:: {bloggen[i][3]}\n");
            }
        }

        static void sorteraInlägg() // Använder bubbelmetoden mot datum
        {
            if (bloggen.Count == 0)
            {
                Console.WriteLine("Inga inlägg att sortera");
                return;
            }
                int nummer = bloggen.Count; // lagrar antalet i nummer
                for (int i = 0; i < nummer -1; i++) // yttre loop som går igenom hela min lista
            {
                for (int j = 0; j < nummer - i -1; j++) // innre loop som går igenom element för element
                {
                    DateTime datum1 = DateTime.Parse(bloggen[j][2]); // lagrar datum från min lista för att kunna jämföra
                    DateTime datum2 = DateTime.Parse(bloggen[j+1][2]); // lagrar datum likt ovan fast +1 i listan för att jämföra
                    if (datum1 > datum2) // jämför 2 datum för att se vilket som är skapat senast
                    {
                        string[] temp = bloggen[j]; // skapar min temporära variabel och lagrar värdet
                        bloggen[j] = bloggen[j+1]; // byter plats på mina värden
                        bloggen[j+1] = temp; // ersätter värdet med temp värdet.
                    }
                }
            }
            Console.WriteLine("Sortering lyckades! \nHär kommer sorterad lista:\n ");
        }

        static void sökInlägg() // linjär sökning
        {
            if (bloggen.Count == 0)
            {
                Console.WriteLine("Finns inga inlägg att söka. Vänligen skapa ett först");
                return;
            }
            Console.WriteLine("Sök efter en titel: ");
            string sökord = Console.ReadLine();
            bool hittad = false;

            for (int i = 0; i < bloggen.Count; i++)
            {
                if (sökord.ToLower() == bloggen[i][0].ToLower())
                {
                hittad = true;
                Console.WriteLine($"Titeln finns på indexplats {i}");
                }       
            }
                if(hittad == false)
                {
                Console.WriteLine("Jag kunde tyvärr inte hitta din sökning. Försök igen: ");
                }
        }
                static void sorteraFörfattare() // sortera med .Sort och .CompareTo
        {
            if (bloggen.Count == 0)
            {
                Console.WriteLine("Inga inlägg att sortera");
                return;
            }
            bloggen.Sort((a, b) => a[3].CompareTo(b[3])); // Sortera listan efter författare (element 3)
            Console.WriteLine("Sortering lyckades! \nHär kommer sorterad lista:\n ");
        }

        static void sökFörfattare() // binärsökning
        {
            bool isRunning = true;
            if (bloggen.Count > 0)
            {
                {
                    Console.WriteLine("Sök efter författare");
                    string key = Console.ReadLine().ToLower(); // key = ordet vi söker
                    int första = 0; // Sätter variabel 0 i listan vi söker
                    int sista = bloggen.Count -1; // skapar en sista variabel min listas längd -1                
                    while (första <= sista )
                    {
                    int mellan = (första + sista) / 2;                   
                    if (bloggen[mellan][3].ToString().CompareTo(key) > 0)
                    {
                    sista = mellan -1; // sista variabeln får värdet av mellan -1
                    }
                    else if (bloggen[mellan][3].ToString().CompareTo(key) < 0) // jämför två strängar element 4 mot key (sökordet)
                    {
                    första = mellan +1; // första variabeln får värdet av mellan +1
                    }
                    else
                    {
                    Console.WriteLine($"Författaren {key} du har sökt efter finns på plats {mellan}"); // skriver ut vart författaren finns i min lista
                    isRunning = false;
                    break;
                    }
                    }
                    if (isRunning)
                    {
                        Console.WriteLine($"Författaren {key} kunde inte hittas");
                    }
                }
            }
                    else
                    {
                        Console.WriteLine("Du måste först sortera listan med författare innan du kan använda binärsökningen.");
                        return;
                    }
        }  

        static void redigeraInlägg()
        {
            bool isRunning = true;
            while (isRunning)
            {                          
                Console.WriteLine("Ange indexplats på inlägg du vill redigera:");
                if (int.TryParse(Console.ReadLine(), out int i) && i >= 1 && i <= bloggen.Count) // felhantering konvertera till heltal lagras i index.
                {
                    i--; // Minskar index med -1 så vi hittar första element på plats 0
                    Console.WriteLine($"Nuvarande titel: {bloggen[i][0]}"); // skriver ut min gamla titel

                    Console.WriteLine("Ange den nya titeln:");
                    string nyTitel = Console.ReadLine();

                    Console.WriteLine("Ange ny text till inlägget:");
                    string nyttInlägg = Console.ReadLine();

                    Console.WriteLine("Ange vem som skrivit:");
                    string författare = Console.ReadLine();

                    bloggen[i][0] = nyTitel;
                    bloggen[i][1] = nyttInlägg;
                    bloggen[i][3] = författare;

                    Console.WriteLine("Inlägget har sparats.");
                    isRunning = false; // avslutar min loop vid lyckad redigering
                }
                else
                {
                    Console.WriteLine("Jag kunde inte hitta inlägget du vill redigera. Vill du försöka igen? J/N");
                    string svar = Console.ReadLine().ToLower();
                    if (svar != "j")
                    {
                        Console.Clear();
                        Console.WriteLine("Återvänder till menyn.");
                        isRunning = false; // avslutar loop om användaren inte anger j
                    }
                }
            }
        }

        static void raderaInlägg()
        {
            if (bloggen.Count == 0)
            {
                Console.WriteLine("Inga inlägg att sortera");
                return;
            }
            bool isRunning = true;
            while (isRunning) // skapar en loop som upprepeas tills användaren lyckas med radering då ändras isRunning till false              
            {
                antalInlägg(); // Visar alla inlägg och dess nummer
                Console.WriteLine("För att radera ett specifikt inlägg var god ange vilket genom att skriva inläggets nummer:");
                if (int.TryParse(Console.ReadLine(), out int val)) // Felhantering för att ta mot nummer
                {
                    if (val >= 1 && val <= bloggen.Count) // Valet måste vara minst 1 och får mest vara lika många som jag har inlägg i bloggen.
                    {
                        int index = val - 1; // minus 1 för att få fram rätt indexplats
                        bloggen.RemoveAt(index); // tar bort inlägg från min    lista

                        Console.WriteLine("Inlägg borttaget!");
                        isRunning = false; // avslutar loopen och återgår till huvudmeny
                    }
                    else
                    {
                        Console.WriteLine("Ogiltligt nummer försök igen");
                    }
                }
            }
        }

        static void stängBloggen()
        {
            Console.WriteLine("Stänger ner bloggen. Räknar ner: 3, 2, 1");
            for (int i = 3; i >= 1; i--) // skapar en loop för att räkna ner. Ett roligare avslut på mitt program
            {
                Console.WriteLine(i);
                Thread.Sleep(500);
            }
            Console.WriteLine("Bloggen stängd.");
        }
    }
}