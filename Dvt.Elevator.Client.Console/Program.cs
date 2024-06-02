//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Dvt.Elevator.Application.Abstraction;
using Dvt.Elevator.Application.UseCase.ElevatorServices;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading;

namespace Elevation
{
    public class Program
    {
        private const string QUIT = "q";
        private readonly ILogger _logger;

        public static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                IElevatoring _elevator;

            Start:
                Console.WriteLine("Welcome to awesome elevator!!");
                Console.WriteLine("How tall is the building that this elevator will be in?");

                int floor; string floorInput; IElevatoring elevator;

                floorInput = Console.ReadLine();

                if (Int32.TryParse(floorInput, out floor))
                    _elevator = new ElevatorService(floor);
                else
                {
                    Console.WriteLine(".....That' doesn't make sense...");
                    Console.Beep();
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto Start;
                }
                string input = string.Empty;

                while (input != QUIT)
                {
                    Console.WriteLine("Please press which floor you would like to go to");

                    input = Console.ReadLine();
                    if (Int32.TryParse(input, out floor))
                    {
                        Console.WriteLine("Closed");
                        _elevator.FloorMovement(floor);
                    }
                    else if (input == QUIT)
                        Console.WriteLine("GoodBye!");
                    else
                        Console.WriteLine("You have pressed an incorrect floor, Please try again");
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Example log message2");
                throw;
            }
        }
    }
}