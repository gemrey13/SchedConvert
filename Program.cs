using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. CPU Scheduling");
            Console.WriteLine("2. Number System");
            Console.WriteLine("3. Exit");

            Console.Write("\nSelect an option (1-3): ");
            string mainChoice = Console.ReadLine() ?? string.Empty;

            switch (mainChoice)
            {
                case "1":
                    CPUSchedulingMenu();
                    break;
                case "2":
                    NumberSystemMenu();
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }

    static void CPUSchedulingMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("CPU Scheduling");
            Console.WriteLine("1. FCFS (First Come First Serve)");
            Console.WriteLine("2. SJN (Shortest Job Next)");
            Console.WriteLine("3. Round Robin");
            Console.WriteLine("4. Priority Scheduling");
            Console.WriteLine("5. Multilevel Scheduling");
            Console.WriteLine("6. Exit");

            Console.Write("\nSelect an option (1-6): ");
            string cpuChoice = Console.ReadLine() ?? string.Empty;

            switch (cpuChoice)
            {
                case "1":
                    FCFS();
                    break;
                case "2":
                    SJN();
                    break;
                case "3":
                    RoundRobin();
                    break;
                case "4":
                    PriorityScheduling();
                    break;
                case "5":
                    MultilevelScheduling();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to return to the CPU Scheduling menu...");
            Console.ReadKey();
        }
    }

    static void FCFS()
    {
        Console.WriteLine("You selected FCFS (First Come First Serve) scheduling.");
        Console.Write("Enter process arrival times (comma-separated): ");
        string[] arrivalTimes = (Console.ReadLine() ?? string.Empty).Split(',');
        Console.Write("Enter process burst times (comma-separated): ");
        string[] burstTimesInput = (Console.ReadLine() ?? string.Empty).Split(',');
        int[] burst = Array.ConvertAll(burstTimesInput, s => int.TryParse(s, out int val) ? val : 0);

        int n = arrivalTimes.Length;
        int[] waitingTime = new int[n];
        int[] turnAroundTime = new int[n];

        waitingTime[0] = 0;
        for (int i = 1; i < n; i++)
        {
            waitingTime[i] = waitingTime[i - 1] + burst[i - 1];
        }

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = waitingTime[i] + burst[i];
        }

        Console.WriteLine("Process\tWaiting Time\tTurnaround Time");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{waitingTime[i]}\t\t{turnAroundTime[i]}");
        }
    }

    static void SJN()
    {
        Console.WriteLine("You selected SJN (Shortest Job Next) scheduling.");
        Console.Write("Enter process burst times (comma-separated): ");
        string[] burstTimesInput = (Console.ReadLine() ?? string.Empty).Split(',');
        int[] burst = Array.ConvertAll(burstTimesInput, s => int.TryParse(s, out int val) ? val : 0);

        int n = burst.Length;
        Array.Sort(burst);

        int[] waitingTime = new int[n];
        int[] turnAroundTime = new int[n];

        waitingTime[0] = 0;
        for (int i = 1; i < n; i++)
        {
            waitingTime[i] = waitingTime[i - 1] + burst[i - 1];
        }

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = waitingTime[i] + burst[i];
        }

        Console.WriteLine("Process\tWaiting Time\tTurnaround Time");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{waitingTime[i]}\t\t{turnAroundTime[i]}");
        }
    }

    static void RoundRobin()
    {
        Console.WriteLine("You selected Round Robin scheduling.");
        Console.Write("Enter process burst times (comma-separated): ");
        string[] burstTimesInput = (Console.ReadLine() ?? string.Empty).Split(',');
        int[] burst = Array.ConvertAll(burstTimesInput, s => int.TryParse(s, out int val) ? val : 0);
        Console.Write("Enter time quantum: ");
        if (int.TryParse(Console.ReadLine(), out int quantum))
        {
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        int n = burst.Length;
        int[] remainingBurst = (int[])burst.Clone();
        int[] waitingTime = new int[n];
        int[] turnAroundTime = new int[n];

        int time = 0;
        bool done;

        do
        {
            done = true;
            for (int i = 0; i < n; i++)
            {
                if (remainingBurst[i] > 0)
                {
                    done = false;
                    if (remainingBurst[i] > quantum)
                    {
                        time += quantum;
                        remainingBurst[i] -= quantum;
                    }
                    else
                    {
                        time += remainingBurst[i];
                        waitingTime[i] = time - burst[i];
                        remainingBurst[i] = 0;
                    }
                }
            }
        } while (!done);

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = waitingTime[i] + burst[i];
        }

        Console.WriteLine("Process\tWaiting Time\tTurnaround Time");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{waitingTime[i]}\t\t{turnAroundTime[i]}");
        }
    }

    static void PriorityScheduling()
    {
        Console.WriteLine("You selected Priority Scheduling.");
        Console.Write("Enter process burst times (comma-separated): ");
        string[] burstTimesInput = (Console.ReadLine() ?? string.Empty).Split(',');
        int[] burst = Array.ConvertAll(burstTimesInput, s => int.TryParse(s, out int val) ? val : 0);
        Console.Write("Enter process priorities (comma-separated): ");
        string[] priorities = (Console.ReadLine() ?? string.Empty).Split(',');

        int n = burst.Length;
        int[] priority = Array.ConvertAll(priorities, int.Parse);
        int[] waitingTime = new int[n];
        int[] turnAroundTime = new int[n];

        Array.Sort(priority, burst);

        waitingTime[0] = 0;
        for (int i = 1; i < n; i++)
        {
            waitingTime[i] = waitingTime[i - 1] + burst[i - 1];
        }

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = waitingTime[i] + burst[i];
        }

        Console.WriteLine("Process\tPriority\tWaiting Time\tTurnaround Time");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{priority[i]}\t\t{waitingTime[i]}\t\t{turnAroundTime[i]}");
        }
    }

    static void MultilevelScheduling()
    {
        Console.WriteLine("You selected Multilevel Scheduling.");
        Console.Write("Enter burst times for high-priority processes (comma-separated): ");
        string[] highPriorityInput = (Console.ReadLine() ?? string.Empty).Split(',');
        int[] highPriorityBurst = Array.ConvertAll(highPriorityInput, s => int.TryParse(s, out int val) ? val : 0);
        Console.Write("Enter burst times for low-priority processes (comma-separated): ");
        string[] lowPriorityInput = (Console.ReadLine() ?? string.Empty).Split(',');
        int[] lowPriorityBurst = Array.ConvertAll(lowPriorityInput, s => int.TryParse(s, out int val) ? val : 0);

        Console.WriteLine("\nExecuting High-Priority Queue (FCFS):");
        ExecuteFCFS(highPriorityBurst);

        Console.WriteLine("\nExecuting Low-Priority Queue (FCFS):");
        ExecuteFCFS(lowPriorityBurst);
    }

    static void ExecuteFCFS(int[] burst)
    {
        int n = burst.Length;
        int[] waitingTime = new int[n];
        int[] turnAroundTime = new int[n];

        waitingTime[0] = 0;
        for (int i = 1; i < n; i++)
        {
            waitingTime[i] = waitingTime[i - 1] + burst[i - 1];
        }

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = waitingTime[i] + burst[i];
        }

        Console.WriteLine("Process\tBurst Time\tWaiting Time\tTurnaround Time");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{burst[i]}\t\t{waitingTime[i]}\t\t{turnAroundTime[i]}");
        }
    }

    static void ConvertNumberSystem(string system)
    {
        Console.Write($"Enter a number to convert to {system}: ");
        string input = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(input, out int number))
        {
            switch (system)
            {
                case "Decimal":
                    Console.WriteLine($"Decimal: {number}");
                    break;
                case "Binary":
                    Console.WriteLine($"Binary: {Convert.ToString(number, 2)}");
                    break;
                case "Hexadecimal":
                    Console.WriteLine($"Hexadecimal: {Convert.ToString(number, 16).ToUpper()}");
                    break;
                case "Octal":
                    Console.WriteLine($"Octal: {Convert.ToString(number, 8)}");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid number. Please try again.");
        }
    }

    static void NumberSystemMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Number System");
            Console.WriteLine("1. Convert to Decimal");
            Console.WriteLine("2. Convert to Binary");
            Console.WriteLine("3. Convert to Hexadecimal");
            Console.WriteLine("4. Convert to Octal");
            Console.WriteLine("5. Exit");

            Console.Write("\nSelect an option (1-5): ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    ConvertNumberSystem("Decimal");
                    break;
                case "2":
                    ConvertNumberSystem("Binary");
                    break;
                case "3":
                    ConvertNumberSystem("Hexadecimal");
                    break;
                case "4":
                    ConvertNumberSystem("Octal");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to return to the Number System menu...");
            Console.ReadKey();
        }
    }
}