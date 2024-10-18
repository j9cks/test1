using System.Runtime.InteropServices;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            int tokens = 10;
            int slotsEarnings = 0;
            int hideNSeekEarnings = 0;
            bool run = true;

            do
            {
                Console.Clear();

                if (tokens >= 20)
                {
                    Console.WriteLine("Congratulations! You've passed the adoption test!\nSee you next time!");
                    run = false;
                    break;
                }

                Console.WriteLine("Welcome to the CatCade! =^.^=\nSelect an option:\n1. Slots\n2. Hide'n'Seek\n3. View Token Balance\n4. Exit");
                int input = GetIntParse(Console.ReadLine(), 1, 4);

                switch (input)
                {
                    case 1:
                        if(slotsEarnings < 6)
                        {
                            RunSlots(ref tokens, ref slotsEarnings);
                        }
                        else
                        {
                            Console.WriteLine("You've earned all you can from slots!");
                            Pause();
                        }
                        break;
                    case 2:
                        if(hideNSeekEarnings < 6)
                        {
                            HideNSeek(ref tokens, ref hideNSeekEarnings);
                        }
                        else
                        {
                            Console.WriteLine("You've earned all you can from hide n' seek!");
                            Pause();
                        }
                        break;
                    case 3:
                        ViewTokenOverview(tokens, slotsEarnings, hideNSeekEarnings);
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option.");
                        Pause();
                        break;
                }
            } while(run);
        }
        
        static int GetIntParse(string input, int min, int max)
        {
            bool success = false;
            int output;
            
            do 
            {
                success = int.TryParse(input, out output);
                
                if (!success)
                {
                    Console.Write($"ERROR: {input} is invalid! Try again: ");
                    input = Console.ReadLine();
                }
                else if ((output < min) || (output > max))
                {
                    success = false;

                    Console.Write($"ERROR: {input} is invalid! Try again: ");
                    input = Console.ReadLine();
                }
            } while (!success);

            return output;
        }

        static void Pause()
        {
            Console.Write("Press [ENTER] to continue.");
            Console.ReadLine();
        }

        static bool Replay()
        {
            Console.Write("Play again? (Y/N) ");
            bool success = false;
            do
            {
                string input = Console.ReadLine();
                string inputCompare = input.Substring(0, 1).ToLower();

                if(inputCompare == "n") return false;
                else if (inputCompare == "y") return true;
                else Console.WriteLine($"ERROR: {input} is invalid! Try again: ");
            } while(!success);

            return false;
        }

        static int VerifyWager(int tokens, int maxWager)
        {
            bool success = false;
            int wager;
            int output;

            do
            {
                wager = GetIntParse(Console.ReadLine(), 1, maxWager);
                
                if(wager > tokens)
                {
                    Console.Write("Invalid wager! Please try again: ");
                }
                else success = true;
            } while(!success);

            return wager;
        }

        static int PlaySlots(int tokens, int wager)
        {
            string[] symbolBank = { "=^.^=", "TT_TT", "Hello", ";-;-;", "-_-:3", "8===D" };
            Random random = new Random();

            string[] symbols = new string[3];

            tokens -= wager;

            for (int i = 0; i < 30; i++)
            {
                if(i < 10)
                {
                    symbols[0] = symbolBank[random.Next(0, 6)];
                }

                if(i < 20)
                {
                    symbols[1] = symbolBank[random.Next(0, 6)];
                }

                symbols[2] = symbolBank[random.Next(0, 6)];

                Console.Clear();

                Console.WriteLine($"        Cat Slots\n+-----------------------+\n| {symbols[0]} | {symbols[1]} | {symbols[2]} |\n+-----------------------+");
                Thread.Sleep(50);
            }

            for(int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i] == "=^.^=")
                {
                    tokens += wager;
                }
            }

            return tokens;
        }

        static void RunSlots(ref int tokens, ref int slotEarnings)
        {
            bool slotsRun = true;
            const int maxWager = 2;

            do
            {
                Console.Clear();
                Console.WriteLine("---- Cat Slots ----");
                
                if (slotEarnings < 6)
                {
                    slotEarnings = -tokens;

                    Console.Write($"How many tokens would you like to wager? (max {maxWager}): ");
                    int wager = VerifyWager(tokens, maxWager);

                    tokens = PlaySlots(tokens, wager);
                    slotEarnings += tokens;

                    Console.WriteLine($"Your new token balance: {tokens}");

                    if (tokens > 0)
                    {
                        slotsRun = Replay();
                    }
                    else
                    {
                        Console.WriteLine("You have no more tokens to wager!");
                        slotsRun = false;
                        Pause();
                    }
                }
                else
                {
                    Console.WriteLine("You've earned your maximum from slots. Play Hide'n'Seek to complete your test!");
                    slotsRun = false;
                    Pause();
                }
            } while(slotsRun);
        }
        
        static string VerifyGuess(string[] hidingSpots, string input)
        {
            bool success = false;

            do
            {
                success = hidingSpots.Contains(input);

                if(!success)
                {
                    Console.WriteLine("Enter a valid hiding spot!");
                    input = Console.ReadLine();
                }
            } while(!success);
            
            return input;
        }

        static int GetIndexOf(string[] hidingSpots, string input)
        {
            for (int i = 0; i < hidingSpots.Length; i++)
            {
                if (hidingSpots[i] == input) return i;
            }

            return -1;
        }

        static void HideNSeek(ref int tokens, ref int totalEarnings)
        {
            string[] hidingSpots = { "balcony", "bathtub", "closet", "couch", "fridge", "plant", "sink" };
            bool run = true;

            do
            {
                Random random = new Random();
                int hidingSpot = random.Next(0, hidingSpots.Length);
                int tries = 3;
                int[] proximity = { -1, -1};
                bool won = false;

                Console.Clear();
                Console.WriteLine("---- Hide N' Seek! ----");
                Console.WriteLine($"The cat is hiding in 1 of {hidingSpots.Length} places.");

                for(int i = 0; i < hidingSpots.Length; i++)
                {
                    Console.WriteLine($"\t{hidingSpots[i]}");
                }

                Console.WriteLine("You have 3 tries to guess where the cat is hiding! Good luck!");

                while(tries > 0)
                {
                    Console.WriteLine("Enter your guess: ");
                    string input = Console.ReadLine();
                    string guess = VerifyGuess(hidingSpots, input);

                    tries--;

                    proximity[0] = proximity[1];
                    proximity[1] = GetIndexOf(hidingSpots, guess);

                    if(proximity[1] == hidingSpot)
                    {
                        won = true;
                        break;
                    }

                    if(proximity[0] == -1) continue;

                    if(Math.Abs(proximity[1] - hidingSpot) > Math.Abs(proximity[0] - hidingSpot))
                    {
                        Console.WriteLine("You're getting colder!");
                    }
                    else if (Math.Abs(proximity[1] - hidingSpot) == Math.Abs(proximity[0] - hidingSpot))
                    {
                        Console.WriteLine("Your guesses were equally as close!");
                    }
                    else
                    {
                        Console.WriteLine("You're getting warmer!");
                    }
                }

                Console.Clear();

                if(won)
                {
                    Console.WriteLine("Congratulations! You guessed it!");
                    tokens += 3;
                    totalEarnings += 3;
                }
                else if(!won)
                {
                    Console.WriteLine($"The hiding spot was {hidingSpots[hidingSpot]}.");
                    Console.WriteLine("Better luck next time! Your tokens are mine now!");
                    tokens -= totalEarnings;
                    totalEarnings = 0;
                }

                Console.WriteLine($"Total earnings: {totalEarnings}");

                if(totalEarnings >= 6)
                {
                    Console.WriteLine("You've earned all you can from hide n' seek!");
                    run = false;
                    Pause();
                }
                else
                {
                    run = Replay();
                }
            } while(run);
        }

        static void ViewTokenOverview(int tokens, int slotsEarnings, int hideNSeekEarnings)
        {
            Console.Clear();
            Console.WriteLine($"You have {tokens} tokens.");
            Console.WriteLine($"You've earned {slotsEarnings} tokens from slots.");
            Console.WriteLine($"You've earned {hideNSeekEarnings} tokens from hide n' seek.");
            Pause();
        }
    }
}