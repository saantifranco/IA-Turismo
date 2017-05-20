using System;
using Backpropagation.Nodes;
using System.Collections.Generic;

namespace ConsoleTest
{
    class BackpropagationTest
    {
        private static int inputSize = 13;
        private static int outputSize = 3;
        private static double one = 0.999999999999999;
        private static double zero = 0.0000000000000001;

        static void Main()
        {
            double SI = one;
            double NO = zero;
            string[] lugaresPosibles = new String[] {"Bariloche, San Martín de los Andes",
                                                     "Córdoba, Santa Fé, Bahía Blanca",
                                                     "Pinamar, Villa Gesell, San Bernardo",
                                                     "Iguazú, Esteros del Iberá",
                                                     "Tilcara, Pumamarca",
                                                     "Ushuaia, Calafate",
                                                     "Salsipuedes, Huerta Grande, San Marcos Sierra",
                                                     "Mar del Plata, Rosario"
                                                    };

            var conversor = new Conversor(lugaresPosibles);

            // To know how to use correctly the learning rate and momentum you can read some information about them.
            // But the best you can do is to play with them :)
           
            //Nodos de entrada, nodos de procesamiento, nodos de salida, factor de aprendizaje, momentum (mejora el aprendizaje y acelera la CV)
            var model = new BackProp(inputSize, 10, outputSize, 0.1, 0.25);
            
            model.TrainNetwork(CreatePatternList(), -1, 10000, 0.5, true);
            
            
            Console.WriteLine(model.Print());
            List<Patron> patronesAValidar = new List<Patron>{
                                                            new Patron(new double[] {NO,NO,SI,NO,NO,SI,NO,SI,NO,NO,SI,NO,SI },5,"Mar del Plata, Rosario"), //5
                                                            new Patron(new double[] {NO,NO,SI,NO,NO,SI,SI,NO,NO,SI,NO,NO,SI},8,"Salsipuedes, Huerta Grande, San Marcos Sierra"), //8
                                                            new Patron(new double[] {NO,NO,SI,NO,SI,NO,NO,NO,SI,SI,NO,NO,SI},10,"Iguazú, Esteros del Iberá"),//10
                                                            new Patron(new double[] {NO,NO,SI,SI,NO,NO,NO,NO,SI,NO,SI,NO,SI},16, "Mar del Plata, Rosario"),//16
                                                            new Patron(new double[] {NO,NO,SI,SI,NO,NO,NO,SI,NO,NO,SI,SI,NO},19, "Mar del Plata, Rosario"), //19

                                                            new Patron(new double[] {NO,SI,NO,NO,NO,SI,NO,NO,SI,SI,NO,NO,SI},22,"Iguazú, Esteros del Iberá, Ushuaia, Calafate"), //22
                                                            new Patron(new double[] {NO,SI,NO,NO,NO,SI,NO,SI,NO,NO,SI,NO,SI},24,"Cordoba, Santa Fe, Bahia Blanca, Mar del Plata, Rosario"), //24
                                                            new Patron(new double[] {NO,SI,NO,NO,NO,SI,NO,SI,NO,NO,SI,SI,NO},25,"Cordoba, Santa Fe, Bahia Blanca, Mar del Plata, Rosario"),//25
                                                            new Patron(new double[] {NO,SI,NO,NO,NO,SI,NO,SI,NO,SI,NO,NO,SI},26,"Iguazú, Esteros del Iberá, Ushuaia, Calafate"), //26
                                                            new Patron(new double[] {NO,SI,NO,NO,SI,NO,NO,SI,NO,SI,NO,NO,SI},33,"Iguazú, Esteros del Iberá, Tilcara, Pumamarca"), //33
                                                            new Patron(new double[] {NO,SI,NO,NO,SI,NO,SI,NO,NO,SI,NO,NO,SI},35,"Tilcara, Pumamarca, Salsipuedes, Huerta Grande, San Marcos Sierra"), //35
      
                                                            new Patron(new double[] {SI,NO,NO,NO,NO,SI,NO,NO,SI,SI,NO,NO,SI},45,"Bariloche, San Martin, Ushuaia, Calafate"), //45
                                                            new Patron(new double[] {SI,NO,NO,SI,NO,NO,NO,SI,NO,NO,SI,SI,NO},55,"Mar del Plata, Rosario"), //55
                                                            new Patron(new double[] {SI,NO,NO,SI,NO,NO,NO,SI,NO,SI,NO,NO,SI},56,"Pinamar, Gesell, San Bernardo, Tilcara, Pumamarca"), //56
                                                            new Patron(new double[] {SI,NO,NO,SI,NO,NO,SI,NO,NO,SI,NO,NO,SI},58,"Pinamar, Gesell, San Bernardo, Tilcara, Pumamarca") //58
                                                          };

            foreach (Patron patron in patronesAValidar)
            {
                Console.WriteLine("El resultado para el patron {0} es: " + MapearPatron(patron.input, model, conversor) + " y su salida esperada era " + patron.valorEsperado + Environment.NewLine,patron.idPatron);

            }
            Console.ReadKey();
        }

