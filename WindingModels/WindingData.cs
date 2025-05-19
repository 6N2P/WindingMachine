using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindingModels
{
    public class WindingData
    {
        /// <summary>
        /// Слой
        /// </summary>
        public int Laer {  get; set; }
        /// <summary>
        /// Диаметр проволки
        /// </summary>
        public double WireDiameter { get; set; }
        /// <summary>
        /// Количество витков
        /// </summary>
        public int NumberOfTurns { get; set; }
    }
}
