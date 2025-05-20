using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public interface ICalculateSteps
    {
        int CalculateSteps();
        int CalculateStepsByTurns(int countTurns);
    }
}
