using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerWithSerialPort
{
    public abstract class SPWorkerBase
    {
        public SerialPort SerialPort {  get; set; }

        public SPWorkerBase(SerialPort serialPort)
        {
            SerialPort = serialPort;
        }
    }
}
