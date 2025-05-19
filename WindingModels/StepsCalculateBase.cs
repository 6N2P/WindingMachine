using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public abstract class StepsCalculateBase
    {        
        public StepsCalculateBase(double wireDiameter, int stepsMotor)
        {
            WireDiameter = wireDiameter;
            StepsMotor = stepsMotor;
        }

        public abstract int CalculateSteps();
        public double WireDiameter { get; set; }
        public int StepsMotor {  get; set; }
    }
}
