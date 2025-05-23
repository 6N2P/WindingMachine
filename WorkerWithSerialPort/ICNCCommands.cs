using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerWithSerialPort
{
    public interface ICNCCommands
    {
        void SetStepFreeMuve(int stepFreeMuve);
        void SetStepWorkMuve(int stepWorkMuve);
        void MuveFreeMotor();
        void SetDirectionMotorRight();
        void SetDirectionMotorLeft();
    }
}
