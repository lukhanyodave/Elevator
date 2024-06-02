using Dvt.Elevator.Application.Abstraction;
using Dvt.Elevator.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dvt.Elevator.Application.UseCase.ElevatorServices
{
    public class ElevatorService : IElevatoring
    {
        private readonly ILogger<ElevatorService> _logger;
        private bool[] floorReady;
        public int CurrentFloor = 1;
        private int topfloor;
        public ElevatorStatus Status = ElevatorStatus.STOPPED;

        public ElevatorService(int NumberOfFloors = 10)
        {
            floorReady = new bool[NumberOfFloors + 1];
            topfloor = NumberOfFloors;
          
        }

        public void DontMove()
        {
            Console.WriteLine("That's our current floor");
        }

        public void Down(int floor)
        {
            try
            {

                for (int i = CurrentFloor; i >= 1; i--)
                {
                    if (floorReady[i])
                        Stop(floor);
                    else
                        continue;
                }

                Status = ElevatorStatus.STOPPED;
                Console.WriteLine("Open");
                Console.WriteLine("Waiting..");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to go down");
              
            }
        }

        public void FloorMovement(int floor)
        {
            try
            {
                if (floor > topfloor)
                {
                    Console.WriteLine("We only have {0} floors", topfloor);
                    return;
                }

                floorReady[floor] = true;

                switch (Status)
                {

                    case ElevatorStatus.DOWN:
                        Down(floor);
                        break;

                    case ElevatorStatus.STOPPED:
                        if (CurrentFloor < floor)
                            Up(floor);
                        else if (CurrentFloor == floor)
                            DontMove();
                        else
                            Down(floor);
                        break;

                    case ElevatorStatus.UP:
                        Up(floor);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed  floor movement ");
            }

        }

        public void  Stop(int floor)
        {
            try
            {
                Status = ElevatorStatus.STOPPED;
                CurrentFloor = floor;
                floorReady[floor] = false;
                Console.WriteLine("Stopped at floor {0}", floor);
                Console.WriteLine("Open");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to stop ");
            }
        }

        public void Up(int floor)
        {
            try
            {
                for (int i = CurrentFloor; i <= topfloor; i++)
                {
                    if (floorReady[i])
                        Stop(floor);
                    else
                        continue;
                }

                Status = ElevatorStatus.STOPPED;
                Console.WriteLine("Open");
                Console.WriteLine("Waiting..");
            }
            catch (Exception ex )
            {

                _logger.LogError(ex, "Failed to go up  ");
            }
        }
    }
}
