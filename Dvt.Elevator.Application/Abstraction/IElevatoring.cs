using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dvt.Elevator.Application.Abstraction
{
    public interface IElevatoring
    {
        void Stop(int floor);
        void Down(int floor);
        void Up(int floor);
        void DontMove();
        void FloorMovement(int floor);
    }
}
