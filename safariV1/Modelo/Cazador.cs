using safariV1.utiles;
using System;
using System.Drawing;
using System.IO;

namespace Safari.Modelo
    //examen 2
    // clase 2 creada para el examen 2, practicamente igual a Leon pero con atributos de Gazela 
{
    /// <summary>
    /// Cazador: carnívoro. Come Leones y Gazelas, muere de hambre a los 6 turnos sin cazar.
    /// y se reproduce a los 10 turnos.
    /// y puede ser comido por leones 
    /// </summary>
    public class Cazador : Animal
    {
        private Random rnd = new Random();

        public Cazador(int x, int y) : base("Cazador", "Carnivoro", x, y)
        {
            Imagenes imgUtil = new Imagenes();
            Imagen = imgUtil.CargarImagen("cazador.ico");
        }

        /// <summary>
        /// Busca gacela en 8 casillas adyacentes; si encuentra, se mueve y come.
        /// Si no, se mueve aleatoriamente.
        /// </summary>
        public override bool Alimentar(SerVivo[,] tablero)
        {
            if (!EstaVivo) return false;

            int filas = tablero.GetLength(0);
            int cols = tablero.GetLength(1);
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    int nx = PosX + dx;
                    int ny = PosY + dy;
                    if (nx >= 0 && nx < filas && ny >= 0 && ny < cols)
                    {
                        if (tablero[nx, ny] is Gacela gacela && gacela.EstaVivo   )
                        {
                            // come la gacela: mover leon a esa casilla y eliminar la gacela
                            tablero[PosX, PosY] = null;
                            PosX = nx; PosY = ny;
                            tablero[PosX, PosY] = this;
                            Console.WriteLine($"cazador en ({PosX},{PosY}) ha comido una gacela.");
                            PasosSinComer = 0;
                            return true;
                        }else if (tablero[nx, ny] is Leon leon && leon.EstaVivo)
                        {
                            // come la gacela: mover leon a esa casilla y eliminar la gacela
                            tablero[PosX, PosY] = null;
                            PosX = nx; PosY = ny;
                            tablero[PosX, PosY] = this;
                            Console.WriteLine($"cazador en ({PosX},{PosY}) ha comido una leon.");
                            PasosSinComer = 0;
                            return true;

                        }
                    }
                }
            }

            // no encuentra gacela -> moverse aleatoriamente
            if (MoverAleatorio(tablero))
                PasosSinComer++;
            else
                PasosSinComer++;

            ComprobarHambre();
            return false;
        }

        /// <summary>
        /// Reproduce cada 6 pasos: intenta nacer en casilla vacía adyacente.
        /// </summary>
        public override void Reproducirse(SerVivo[,] tablero)
        {
            if (!EstaVivo) return;
            if (Pasos > 0 && Pasos % 10 == 0)
            {
                var (nx, ny) = BuscarCasillaVaciaAdyacente(tablero);
                if (nx != -1)
                {
                    tablero[nx, ny] = new Cazador(nx, ny);
                    Console.WriteLine($"cazador nacido en ({nx},{ny})");
                }
            }
        }
    }
}
