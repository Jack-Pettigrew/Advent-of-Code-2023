static class Day3
{
    private static char[,] inputArray;

    public static int Challenge1()
    {
        int total = 0;

        // Extract all numbers + create array
        string[] lines = File.ReadAllLines("day-3/input.txt");

        // Lines - Rows, Characters -Columns
        inputArray = new char[lines.Length, lines[0].Length];

        List<Number> numbers = new List<Number>();

        // Get All Numbers 
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                if (char.IsDigit(lines[row][col]))
                {
                    Number number = GetNumber(lines[row], row, col);
                    numbers.Add(number);
                    col += number.Length - 1;
                }
                else
                {
                    // Mask numbers - values will be '\0'
                    inputArray[row, col] = lines[row][col];
                }
            }
        }

        foreach (Number number in numbers)
        {
            if (IsNumberSymbolAdjacent(number))
            {
                Console.WriteLine(number.Value);
                total += number.Value;
            }
        }

        return total;
    }

    private static bool IsNumberSymbolAdjacent(Number number)
    {
        int searchStartCol = number.ColIndex - 1;
        int searchStartRow = number.RowIndex - 1;
        int iteration = 0;

        for (int row = searchStartRow; row <= searchStartRow + 3; row++)
        {
            iteration++;

            // Edge-case: Rows
            if (row < 0 || row > inputArray.GetLength(0) - 1)
            {
                continue;
            }

            switch (iteration)
            {
                // Above + Below Rows
                case 1:
                case 3:

                    for (int i = searchStartCol; i <= searchStartCol + number.Length + 1; i++)
                    {
                        if (i < 0 || i > inputArray.GetLength(1) - 1)
                        {
                            continue;
                        }

                        if (inputArray[row, i] != '.'  && inputArray[row,i] != '\0' && !char.IsLetterOrDigit(inputArray[row, i]))
                        {
                            return true;
                        }
                    }

                    break;

                // Same Row
                case 2:

                    // Edge-case: Columns
                    if (searchStartCol - 1 > 0)
                    {
                        if (inputArray[row, searchStartCol] != '.' && inputArray[row, searchStartCol] != '\0' && !char.IsLetterOrDigit(inputArray[row, searchStartCol]))
                        {
                            return true;
                        }
                    }

                    if (searchStartCol + number.Length + 1 < inputArray.GetLength(1) - 1)
                    {
                        if (inputArray[row, searchStartCol + number.Length + 1] != '.' && inputArray[row, searchStartCol + number.Length + 1] != '\0' && !char.IsLetterOrDigit(inputArray[row, searchStartCol + number.Length + 1]))
                        {
                            return true;
                        }
                    }

                    break;
            }


        }

        return false;
    }

    private static Number GetNumber(string line, int rowIndex, int startingIndex)
    {
        string stringifiedNumber = "0";
        int currentCharIndex = startingIndex;

        do
        {
            stringifiedNumber += line[currentCharIndex];
            currentCharIndex++;
        }
        while (currentCharIndex < line.Length && char.IsDigit(line[currentCharIndex]));

        return new Number(int.Parse(stringifiedNumber), rowIndex, startingIndex);
    }

    public class Number
    {
        public int Length { private set; get; }
        public int Value { private set; get; }
        public int RowIndex { private set; get; }
        public int ColIndex { private set; get; }

        public Number(int number, int startingRowIndex, int startingColIndex)
        {
            Value = number;
            Length = number.ToString().Length;
            ColIndex = startingColIndex;
            RowIndex = startingRowIndex;
        }
    }
}
