using System;
using System.IO;
using System.Text;

namespace MazeSolver
{
    internal class Program
    {
        private static readonly int[][] Moves = {new[] {-1, 0}, new[] {0, 1}, new[] {1, 0}, new[] {0, -1}};

        private static void Main()
        {
            Console.WriteLine("Enter file route: "); // Prompt
            var fileRoute = Console.ReadLine(); // Get file route and file name from user

            if (fileRoute == null) return; // If there's not such file

            var sr = new StreamReader(fileRoute); //Pass the file path and file name to the StreamReader constructor
            var line = sr.ReadLine(); // Load the line that contains the width and the height
            var splittedLine = line.Split(' '); // Split the line for future use of those elements
            var width = int.Parse(splittedLine[0]); //The width is always the first element of this line
            var height = int.Parse(splittedLine[1]); // The height is always the second element of this line

            line = sr.ReadLine(); // Load the line that contains the starting coordinates
            splittedLine = line.Split(' '); // Split the line for future use of those elements
            var startX = int.Parse(splittedLine[1]); //The X coordinate is always the second element of this line
            var startY = int.Parse(splittedLine[0]); //The Y coordinate is always the first element of this line

            line = sr.ReadLine(); // Load the line that contains the ending coordinates
            splittedLine = line.Split(' '); // Split the line for future use of those elements
            var endX = int.Parse(splittedLine[1]); //The X coordinate is always the second element of this line
            var endY = int.Parse(splittedLine[0]); //The Y coordinate is always the first element of this line

            var mazeArrayInt = new int[height, width]; // Initialize the numeric array for future processing
            var mazeArrayString = new string[height, width]; // Initialize the string array to show the path to the user

            for (var row = 0; row < height; row++)
            {
                line = sr.ReadLine(); // Loads the line that contains the maze information
                splittedLine = line.Split(' '); // Split the line for future use of those elements

                for (var column = 0; column < width; column++) // This is done to analyze every element on this line
                {
                    mazeArrayInt[row, column] = int.Parse(splittedLine[column]); // Parse every digit to the array

                    if (mazeArrayInt[row, column] == 1) // This denotes a wall
                        mazeArrayString[row, column] = "#"; // Puts the information on the string array for later use
                    else // This denotes a fre space
                        mazeArrayString[row, column] = " "; // Puts the information on the string array for later use
                }
            }

            sr.Close(); // Close the file

            mazeArrayInt[endX, endY] = height * width; // Set a value to the end point

            var limit = 0; // This is the variable that will be used as a condition to stop the while loop
            int xCoord; // This variable will be used for the x axis
            int yCoord; // This variable will be used for the y axis

            while (mazeArrayInt[startX, startY] == 0 && limit < height * width
            ) // This loop is used to put a value in every coordinate of the maze from the ending point
            {
                var counter = -1; // This is used as an ID of how many elements I've read
                foreach (var element in mazeArrayInt) // Try all possible moves from this square.
                {
                    counter++; // Update the number of cells that I've checked

                    if (element <= 1) continue; // Check if this cell was used before as a possible solution

                    xCoord = counter / width; // Find the X coordinate of the position that I'm currently in
                    yCoord = counter % width; // Find the Y coordinate of the position that I'm currently in

                    foreach (var coordinates in Moves) // Possible moves
                    {
                        var newRow = xCoord + coordinates[0]; // To change the x-axis to check
                        var newColumn = yCoord + coordinates[1]; // To change the y-axis to check

                        if (newRow < 0) newRow = height - 1; // To allow the warping in the x-axis
                        else if (newRow >= height) newRow = 0;

                        if (newColumn < 0) newColumn = width - 1; // To allow the warping in the y-axis
                        else if (newColumn >= width) newColumn = 0;

                        if (mazeArrayInt[newRow, newColumn] > 0)
                            continue; // Not changing the previous value or if it is a wall

                        mazeArrayInt[newRow, newColumn] =
                            mazeArrayInt[xCoord, yCoord] - 1; // Update the value of the cell
                    }
                }

                limit++; // Update amount of retries that the algorithm has tried to find a solution
            }

            if (limit == height * width) // It shouldn't take this long to find a path because it's the number of cells on the grid
            {
                Console.WriteLine("Couldn't find the starting point while resolving"); // Showing the error
                Console.ReadLine();
                return;
            }

            xCoord = startX; // Updating the values with the staring point
            yCoord = startY;

            while (xCoord != endX || yCoord != endY) // Have to re evaluate the whole grid
                foreach (var coordinates in Moves) // Try all possible moves from this square.
                {
                    var newRow = xCoord + coordinates[0]; // To change the x-axis to check
                    var newColumn = yCoord + coordinates[1]; // To change the y-axis to check

                    if (newRow < 0) newRow = height - 1; // To allow the warping in the x-axis
                    else if (newRow >= height) newRow = 0;

                    if (newColumn < 0) newColumn = width - 1; // To allow the warping in the y-axis
                    else if (newColumn >= width) newColumn = 0;

                    if (mazeArrayInt[newRow, newColumn] != mazeArrayInt[xCoord, yCoord] + 1) continue; // If this cell is not the one that we should follow, continue

                    mazeArrayString[newRow, newColumn] = "X"; // Mark the cell as part of the path to follow
                    xCoord = newRow; // Update the coordinate to check next
                    yCoord = newColumn;

                    break;
                }

            mazeArrayString[startX, startY] = "S"; // Set the starting point
            mazeArrayString[endX, endY] = "E";

            for (var i = 0; i < height; i++) // Used to show the result
            {
                var lineBuilder = new StringBuilder();

                for (var j = 0; j < width; j++) lineBuilder.Append(mazeArrayString[i, j]);

                Console.WriteLine(lineBuilder.ToString());
            }

            Console.ReadLine();
        }
    }
}