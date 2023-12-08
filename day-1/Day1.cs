using System.Text.RegularExpressions;

static class Day1
{
    public static int Challenge1()
    {
        string[] inputLines = File.ReadAllLines("day-1/day-1-input.txt");

        int total = 0;

        for (int i = 0; i < inputLines.Length; i++)
        {
            string[] split = Regex.Split(inputLines[i], @"\D+").Where(text => !string.IsNullOrEmpty(text)).ToArray();

            if(split.Length == 0)
            {
                total += 0;
                continue;
            }
            
            if(int.TryParse(split[0][0].ToString() + split[^1][^1].ToString(), out int result))
            {
                total += result;
            }
        }
        
        return total;
    }

    public static int Challenge2()
    {
        int total = 0;



        return total;
    }
}