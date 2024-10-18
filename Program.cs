using System.Runtime.InteropServices;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            int tokens = 10;
            int slotEarnings = 0;
            bool mainRun = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the CatCade! =^.^=\nSelect an option:\n1. Slots\n2. Hide'n'Seek\n3. View Token Balance\n4. Exit");
                int input = GetIntParse(Console.ReadLine(), 1, 4);

                switch (input)
                {
                    case 1:
                        RunSlots(ref slotEarnings, ref tokens);
                        break;
                    case 2:
                        break;
                    case 3:
                        ViewTokenBalance(tokens);
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        mainRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option.");
                        Pause();
                        break;
                }
            } while(mainRun);
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
                if ((output < min) || (output > max))
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

                if (Console.IsOutputRedirected == false)
                {
                    Console.Clear();
                }

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

        static void RunSlots(ref int slotEarnings, ref int tokens)
        {
            bool slotsRun = true;
            const int maxWager = 2;

            do
            {
                Console.Clear();
                Console.WriteLine(">->->->-> Cat Slots <-<-<-<-<");
                
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

        static void HideNSeek()
        {
            string[] hidingSpots = { "balcony", "bathtub", "closet", "couch", "fridge", "plant", "sink", "lLlaasd" };
        }

        static void ViewTokenBalance(int tokens)
        {
            Console.Clear();
            Console.WriteLine($"Token balance: {tokens}");
            Pause();
        }
    }
}