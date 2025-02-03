﻿using System;
using System.Collections.Generic;
using System.Media;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

internal class Program
{
    static void TegnBane(int size)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.OutputEncoding = Encoding.UTF8;
        for (int i = 0; i < size; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.WriteLine("\U000023BA");
            Console.SetCursorPosition(i, size/4);
            Console.WriteLine("\U000023BD");
            if (i < size / 4)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine("\U000023B8");
                Console.SetCursorPosition(size, i);
                Console.WriteLine("\U000023B9");
            }
        }
        //hjørnerne
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("\U0000231C");
        Console.SetCursorPosition(0, size/4);
        Console.WriteLine("\U0000231E");
        Console.SetCursorPosition(size, size / 4);
        Console.WriteLine("\U0000231F");
        Console.SetCursorPosition(size, 0);
        Console.WriteLine("\U0000231D");

    }
    static void TEGN(List<int> x_pos, List<int> y_pos, int æble_x, int æble_y, int score)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Green;
        //Score tegnes
        Console.SetCursorPosition(1, 1);                                            // Her har jeg ændret - Mark
        Console.Write("Score: " + score);                                           // Her har jeg ændret - Mark

        //Slange tegnes
        string ball = "\U00002609";
        string head = "\U0000263B";
        for (int i = 0; i < x_pos.Count; i++)
        {
            
            if (i == 0)
            {
                Console.SetCursorPosition(x_pos[i], y_pos[i]);
                Console.Write(head);
            }
            else
            {
                Console.SetCursorPosition(x_pos[i], y_pos[i]);
                Console.Write(ball);

            }
        }

        //Æble tegnes
        
        string apple = "\U0001F34E";
        Console.SetCursorPosition(æble_x, æble_y);                                  // Her har jeg ændret - Mark
        Console.Write(apple);
        Console.ForegroundColor = ConsoleColor.White;
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
    static bool Alive(List<int> x_pos, List<int> y_pos, int size, int score)
    {
        //Kontrollere spillets bander
        if (x_pos[0] == 0 || y_pos[0] == 0 || x_pos[0] == size || y_pos[0] == size/4)
        {
            using (var soundPlayer = new SoundPlayer(@"C:\Users\victo\Downloads\smb_mariodie.wav"))
            {
                soundPlayer.Play(); // can also use soundPlayer.PlaySync()
            }
            Console.Clear();
            Console.WriteLine("Du tabte.");
            Console.WriteLine("Din score blev: "+ score);
            Console.WriteLine("Vil du spille igen? Tast r");
            string igen = Console.ReadLine();
            if (igen == "r")
            {
                Console.WriteLine("En gang til!");
            }
            
            return false;
        }
        //Kontrollere om slangen spiser sig selv
        //x_pos[0],y_pos[0] er slangens start position.x_pos.Count=y_pos.count
        for (int i = 1; i < x_pos.Count; i++)
        {       //i marker kordinatsættet vi er nået til at kontrollere. 
            if (x_pos[0] == x_pos[i] && y_pos[0] == y_pos[i])
            {    //kontrol om hovedet er på en del af slangen. 
                using (var soundPlayer = new SoundPlayer(@"C:\Users\victo\Downloads\smb_mariodie.wav"))
                {
                    soundPlayer.Play(); // can also use soundPlayer.PlaySync()
                }
                Console.Clear();
                Console.WriteLine("Du tabte.");
                Console.WriteLine("Din score blev: "+ score);
                Console.WriteLine("Vil du spille igen? Tast r");
                string igen = Console.ReadLine();
                if (igen == "r")
                {
                    Console.WriteLine("En gang til!");
                }

                return false;
            }
        }

        return true;    //Slangen er stadig i live hvis ingen dødsbetingelser er opfyldt. 

    }
    static void Spis(List<int> x_pos, List<int> y_pos, ref int æble_x, ref int æble_y, ref int score, int size, ref int hastighed)
    {
        Random rand = new Random();

        if ((x_pos[0] == æble_x && y_pos[0] == æble_y) || (x_pos[0] == æble_x + 1 && y_pos[0] == æble_y))                   // Her har jeg ændret - Mark
        {
            x_pos.Add(x_pos[x_pos.Count - 1]);                          // Her har jeg ændret - Mark
            y_pos.Add(y_pos[y_pos.Count - 1]);                          // * Forlængelse af slangen
            score = score + 100;                                        // Her har jeg ændret - Mark

            using (var soundPlayer = new SoundPlayer(@"C:\Users\victo\Downloads\smW_coin.wav"))
            {
                soundPlayer.Play(); // can also use soundPlayer.PlaySync()
            }
            hastighed = hastighed *9/10;
            //slet æble her
            Console.SetCursorPosition(æble_x, æble_y);                                  // Her har jeg ændret - Mark
            Console.Write(" ");
            æble_x = rand.Next(4, size-2);                                  // Her har jeg ændret - Mark
            æble_y = rand.Next(4, size / 4 -2);                             // Her har jeg ændret - Mark
            while (true)
            {
                for (int i = 0; i < x_pos.Count; i++)
                {
                    if ((x_pos[i] == æble_x && y_pos[i] == æble_y) || (x_pos[i] == æble_x + 1 && y_pos[i] == æble_y))
                    {
                        //slet æble her
                        Console.SetCursorPosition(æble_x, æble_y);                                  // Her har jeg ændret - Mark
                        Console.Write(" ");

                        æble_x = rand.Next(4, size - 2);                                  // Her har jeg ændret - Mark
                        æble_y = rand.Next(4, size / 4 - 2);
                                           }
                    else
                    {
                        
                        return;
                    }
                }
             
            }
            
        }
    }
    static void RemoveSnake(List<int> x_pos, List<int> y_pos)
    {
        for (int i = 0; i < x_pos.Count; i++)
        {
            Console.SetCursorPosition(x_pos[i], y_pos[i]);
            Console.Write(" ");
        }
    }
   
    private static void Main(string[] args)
    {
        //Fjerner cursor 
        Console.CursorVisible = false;

        //Definer two lister med koordinater
        List<int> x_pos = new List<int>();
        List<int> y_pos = new List<int>();
        //random funk. til æble spawn
        Random rand = new Random();

        //Bane størrelse
        int size = 80;
        int hastighed = 100;
        int æble_x = rand.Next(4, size - 2);                               
        int æble_y = rand.Next(4, size/4 - 2);                              
        int score = 0;                                            
        

        //Tegn baggrund
        TegnBane(size);
      
        // start placering
        x_pos.AddRange(new int[] { 33, 34, 35 });
        y_pos.AddRange(new int[] { 10, 10, 10 });


       
        do
        {
            //Slangen skal starte med at bevæge sig til venstre
            VENSTRE(x_pos, y_pos);
            Spis(x_pos, y_pos, ref æble_x, ref æble_y, ref score, size, ref hastighed);
            if (!Alive(x_pos, y_pos, size, score))
            {
                return;
            }
            TEGN(x_pos, y_pos, æble_x, æble_y, score);
            Thread.Sleep(hastighed);
            RemoveSnake(x_pos, y_pos);

            //Hvis en tast bliver trykket så går den ind i case loopet
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                //case loop
                while (true)
                {
                    Spis(x_pos, y_pos, ref æble_x, ref æble_y, ref score, size, ref hastighed);

                    RemoveSnake(x_pos, y_pos);
                    if (!Alive(x_pos, y_pos, size, score))
                    {
                        return;
                    }
                    //Muligheder for loopet: højre pil, venstre pil, op pil og ned pil. 
                    switch (keyInfo.Key)
                    {
                        //Hvis højre pil bliver trykket så skal den gå ind i et nyt loop
                        case ConsoleKey.RightArrow:
                            //Dette loop skal fortsætte bevægelsen
                            while (true)
                            {
                                // Tjekker om der er trykket på en tast uden at blokere løkken
                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo keyInfo1 = Console.ReadKey(intercept: true);

                                    // Hvis Q, Pil op eller Pil ned trykkes, brydes løkken
                                    if (keyInfo1.Key == ConsoleKey.Q || keyInfo1.Key == ConsoleKey.UpArrow || keyInfo1.Key == ConsoleKey.DownArrow)
                                    {
                                        keyInfo = keyInfo1;  // Opdaterer keyInfo, så switch ved hvad der skal ske bagefter
                                        break;
                                    }
                                }
                                if (!Alive(x_pos, y_pos,size,score))
                                    return;
                                                               
                                Spis(x_pos, y_pos, ref æble_x,ref æble_y, ref score, size, ref hastighed);
                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                HØJRE(x_pos, y_pos);

                                TEGN(x_pos, y_pos, æble_x, æble_y, score);
                                Thread.Sleep(hastighed);

                                RemoveSnake(x_pos, y_pos);

                            }
                            break;
                        //Hvis venstre pil bliver trykket så skal den gå ind i et nyt loop
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
                                if (!Alive(x_pos, y_pos, size, score))
                                {
                                    return;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                VENSTRE(x_pos, y_pos);
                                Spis(x_pos, y_pos, ref æble_x, ref æble_y, ref score, size, ref hastighed);
                                TEGN(x_pos, y_pos, æble_x, æble_y, score);
                                
                                Thread.Sleep(hastighed);

                                RemoveSnake(x_pos, y_pos);
                            }
                            break;
                        //Hvis op pil bliver trykket så skal den gå ind i et nyt loop
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
                                if (!Alive(x_pos, y_pos, size, score))
                                {
                                    return;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                OP(x_pos, y_pos);
                                Spis(x_pos, y_pos, ref æble_x, ref æble_y, ref score, size, ref hastighed);
                                TEGN(x_pos, y_pos, æble_x, æble_y, score);
                                
                                Thread.Sleep(2*hastighed);

                                RemoveSnake(x_pos, y_pos);
                            }
                            break;
                        //Hvis ned pil bliver trykket så skal den gå ind i et nyt loop
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
                                if (!Alive(x_pos, y_pos, size, score))
                                {
                                    return;
                                }

                                // Flytter og tegner, så længe der ikke er trykket en anden tast
                                NED(x_pos, y_pos);

                                Spis(x_pos, y_pos, ref æble_x, ref æble_y, ref score, size, ref hastighed);
                                TEGN(x_pos, y_pos, æble_x, æble_y, score);
                                Thread.Sleep(2*hastighed);
                                RemoveSnake(x_pos, y_pos);
                            }
                            break;
                        //hvis Q bliver trykket så stopper programet
                        case ConsoleKey.Q:
                            return;
                    }
                  
                    }
                }
        } while (true);
    }
}