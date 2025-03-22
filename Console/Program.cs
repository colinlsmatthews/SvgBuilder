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
        int? inputWidth = null;
        int? inputHeight = null;
        Exception e = new Exception();

        // Display welcome message and prompt for file input
        Console.WriteLine("Welcome to SVG Builder!");
        Console.WriteLine("Please enter the path to the JSON specification for your SVG:");
        inputPath = GetInput(InputType.Path);
        Console.WriteLine("Please enter the path to your destination directory:");
        outputPath = GetInput(InputType.Directory);

        // Handle dimension input
        GetDimChoice(out string sizeChoice);
        if (sizeChoice == "Y")
        {
            GetInputDims(out inputWidth, out inputHeight);
        }

        //inputPath = "C:\\Users\\clsm\\source\\repos\\SvgBuilder\\Core\\input.json";
        //outputPath = "C:\\Users\\clsm\\source\\repos\\SvgBuilder\\Core\\";

        // Call svg builder and display results
        Builder svgBuilder = new Builder();
        if (svgBuilder.Build(inputPath, outputPath, inputWidth, inputHeight, out outputDestination, out e))
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

    private static void GetDimChoice(out string sizeChoice)
    {
        Console.WriteLine("Enter custom width and height? (Y/N)");
        sizeChoice = Console.ReadLine().ToUpper();
        if (sizeChoice != "Y" && sizeChoice != "N")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter 'Y' or 'N':");
            Console.ResetColor();
            GetDimChoice(out sizeChoice);
        }
    }

    private static void GetInputDims(out int? inputWidth, out int? inputHeight)
    {
        try
        {
            Console.WriteLine("Enter width (px):");
            inputWidth = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter height (px):");
            inputHeight = int.Parse(Console.ReadLine());
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please provide valid integers for width and height:");
            Console.ResetColor();
            GetInputDims(out inputWidth, out inputHeight);
        }
    }

    private enum InputType
    {
        Path,
        Directory
    }
}