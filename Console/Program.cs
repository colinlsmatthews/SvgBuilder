using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SvgBuilder.Core;

namespace SvgBuilder.CLI;
class Program
{
    static void Main(string[] args)
    {
        // Establish program variables
        string? inputPath = String.Empty;
        string? outputPath = String.Empty;
        string outputDestination = String.Empty;
        Exception e = new Exception();

        // Display welcome message and prompt for input
        Console.WriteLine("Welcome to SVG Builder!");
        Console.WriteLine("Please enter the path to the JSON specification for your SVG:");
        inputPath = GetInput();
        Console.WriteLine("Please enter the path to your destination directory:");
        outputPath = GetInput();
        
        // Call svg builder and display results
        Builder svgBuilder = new Builder();
        if (svgBuilder.Build(inputPath, outputPath, out outputDestination, out e))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSVG built successfully!");
            Console.WriteLine($"SVG built at {outputDestination}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nThere was an error building your SVG:");
            Console.WriteLine(e.Message + "\n");
            Console.ResetColor();
        }
    }

    private static string? GetInput()
    {
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. Please provide a valid path:");
            GetInput();
        }
        return input;
    }
}