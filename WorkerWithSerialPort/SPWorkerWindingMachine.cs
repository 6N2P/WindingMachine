using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerWithSerialPort
{
    public class SPWorkerWindingMachine : SPWorkerBase, ICNCCommands
    {
        public SPWorkerWindingMachine(SerialPort serialPort) : base(serialPort)
        {
        }

        public void MuveFreeMotor()
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                string command = "MUVE_FREE\n";
                SerialPort.Write(command);
            }
        }

        public void SetDirectionMotorLeft()
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                SerialPort.Write("F\n");
            }
        }

        public void SetDirectionMotorRight()
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                SerialPort.Write("B\n");
            }
        }

        public void SetStepFreeMuve(int stepFreeMuve)
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                string command = $"STEPS_FREE:{stepFreeMuve}\n"; // \n — конец строки для Arduino
                SerialPort.Write(command);
            }
        }

        public void SetStepWorkMuve(int stepWorkMuve)
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                string command = $"STEPS:{stepWorkMuve}\n";
                SerialPort.Write(command);
            }
        }
    }
}
