static class Day2
{
    private static Dictionary<string, int> diceAmounts;

    private static void InitDiceAmounts()
    {
        diceAmounts = new Dictionary<string, int>();
        
        Console.WriteLine("How many Red dice? ");
        diceAmounts.Add("red", int.Parse(Console.ReadLine()));
        
        Console.WriteLine("How many Green dice? ");
        diceAmounts.Add("green", int.Parse(Console.ReadLine()));

        Console.WriteLine("How many Blue dice? ");
        diceAmounts.Add("blue", int.Parse(Console.ReadLine()));
    }
    
    public static int Challenge1()
    {
        InitDiceAmounts();

        string[] inputLines = File.ReadAllLines("day-2/day-2-input.txt");
        Dictionary<int, bool> gameResults = new Dictionary<int, bool>();

        foreach (string line in inputLines)
        {
            string[] game = line.Split(':');

            int gameID = int.Parse(game[0].Split(' ')[1]);

            bool isPossible = true;

            foreach (string round in game[1].Split(';'))
            {
                string[] diceResults = round.Split(',', ' ').Where(item => !string.IsNullOrEmpty(item)).ToArray();

                for (int i = 0; i < diceResults.Length; i += 2)
                {
                    if(diceAmounts[diceResults[i + 1]] < int.Parse(diceResults[i]))
                    {
                        isPossible = false;
                        break;
                    }
                }

                if(!isPossible)
                {
                    break;
                }
            }

            gameResults.Add(gameID, isPossible);
        }

        return gameResults.Where(pair => pair.Value == true).Sum(pair => pair.Key);
    }

    public static int Challenge2()
    {
        InitDiceAmounts();
        
        string[] inputLines = File.ReadAllLines("day-2/day-2-input.txt");
        Dictionary<int, (int red, int green, int blue)> fewestRequiredEachGame = new Dictionary<int, (int, int, int)>();

        foreach (string line in inputLines)
        {
            string[] game = line.Split(':');

            int gameID = int.Parse(game[0].Split(' ')[1]);

            int red = int.MinValue;
            int green = int.MinValue;
            int blue = int.MinValue;

            foreach (string round in game[1].Split(';'))
            {
                string[] diceResults = round.Split(',', ' ').Where(item => !string.IsNullOrEmpty(item)).ToArray();

                for (int i = 0; i < diceResults.Length; i += 2)
                {
                    int diceValue = int.Parse(diceResults[i]);
                    
                    if(diceResults[i + 1] == "red" && diceValue > red)
                    {
                        red = diceValue;
                    }

                    if(diceResults[i + 1] == "green" && diceValue > green)
                    {
                        green = diceValue;
                    }

                    if(diceResults[i + 1] == "blue" && diceValue > blue)
                    {
                        blue = diceValue;
                    }
                }
            }

            fewestRequiredEachGame.Add(gameID, (red:red, green: green, blue: blue));
        }

        return fewestRequiredEachGame.Sum(pair => pair.Value.red * pair.Value.green * pair.Value.blue);
    }
}