using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public class StepsCalculateTor : ICalculateSteps
    {
        public StepsCalculateTor(double wireDiameter, int stepsMotor, double innerDiametr,
            double outerDiametrTor, double diameterOfDriveRoller)
        {
            WireDiameter = wireDiameter;
            StepsMotor = stepsMotor;
            InnerDiametr = innerDiametr;
            OuterDiametrTor = outerDiametrTor;
            DiameterOfDriveRoller = diameterOfDriveRoller;
        }

        /// <summary>
        /// Диаметр проволки
        /// </summary>
        public double WireDiameter { get; set; }

        /// <summary>
        /// Кол-во шагов одного круга шагового двигателя
        /// </summary>
        public int StepsMotor { get; set; }

        /// <summary>
        /// Внутрений диаметр тора
        /// </summary>
        public double InnerDiametr { get; set; }

        private double OuterDiametrTor { get; set; }
        private double DiameterOfDriveRoller { get; set; }

        public  int CalculateSteps()
        {
            if (InnerDiametr == 0 || WireDiameter == 0 || StepsMotor == 0 || OuterDiametrTor == 0 || DiameterOfDriveRoller ==0) return 0;
            //Вычисляю длину дуги
            var L = Math.PI * InnerDiametr;

            //Передаточное отношение
            var peredatOtn = OuterDiametrTor / DiameterOfDriveRoller;

            //Сколько витков помещается в окружности
            var n = Math.Round(L / WireDiameter, 1);
            //Округляю до цулого числа

            //Сопостовляю круг мотора с кругом витков
            //Нахожу сколько шагов один виток
            //double d = StepsMotor  / n;
            double d = StepsMotor * peredatOtn / n;
            var result = Convert.ToInt32(Math.Round(d, 0));
            return result;

        }

        public int CalculateStepsByTurns(int countTurns)
        {
            if(StepsMotor == 0 || countTurns == 0) return 0;

            //Передаточное отношение
            var peredatOtn = OuterDiametrTor / DiameterOfDriveRoller;
            double d = StepsMotor * peredatOtn / countTurns;

            var result = Convert.ToInt32(Math.Round(d, 0));
            return result;
        }
    }
}
