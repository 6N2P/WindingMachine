using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public class StepsCalculateTor : StepsCalculateBase
    {
        public StepsCalculateTor(double wireDiameter, int stepsMotor, double innerDiametr) : base(wireDiameter, stepsMotor, innerDiametr)
        {
        }

        public override int CalculateSteps()
        {
            throw new NotImplementedException();
        }
    }
}
