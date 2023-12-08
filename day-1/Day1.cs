using System.Text.RegularExpressions;

static class Day1
{
    public static int Challenge1()
    {
        string[] inputLines = File.ReadAllLines("day-1/day-1-input.txt");

        int total = 0;

        foreach (string line in inputLines)
        {
            string[] split = Regex.Split(line, @"\D+").Where(text => !string.IsNullOrEmpty(text)).ToArray();

            if (split.Length == 0)
            {
                total += 0;
                continue;
            }

            if (int.TryParse(split[0][0].ToString() + split[^1][^1].ToString(), out int result))
            {
                total += result;
            }
        }

        return total;
    }

    public static int Challenge2()
    {
        string[] inputLines = File.ReadAllLines("day-1/day-1-input.txt");

        int total = 0;
        Dictionary<string, string> alphaNums = new Dictionary<string, string> {
            { "one",    "1" },
            { "two",    "2" },
            { "three",  "3" },
            { "four",   "4" },
            { "five",   "5" },
            { "six",    "6" },
            { "seven",  "7" },
            { "eight",  "8" },
            { "nine",   "9" },
        };

        foreach (string line in inputLines)
        {
            string value = "";

            int? firstPassFoundIndex = null;

            // Forwards - find as usual
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    value += line[i];
                    firstPassFoundIndex = i;
                    break;
                }

                string? numeric = "";

                if (i + 3 < line.Length && alphaNums.TryGetValue(line.Substring(i, 3), out numeric))
                {
                    value += numeric;
                    firstPassFoundIndex = i;
                    break;
                }

                if (i + 4 < line.Length && alphaNums.TryGetValue(line.Substring(i, 4), out numeric))
                {
                    value += numeric;
                    firstPassFoundIndex = i;
                    break;
                }

                if (i + 5 < line.Length && alphaNums.TryGetValue(line.Substring(i, 5), out numeric))
                {
                    value += numeric;
                    firstPassFoundIndex = i;
                    break;
                }
            }

            // Early exit - there are no numbers at all
            if(firstPassFoundIndex == null)
            {
                continue;
            }

            // Backwards - possibility of finding last number faster, worst case searches all indices
            for (int i = line.Length - 1; i >= firstPassFoundIndex; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    value += line[i];
                    break;
                }

                string? numeric;

                if (i - 2 > 0 && alphaNums.TryGetValue(line.Substring(i - 2, 3), out numeric))
                {
                    value += numeric;
                    break;
                }

                if (i - 3 > 0 && alphaNums.TryGetValue(line.Substring(i - 3, 4), out numeric))
                {
                    value += numeric;
                    break;
                }

                if (i - 4 > 0 && alphaNums.TryGetValue(line.Substring(i - 4, 5), out numeric))
                {
                    value += numeric;
                    break;
                }
            }

            if (int.TryParse(value, out int result))
            {
                total += result;
            }
        }

        return total;
    }
}