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
        Console.WriteLine("Executing FCFS Scheduling...");

        int[] arrivalTimes = { 0, 2, 4, 6, 8 };
        int[] burstTimes = { 5, 3, 6, 4, 2 };

        int n = arrivalTimes.Length;
        int[] completionTime = new int[n];
        int[] turnAroundTime = new int[n];
        int[] waitingTime = new int[n];

        completionTime[0] = arrivalTimes[0] + burstTimes[0];
        for (int i = 1; i < n; i++)
        {
            if (completionTime[i - 1] < arrivalTimes[i])
            {
                completionTime[i] = arrivalTimes[i] + burstTimes[i];
            }
            else
            {
                completionTime[i] = completionTime[i - 1] + burstTimes[i];
            }
        }

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = completionTime[i] - arrivalTimes[i];
            waitingTime[i] = turnAroundTime[i] - burstTimes[i];
        }

        Console.WriteLine("Process\tAT\tBT\tCT\tTAT\tWT");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{arrivalTimes[i]}\t{burstTimes[i]}\t{completionTime[i]}\t{turnAroundTime[i]}\t{waitingTime[i]}");
        }

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    static void SJN()
    {
        Console.WriteLine("Executing SJN Scheduling...");

        int[] arrivalTimes = { 0, 2, 4, 6, 8 };
        int[] burstTimes = { 5, 3, 6, 4, 2 };

        int n = arrivalTimes.Length;
        int[] completionTime = new int[n];
        int[] turnAroundTime = new int[n];
        int[] waitingTime = new int[n];
        bool[] isCompleted = new bool[n];

        int currentTime = 0, completed = 0;

        while (completed < n)
        {
            int shortestIndex = -1;
            int shortestBurst = int.MaxValue;

            for (int i = 0; i < n; i++)
            {
                if (!isCompleted[i] && arrivalTimes[i] <= currentTime && burstTimes[i] < shortestBurst)
                {
                    shortestBurst = burstTimes[i];
                    shortestIndex = i;
                }
            }

            if (shortestIndex != -1)
            {
                currentTime += burstTimes[shortestIndex];
                completionTime[shortestIndex] = currentTime;
                turnAroundTime[shortestIndex] = completionTime[shortestIndex] - arrivalTimes[shortestIndex];
                waitingTime[shortestIndex] = turnAroundTime[shortestIndex] - burstTimes[shortestIndex];
                isCompleted[shortestIndex] = true;
                completed++;
            }
            else
            {
                currentTime++;
            }
        }

        Console.WriteLine("Process\tAT\tBT\tCT\tTAT\tWT");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{i + 1}\t{arrivalTimes[i]}\t{burstTimes[i]}\t{completionTime[i]}\t{turnAroundTime[i]}\t{waitingTime[i]}");
        }

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    static void RoundRobin()
    {
        Console.WriteLine("Executing Round Robin Scheduling...");

        int[] burstTimes = { 5, 3, 8, 6, 2 };
        int quantum = 2; // Time quantum

        int n = burstTimes.Length;
        int[] remainingBurst = (int[])burstTimes.Clone();
        int[] completionTime = new int[n];
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
                        Console.WriteLine($"Process {i + 1} executed for {quantum} units.");
                    }
                    else
                    {
                        time += remainingBurst[i];
                        Console.WriteLine($"Process {i + 1} executed for {remainingBurst[i]} units.");
                        remainingBurst[i] = 0;
                        completionTime[i] = time;
                        Console.WriteLine($"Process {i + 1} completed at time {time}.");
                    }
                }
            }
        } while (!done);

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    static void PriorityScheduling()
    {
        Console.WriteLine("Executing Priority Scheduling...");

        int[] burstTimes = { 5, 3, 6, 4 };
        int[] priorities = { 3, 1, 4, 2 };

        int n = burstTimes.Length;
        int[] completionTime = new int[n];
        int[] turnAroundTime = new int[n];
        int[] waitingTime = new int[n];
        int[] processIds = new int[n];

        for (int i = 0; i < n; i++)
        {
            processIds[i] = i + 1;
        }

        Array.Sort(priorities, processIds);
        Array.Sort(priorities, burstTimes);

        completionTime[0] = burstTimes[0];
        for (int i = 1; i < n; i++)
        {
            completionTime[i] = completionTime[i - 1] + burstTimes[i];
        }

        for (int i = 0; i < n; i++)
        {
            turnAroundTime[i] = completionTime[i];
            waitingTime[i] = turnAroundTime[i] - burstTimes[i];
        }

        Console.WriteLine("Process\tPriority\tBT\tCT\tTAT\tWT");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{processIds[i]}\t{priorities[i]}\t\t{burstTimes[i]}\t{completionTime[i]}\t{turnAroundTime[i]}\t{waitingTime[i]}");
        }

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    static void MultilevelScheduling()
    {
        Console.WriteLine("Executing Multilevel Queue Scheduling...");
        Console.WriteLine("Splitting processes into High-Priority and Low-Priority queues...");

        int[] highPriorityBurst = { 3, 2 }; 
        int[] lowPriorityBurst = { 4, 6 };  

        Console.WriteLine("\nExecuting High-Priority queue (FCFS)...");
        ExecuteFCFSWithDetails(highPriorityBurst);

        Console.WriteLine("\nExecuting Low-Priority queue (SJN)...");
        ExecuteSJNWithDetails(lowPriorityBurst);

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    static void ExecuteFCFSWithDetails(int[] burst)
    {
        int n = burst.Length;
        int time = 0;

        for (int i = 0; i < n; i++)
        {
            time += burst[i];
            Console.WriteLine($"Process {i + 1} executed for {burst[i]} units.");
        }
    }

    static void ExecuteSJNWithDetails(int[] burst)
    {
        int n = burst.Length;
        int[] sortedBurst = (int[])burst.Clone();
        Array.Sort(sortedBurst); // Sort burst times for SJN
        int time = 0;

        for (int i = 0; i < n; i++)
        {
            time += sortedBurst[i];
            Console.WriteLine($"Process {i + 1} executed for {sortedBurst[i]} units.");
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