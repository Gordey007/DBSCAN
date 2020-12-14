using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    class Elements
    {
        public const int noise = -1, classfied = 0;
        public int clusterNumber;
        public List<double> elements;

        public Elements(List<double> elements)
        {
            this.elements = elements;
        }

        public static double distance(Elements element1, Elements element2)
        {
            double distance = 0;

            for (int i = 0; i < element1.elements.Count; i++)
            {
                distance += Math.Pow((element1.elements[i] - element2.elements[i]), 2);
            }

            distance = Math.Sqrt(distance);

            return distance;
        }

        public override string ToString()
        {
            string parameters = "";

            for (int i = 0; i < elements.Count; i++)
            {
                parameters += " " + elements[i];
            }
            return parameters;
        }
    }
}
