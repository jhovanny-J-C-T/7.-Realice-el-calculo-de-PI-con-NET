using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        const int totalPuntos = 1000000; // Número total de puntos a generar
        int puntosDentroCirculo = 0;
        object lockObj = new object(); // Objeto de bloqueo para evitar condiciones de carrera

        Parallel.For(0, totalPuntos, i =>
        {
            Random random = new Random(); // Cada hilo necesita su propia instancia de Random
            double x = random.NextDouble(); // Coordenada x aleatoria entre 0 y 1
            double y = random.NextDouble(); // Coordenada y aleatoria entre 0 y 1

            // Calcula la distancia desde el punto al centro (0.5, 0.5)
            double distanciaAlCentro = Math.Sqrt(Math.Pow(x - 0.5, 2) + Math.Pow(y - 0.5, 2));

            // Si la distancia es menor o igual a 0.5, el punto está dentro del círculo
            if (distanciaAlCentro <= 0.5)
            {
                lock (lockObj) // Bloqueo para evitar condiciones de carrera
                {
                    puntosDentroCirculo++;
                }
            }
        });

        // Estima el valor de pi utilizando la relación entre puntos dentro y fuera del círculo
        double estimacionPi = 4.0 * puntosDentroCirculo / totalPuntos;

        Console.WriteLine($"Estimación de PI usando {totalPuntos} puntos y programación paralela: {estimacionPi}");
    }
}

