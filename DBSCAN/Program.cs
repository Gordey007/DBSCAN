using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    class Program
    {
        static void Main()
        {
            List<List<double>> data = new List<List<double>>();
            data.Add(new List<double> { });
            data[0].Add(0);
            data[0].Add(100);
            data[0].Add(100);
            data.Add(new List<double> { });
            data[1].Add(0);
            data[1].Add(200);
            data[1].Add(200);
            data.Add(new List<double> { });
            data[2].Add(0);
            data[2].Add(275);
            data[2].Add(250);
            data.Add(new List<double> { });
            data[3].Add(0);
            data[3].Add(150);
            data[3].Add(125);
            data.Add(new List<double> { });
            data[4].Add(200);
            data[4].Add(100);
            data[4].Add(120);
            data.Add(new List<double> { });
            data[5].Add(250);
            data[5].Add(200);
            data[5].Add(150);
            data.Add(new List<double> { });
            data[6].Add(100);
            data[6].Add(200);
            data[6].Add(175);
            data.Add(new List<double> { });
            data[7].Add(650);
            data[7].Add(700);
            data[7].Add(700);
            data.Add(new List<double> { });
            data[8].Add(657);
            data[8].Add(700);
            data[8].Add(800);
            data.Add(new List<double> { });
            data[9].Add(0);
            data[9].Add(300);
            data[9].Add(150);
            data.Add(new List<double> { });
            data[10].Add(0);
            data[10].Add(100);
            data[10].Add(100);
            data.Add(new List<double> { });
            data[11].Add(675);
            data[11].Add(710);
            data[11].Add(500);
            data.Add(new List<double> { });
            data[12].Add(675);
            data[12].Add(720);
            data[12].Add(870);
            data.Add(new List<double> { });
            data[13].Add(50);
            data[13].Add(400);
            data[13].Add(100);
            data.Add(new List<double> { });
            data[14].Add(5000);
            data[14].Add(4000);
            data[14].Add(1000);
            data.Add(new List<double> { });
            data[15].Add(5000);
            data[15].Add(4000);
            data[15].Add(1000);
            data.Add(new List<double> { });
            data[16].Add(5000);
            data[16].Add(4000);
            data[16].Add(1000);
            data.Add(new List<double> { });
            data[17].Add(500);
            data[17].Add(4000);
            data[17].Add(3000);
            data.Add(new List<double> { });
            data[18].Add(500);
            data[18].Add(100);
            data[18].Add(2500);

            data.Add(new List<double> { });
            data[19].Add(7500);
            data[19].Add(100);
            data[19].Add(1000);

            List<Elements> elements = new List<Elements>();

            Console.Write("Введите minPts: ");
            int minPts = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите eps: ");
            double eps = Convert.ToDouble(Console.ReadLine());

            for (int i = 0; i < data.Count; i++)
            {
                elements.Add(new Elements(data[i]));
            }

            Console.WriteLine("Парамметры элементов:");
            foreach (Elements element in elements)
            {
                Console.WriteLine(element);
            }

            List<List<Elements>> clusters = Clusters(elements, eps, minPts);

            for (int i = 0; i < clusters.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Кластер № " + (i + 1));
                foreach (Elements e in clusters[i])
                {
                    Console.WriteLine(e);
                }
            }



        static List<Elements> Region(List<Elements> elements, Elements element, double eps)
        {
            List<Elements> region = new List<Elements>();
            for (int i = 0; i < elements.Count; i++)
            {
                double distSquared = Elements.distance(element, elements[i]);

                if (distSquared <= eps)
                {
                    region.Add(elements[i]);
                }
            }
            return region;
        }
    
        static List<List<Elements>> Clusters(List<Elements> elements, double eps, int minPts)
        {
            List<List<Elements>> clusters = new List<List<Elements>>();
            int clusterNumber = 1;
            eps *= eps;

            if (elements == null)
            {
                return null;
            }

            for (int i = 0; i < elements.Count; i++)
            {
                Elements element = elements[i];

                if (element.clusterNumber == Elements.classfied)
                {
                    if (Expand(elements, element, clusterNumber, eps, minPts))
                    {
                        clusterNumber++;
                    }
                }
            }

            int maxCluster = elements.OrderBy(element => element.clusterNumber).Last().clusterNumber;

            if (maxCluster < 1)
            {
                return clusters;
            }

            for (int i = 0; i < maxCluster; i++)
            {
                clusters.Add(new List<Elements>());
            }

            foreach (Elements element in elements)
            {
                if (element.clusterNumber > 0)
                {
                    clusters[element.clusterNumber - 1].Add(element);
                }
            }
            return clusters;
        }

        static bool Expand(List<Elements> elements, Elements element, int clusterNumber, double eps, int minPts)
        {
            List<Elements> seeds = Region(elements, element, eps);
            if (seeds.Count < minPts)
            {
                element.clusterNumber = Elements.noise;
                return false;
            }
            else
            {
                for (int i = 0; i < seeds.Count; i++)
                {
                    seeds[i].clusterNumber = clusterNumber;
                }
                seeds.Remove(element);

                while (seeds.Count > 0)
                {
                    Elements currentE = seeds[0];
                    List<Elements> result = Region(elements, currentE, eps);

                    if (result.Count >= minPts)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            Elements resultE = result[i];
                            if (resultE.clusterNumber == Elements.classfied || resultE.clusterNumber == Elements.noise)
                            {
                                if (resultE.clusterNumber == Elements.classfied)
                                {
                                    seeds.Add(resultE);
                                }
                                resultE.clusterNumber = clusterNumber;
                            }
                        }
                    }
                    seeds.Remove(currentE);
                }
                return true;
            }
        }

    }
}
