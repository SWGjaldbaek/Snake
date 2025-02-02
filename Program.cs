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
        if (x_pos[0] == 0 || y_pos[0]==0|| x_pos[x_pos.Count-1] == 100 || y_pos[y_pos.Count-1] == 100)
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

    static string retningsÆndring(string ret, ConsoleKeyInfo keyInfo)
    {
        //Intercept, springer over uanset hvad.
        if (Console.KeyAvailable)
        {
            if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                return "venstre";
            }
            if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                return "højre";
            }
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                return "op";
            }
            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                return "ned";
            }
            else
            {
                return ret;
            }
        }
        else
        {
            return ret;
        }
    }
    static void retning(List<int> x_pos, List<int> y_pos, string ret, ConsoleKeyInfo keyinfo)
    {
        //Kontrollerer om tasten er trykket og om slangens retning
        
        ret = retningsÆndring(ret, keyinfo);
        switch (ret)
        {
            case "venstre":
                VENSTRE(x_pos, y_pos);
                TEGN(x_pos, y_pos);
                break;
            case "højre":
                HØJRE(x_pos, y_pos);
                TEGN(x_pos, y_pos);
                break;
            case "ned":
                NED(x_pos, y_pos);
                TEGN(x_pos, y_pos);
                break;
            case "op":
                OP(x_pos, y_pos);
                TEGN(x_pos, y_pos);
                break;
        }
       
    }

    public static void Main()
    {
        
        List<int> x_pos = new List<int>();
        List<int> y_pos = new List<int>();


        
        string ret = "venstre";
        // Initialisering
        x_pos.Add(33);
        x_pos.Add(34);
        x_pos.Add(35);
        x_pos.Add(36);
        x_pos.Add(37);
        x_pos.Add(38);

        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);
        y_pos.Add(10);

        for (int i = 0; i < x_pos.Count; i++)
        {
            Console.SetCursorPosition(x_pos[i], y_pos[i]);
            Console.Write("X");
        }
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        //Clock
        System.Timers.Timer aTimer = new System.Timers.Timer();
        aTimer.Elapsed += (source, e) => OnTimedEvent(source, e, x_pos,y_pos,ret, keyInfo);
        aTimer.Interval = 200; // 
        aTimer.Enabled = true;

        
        while (Console.Read() != 'q') ;
    }
    private static void OnTimedEvent(object source, ElapsedEventArgs e, List<int> x_pos, List<int> y_pos, string ret, ConsoleKeyInfo keyInfo)
    {
        //Spillets løkke
        
        do {        
            //Thread.Sleep(200);
            retning(x_pos, y_pos, ret, keyInfo);
            
            if (!Alive(x_pos, y_pos))
            {
                Console.Clear();
                Console.WriteLine("Du tabte.");
                Console.WriteLine("Din score blev: ");
                return;
            }

            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                return;
            }
        } while (true);
    }
}