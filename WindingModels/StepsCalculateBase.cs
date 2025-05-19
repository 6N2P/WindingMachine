using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public abstract class StepsCalculateBase
    {
        public StepsCalculateBase(double wireDiameter, int stepsMotor, double innerDiametr)
        {
            WireDiameter = wireDiameter;
            StepsMotor = stepsMotor;
            InnerDiametr = innerDiametr;
        }

        public abstract int CalculateSteps();
        public double WireDiameter { get; set; }
        public int StepsMotor {  get; set; }
        public double InnerDiametr { get; set; }
    }
}
