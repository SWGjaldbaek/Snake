using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

internal class Program
{
    static void TEGN(List<int> x_pos, List<int> y_pos)
    {
        Console.Clear();
        for (int i = 0; i < x_pos.Count; i++)
        {
            Console.SetCursorPosition(x_pos[i], y_pos[i]);
            Console.Write("X");
        }
    }
    static void VENSTRE(List<int> x_pos, List<int> y_pos)
    {
        if (y_pos == null || y_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        if (x_pos == null || x_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        // Indsæt en ny X værdi forrest: siden den skal gå til VENSTRE skal den forreste værdi +1 
        x_pos.Insert(0, x_pos[0] - 1);
        // Fjern det sidste element
        x_pos.RemoveAt(x_pos.Count - 1);
        // Indsæt en ny Y værdi forrest: siden den skal gå til VENSTRE skal den nye forreste værdi være lig med den gamle forreste værdi
        y_pos.Insert(0, y_pos[0]);
        // Fjern det sidste element
        y_pos.RemoveAt(y_pos.Count - 1);
    }

    static void HØJRE(List<int> x_pos, List<int> y_pos)
    {
        if (y_pos == null || y_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        if (x_pos == null || x_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        // Indsæt en ny X værdi forrest: siden den skal gå til HØJRE skal den forreste værdi +1 
        x_pos.Insert(0, x_pos[0] + 1);
        // Fjern det sidste element
        x_pos.RemoveAt(x_pos.Count - 1);
        // Indsæt en ny Y værdi forrest: siden den skal gå til HØJRE skal den nye forreste værdi være lig med den gamle forreste værdi
        y_pos.Insert(0, y_pos[0]);
        // Fjern det sidste element
        y_pos.RemoveAt(y_pos.Count - 1);
    }
    static void NED(List<int> x_pos, List<int> y_pos)
    {
        if (y_pos == null || y_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        if (x_pos == null || x_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        // Indsæt en ny X værdi forrest: siden den skal gå til NED skal den nye forreste værdi være lig med den gamle forreste værdi
        x_pos.Insert(0, x_pos[0]);
        // Fjern det sidste element
        x_pos.RemoveAt(x_pos.Count - 1);
        // Indsæt en ny Y værdi forrest: siden den skal gå til NED skal den forreste værdi +1 (y bliver mindre op ad)
        y_pos.Insert(0, y_pos[0] + 1);
        // Fjern det sidste element
        y_pos.RemoveAt(y_pos.Count - 1);

    }
    static void OP(List<int> x_pos, List<int> y_pos)
    {
        if (y_pos == null || y_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        if (x_pos == null || x_pos.Count == 0)
            return; // Undgå fejl ved tom liste
        // Indsæt en ny X værdi forrest: siden den skal gå til OP skal den nye forreste værdi være lig med den gamle forreste værdi
        x_pos.Insert(0, x_pos[0]);
        // Fjern det sidste element
        x_pos.RemoveAt(x_pos.Count - 1);
        // Indsæt en ny Y værdi forrest: siden den skal gå til OP skal den forreste værdi - 1 (y bliver mindre op ad)
        y_pos.Insert(0, y_pos[0] - 1);
        // Fjern det sidste element
        y_pos.RemoveAt(y_pos.Count - 1);

    }
    
    static bool Alive(List<int> x_pos, List<int> y_pos)
    {
        //Kontrollere spillets bander
        if (x_pos[0] == 0 || y_pos[0]==0|| x_pos[x_pos.Count-1] == 115 || y_pos[y_pos.Count-1] == 115)
        {
            return false;
        }
        //Kontrollere om slangen spiser sig selv
        //x_pos[0],y_pos[0] er slangens start position.x_pos.Count=y_pos.count
        for(int i = 1; i < x_pos.Count; i++)
        {       //i marker kordinatsættet vi er nået til at kontrollere. 
            if (x_pos[0] == x_pos[i] && y_pos[0] == y_pos[i])   //kontrol om hovedet er på en del af slangen. 
                return false;
        }

        return true;    //Slangen er stadig i live hvis ingen dødsbetingelser er opfyldt. 

    }

    private static void Main(string[] args)
    {
        List<int> x_pos = new List<int>();
        List<int> y_pos = new List<int>();


        // start placering
        x_pos.Add(33);
        x_pos.Add(34);
        x_pos.Add(35);
        x_pos.Add(36);
        x_pos.Add(37);

        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);
        TEGN(x_pos, y_pos);

        do
        {
            VENSTRE(x_pos, y_pos);
            TEGN(x_pos, y_pos);
            Thread.Sleep(200);

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                while (true)
                {
                   
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            while (true)
                            {
                                // Tjekker om der er trykket på en tast uden at blokere løkken
                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo keyInfo1 = Console.ReadKey(intercept: true);

                                    // Hvis Q, Pil op eller Pil ned trykkes, brydes løkken
                                    if (keyInfo1.Key == ConsoleKey.Q ||
                                        keyInfo1.Key == ConsoleKey.UpArrow ||
                                        keyInfo1.Key == ConsoleKey.DownArrow)
                                    {
                                        keyInfo = keyInfo1;  // Opdaterer keyInfo, så switch ved hvad der skal ske bagefter
                                        break;
                                    }
                                }
                                if (!Alive(x_pos, y_pos))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Du tabte.");
                                    Console.WriteLine("Din score blev: ");
                                    break;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                HØJRE(x_pos, y_pos);
                                TEGN(x_pos, y_pos);
                                Thread.Sleep(100);
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            while (true)
                            {
                                // Tjekker om der er trykket på en tast uden at blokere løkken
                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo keyInfo1 = Console.ReadKey(intercept: true);

                                    // Hvis Q, Pil op eller Pil ned trykkes, brydes løkken
                                    if (keyInfo1.Key == ConsoleKey.Q ||
                                        keyInfo1.Key == ConsoleKey.UpArrow ||
                                        keyInfo1.Key == ConsoleKey.DownArrow)
                                    {
                                        keyInfo = keyInfo1;  // Opdaterer keyInfo, så switch ved hvad der skal ske bagefter
                                        break;
                                    }
                                }
                                if (!Alive(x_pos, y_pos))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Du tabte.");
                                    Console.WriteLine("Din score blev: ");
                                    break;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                VENSTRE(x_pos, y_pos);
                                TEGN(x_pos, y_pos);
                                Thread.Sleep(100);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            while (true)
                            {
                                // Tjekker om der er trykket på en tast uden at blokere løkken
                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo keyInfo1 = Console.ReadKey(intercept: true);

                                    // Hvis Q, Pil Højre eller Pil venstre trykkes, brydes løkken
                                    if (keyInfo1.Key == ConsoleKey.Q ||
                                        keyInfo1.Key == ConsoleKey.LeftArrow ||
                                        keyInfo1.Key == ConsoleKey.RightArrow)
                                    {
                                        keyInfo = keyInfo1;  // Opdaterer keyInfo, så switch ved hvad der skal ske bagefter
                                        break;
                                    }
                                }
                                if (!Alive(x_pos, y_pos))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Du tabte.");
                                    Console.WriteLine("Din score blev: ");
                                    break;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                OP(x_pos, y_pos);
                                TEGN(x_pos, y_pos);
                                Thread.Sleep(100);
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            while (true)
                            {
                                // Tjekker om der er trykket på en tast uden at blokere løkken
                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo keyInfo1 = Console.ReadKey(intercept: true);

                                    // Hvis Q, Pil Højre eller Pil venstre trykkes, brydes løkken
                                    if (keyInfo1.Key == ConsoleKey.Q ||
                                        keyInfo1.Key == ConsoleKey.LeftArrow ||
                                        keyInfo1.Key == ConsoleKey.RightArrow)
                                    {
                                        keyInfo = keyInfo1;  // Opdaterer keyInfo, så switch ved hvad der skal ske bagefter
                                        break;
                                    }
                                }
                                if (!Alive(x_pos, y_pos))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Du tabte.");
                                    Console.WriteLine("Din score blev: ");
                                    break;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                NED(x_pos, y_pos);
                                TEGN(x_pos, y_pos);
                                Thread.Sleep(100);
                            }
                            break;
                        case ConsoleKey.Q:
                            return;
                    }
                }
            }
        } while (true);
    }
}
