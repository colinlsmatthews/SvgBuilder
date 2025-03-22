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
        inputPath = GetInput(InputType.Path);
        Console.WriteLine("Please enter the path to your destination directory:");
        outputPath = GetInput(InputType.Directory);

        //inputPath = "C:\\Users\\clsm\\source\\repos\\SvgBuilder\\Core\\input.json";
        //outputPath = "C:\\Users\\clsm\\source\\repos\\SvgBuilder\\Core\\";

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

    private static string? GetInput(InputType type)
    {
        string? input = Console.ReadLine();
        switch (type)
        {
            case InputType.Path:
                if (!Util.ValidatePath(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please provide a valid path:");
                    Console.ResetColor();
                    return GetInput(type);
                }
                break;
            case InputType.Directory:
                if (!Util.ValidateDirectory(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please provide a valid directory:");
                    Console.ResetColor();
                    return GetInput(type);
                }
                break;
            default:
                break;
        }
        return input;
    }

    private enum InputType
    {
        Path,
        Directory
    }
}