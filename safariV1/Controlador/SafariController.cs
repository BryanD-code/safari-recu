using Safari.Modelo;
using System;
using System.Collections.Generic;

namespace Safari.Controlador
{
    /// <summary>
    /// Controlador principal que gestiona el tablero y la simulación por pasos.
    /// </summary>
    public class SafariController
    {
        public SerVivo[,] Tablero { get; private set; }
        private int filas;
        private int columnas;
        private Random rnd = new Random();
        private bool modo;
        
        

        /// <summary>
        /// Crea el controlador con el tamaño de tablero.
        /// </summary>
        public SafariController(int filas, int columnas)
        {
            this.filas = filas;
            this.columnas = columnas;
            Tablero = new SerVivo[filas, columnas];
        }

        /// <summary>
        /// Inicializa el tablero con una distribución aleatoria de seres.
        /// Ajusta las proporciones a tu gusto.
        /// </summary>
        public void Inicializar()
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    int r = rnd.Next(0, 10);
                    if (r < 4)
                        Tablero[i, j] = new Planta(i, j);      // 40% plantas
                    else if (r < 7)
                        Tablero[i, j] = new Gacela(i, j);      // 30% gacelas
                    else if (r < 9)
                        Tablero[i, j] = new Leon(i, j);        // 20% leones
                    else
                        Tablero[i, j] = new Cazador(i, j);      // 10% cazador
                }
            }
        }

        /// <summary>
        /// Ejecuta un paso de la simulación:
        /// - snapshot de seres (para que recién nacidos no actúen en el mismo paso)
        /// - alimentación/movimiento
        /// - incremento de pasos y reproducción
        /// - limpieza de seres muertos
        /// </summary>
        public void AvanzarPaso()
        {
            int f = Tablero.GetLength(0);
            int c = Tablero.GetLength(1);

            // Tomar snapshot de los seres existentes al inicio del paso
            var seresSnapshot = new List<SerVivo>();
            for (int i = 0; i < f; i++)
                for (int j = 0; j < c; j++)
                    if (Tablero[i, j] != null)
                        seresSnapshot.Add(Tablero[i, j]);

            // 1) Alimentación y movimiento (estos métodos actualizan el tablero directamente)
            foreach (var ser in seresSnapshot)
            {
                // si fue eliminado o murió por otra acción, saltar
                if (!ser.EstaVivo) continue;
                // sólo animales y plantas tienen implementación concreta
                
                bool comio = ser.Alimentar(Tablero);
                // si es animal y comió, su contador PasosSinComer ha sido reseteado en el método
                // (implementado en Animal)
            }

            // 2) Incrementar pasos y controlar reproducción
            foreach (var ser in seresSnapshot)
            {
                if (!ser.EstaVivo) continue;
                
                ser.DarPaso(); // incrementa Pasos
                
                ser.Reproducirse(Tablero); // cada clase aplicará su regla
            }

            // 3) Limpiar los que hayan muerto (por hambre u otros)
            for (int i = 0; i < f; i++)
                for (int j = 0; j < c; j++)
                    if (Tablero[i, j] != null && !Tablero[i, j].EstaVivo)
                        Tablero[i, j] = null;
        }

        public void AvanzarPasoDiez()
        {
            for (int i = 0; i <= 10; i++)
            {
                AvanzarPaso();
            }
            
            
        }
    }
}
