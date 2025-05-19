using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public class StepsCalculateTor : StepsCalculateBase
    {
        public StepsCalculateTor(double wireDiameter, int stepsMotor) : base(wireDiameter, stepsMotor)
        {
        }
      
        public double InnerDiametrTor {  get; set; }
        public override int CalculateSteps()
        {
            throw new NotImplementedException();
        }
    }
}