        private static string MapearPatron(double[] patron, BackProp model, Conversor conversor)
        {
            string valorRetorno = conversor.convertir(getCityID(model.RunNetwork(patron)));

            return valorRetorno;
        }

        // Un input de ejemplo
        private static double[] CreateInput()
        {
            double SI = one;
            double NO = zero;
            return new double[] { NO, NO, SI, SI, NO, NO, NO, SI, NO, SI, NO, SI, NO };
        }

        static int getCityID(double[] array)
        {
            int acum = 0;
            for (int i = 1; i <= array.Length; i++)
                acum += (Math.Round(array[array.Length - i]) > 0 ?  2 ^ (array.Length - i) : 0);
            return acum;
        }
        

        // Lista de patrones de ejemplo
        // Cantidad: 44
        static PatternList CreatePatternList()
        {
            var patterns = new PatternList();

            double SI = one;
            double NO = zero;

            /*
            "Bariloche, San Martín de los Andes" --> 0: zero zero zero
            "Córdoba, Santa Fé, Bahía Blanca" --> 1: zero zero one
            "Pinamar, Villa Gesell, San Bernardo" --> 2: zero one zero
            "Iguazú, Esteros del Iberá" --> 3: zero one one
            "Tilcara, Pumamarca" --> 4: one zero zero
            "Ushuaia, Calafate" --> 5: one zero one
            "Salsipuedes, Huerta Grande, San Marcos Sierra" --> 6: one one zero
            "Mar del Plata, Rosario" --> 7: one one one
            */

            //Patrones
            //1
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, NO, NO, SI, NO, SI, SI, NO },
                          new double[] { one, one, one });
            //2
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, NO, NO, SI, SI, NO, NO, SI },
                          new double[] { zero, one, one });
            //3
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, NO, NO, SI, SI, NO, SI, NO },
                          new double[] { zero, one, one });
            //4
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, NO, SI, NO, NO, SI, NO, SI },
                         new double[] { one, one, one });
            //6
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, NO, SI, NO, SI, NO, NO, SI },
                         new double[] { zero, one, one });
            //7
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, NO, SI, NO, SI, NO, SI, NO },
                         new double[] { zero, one, one });
            //9
            patterns.Add(new double[] { NO, NO, SI, NO, NO, SI, SI, NO, NO, SI, NO, SI, NO },
                         new double[] { one, one, zero });
            //11
            patterns.Add(new double[] { NO, NO, SI, NO, SI, NO, NO, NO, SI, SI, NO, SI, NO },
                         new double[] { zero, one, one });
            //12
            patterns.Add(new double[] { NO, NO, SI, NO, SI, NO, NO, SI, NO, SI, NO, NO, SI },
                         new double[] { zero, one, one });
            //13
            patterns.Add(new double[] { NO, NO, SI, NO, SI, NO, NO, SI, NO, SI, NO, SI, NO },
                         new double[] { zero, one, one });
            //14
            patterns.Add(new double[] { NO, NO, SI, NO, SI, NO, SI, NO, NO, SI, NO, NO, SI },
                         new double[] { one, one, zero });
            //15
            patterns.Add(new double[] { NO, NO, SI, NO, SI, NO, SI, NO, NO, SI, NO, SI, NO },
                         new double[] { one, one, zero });
            //17
            patterns.Add(new double[] { NO, NO, SI, SI, NO, NO, NO, NO, SI, NO, SI, SI, NO },
                         new double[] { one, one, one });
            //18
            patterns.Add(new double[] { NO, NO, SI, SI, NO, NO, NO, SI, NO, NO, SI, NO, SI },
                         new double[] { one, one, one });
            //20
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, NO, NO, SI, NO, SI, NO, SI },
                         new double[] { one, one, one });
            //21
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, NO, NO, SI, NO, SI, SI, NO },
                         new double[] { one, one, one });
            //23
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, NO, NO, SI, SI, NO, SI, NO },
                         new double[] { zero, one, one }); 
            //27
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, NO, SI, NO, SI, NO, SI, NO },
                         new double[] { zero, one, one });
            //28
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, SI, NO, NO, NO, SI, NO, SI },
                         new double[] { zero, zero, one });
            //29
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, SI, NO, NO, NO, SI, SI, NO },
                         new double[] { zero, zero, one });
            //30
            patterns.Add(new double[] { NO, SI, NO, NO, NO, SI, SI, NO, NO, SI, NO, SI, NO },
                         new double[] { one, one, zero });
            //31
            patterns.Add(new double[] { NO, SI, NO, NO, SI, NO, NO, NO, SI, SI, NO, NO, SI },
                         new double[] { zero, one, one });
            //32
            patterns.Add(new double[] { NO, SI, NO, NO, SI, NO, NO, NO, SI, SI, NO, SI, NO },
                         new double[] { zero, one, one });
            //34
            patterns.Add(new double[] { NO, SI, NO, NO, SI, NO, NO, SI, NO, SI, NO, SI, NO },
                         new double[] { zero, one, one });
            //36
            patterns.Add(new double[] { NO, SI, NO, NO, SI, NO, SI, NO, NO, SI, NO, SI, NO },
                         new double[] { one, one, zero });
            //37
            patterns.Add(new double[] { NO, SI, NO, SI, NO, NO, NO, NO, SI, NO, SI, NO, SI },
                         new double[] { one, one, one });
            //38
            patterns.Add(new double[] { NO, SI, NO, SI, NO, NO, NO, NO, SI, NO, SI, SI, NO },
                         new double[] { one, one, one });
            //39
            patterns.Add(new double[] { NO, SI, NO, SI, NO, NO, NO, SI, NO, NO, SI, NO, SI },
                         new double[] { one, one, one });
            //40
            patterns.Add(new double[] { NO, SI, NO, SI, NO, NO, NO, SI, NO, NO, SI, SI, NO },
                         new double[] { one, one, one });
            //41
            patterns.Add(new double[] { NO, SI, NO, SI, NO, NO, NO, SI, NO, SI, NO, NO, SI },
                         new double[] { one, zero, zero });
            //42
            patterns.Add(new double[] { NO, SI, NO, SI, NO, NO, SI, NO, NO, SI, NO, NO, SI },
                         new double[] { one, zero, zero });

            patterns.Add(new double[] { SI, NO, NO, NO, NO, SI, NO, NO, SI, NO, SI, NO, SI },
                         new double[] { one, one, one }); //43
            patterns.Add(new double[] { SI, NO, NO, NO, NO, SI, NO, NO, SI, NO, SI, SI, NO },
                       new double[] { one, one, one }); //44
            patterns.Add(new double[] { SI, NO, NO, NO, NO, SI, NO, SI, NO, NO, SI, NO, SI },
                      new double[] { one, one, one }); //46
            patterns.Add(new double[] { SI, NO, NO, NO, NO, SI, NO, NO, SI, NO, SI, SI, NO },
                      new double[] { one, one, one }); //47
            patterns.Add(new double[] { SI, NO, NO, NO, NO, SI, NO, SI, NO, SI, NO, NO, SI },
                      new double[] { one, zero, one }); //48
            patterns.Add(new double[] { SI, NO, NO, NO, SI, NO, NO, SI, NO, SI, NO, NO, SI },
                      new double[] { one, zero, zero }); //49
            patterns.Add(new double[] { SI, NO, NO, NO, SI, NO, SI, NO, NO, SI, NO, NO, SI },
                     new double[] { one, zero, zero }); //50
            patterns.Add(new double[] { SI, NO, NO, SI, NO, NO, NO, NO, SI, NO, SI, NO, SI },
                     new double[] { one, one, one }); //51
            patterns.Add(new double[] { SI, NO, NO, SI, NO, NO, NO, NO, SI, NO, SI, SI, NO },
                     new double[] { one, one, one }); //52
            patterns.Add(new double[] { SI, NO, NO, SI, NO, NO, NO, NO, SI, SI, NO, SI, NO },
                     new double[] { zero, zero, zero }); //53
            patterns.Add(new double[] { SI, NO, NO, SI, NO, NO, NO, SI, NO, NO, SI, NO, SI },
                     new double[] { one, one, one }); //54
            patterns.Add(new double[] { SI, NO, NO, SI, NO, NO, NO, SI, NO, SI, NO, SI, NO },
                   new double[] { zero, one, zero }); //57
            patterns.Add(new double[] { SI, NO, NO, SI, NO, NO, SI, NO, NO, SI, NO, SI, NO },
                   new double[] { zero, one, zero }); //59

            return patterns;
        }
    }
}
