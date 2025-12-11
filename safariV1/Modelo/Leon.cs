using safariV1.utiles;
using System;
using System.Drawing;
using System.IO;

namespace Safari.Modelo
{
    /// <summary>
    /// León: carnívoro. Come gacelas, se reproduce cada 6 pasos, muere si no come 3 pasos.
    /// </summary>
    public class Leon : Animal
    {
        private Random rnd = new Random();

        public Leon(int x, int y) : base("Leon", "Carnivoro", x, y)
        {
            Imagenes imgUtil = new Imagenes();
            Imagen = imgUtil.CargarImagen("leon.ico");
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
                        if (tablero[nx, ny] is Gacela gacela && gacela.EstaVivo)
                        {
                            // come la gacela: mover leon a esa casilla y eliminar la gacela
                            tablero[PosX, PosY] = null;
                            PosX = nx; PosY = ny;
                            tablero[PosX, PosY] = this;
                            Console.WriteLine($"León en ({PosX},{PosY}) ha comido una gacela.");
                            PasosSinComer = 0;
                            return true;
                        }
                        else if (tablero[nx, ny] is Leon leon && leon.EstaVivo)
                        {
                            // come la gacela: mover leon a esa casilla y eliminar la gacela
                            tablero[PosX, PosY] = null;
                            PosX = nx; PosY = ny;
                            tablero[PosX, PosY] = this;
                            Console.WriteLine($"León en ({PosX},{PosY}) ha comido una cazador.");
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
            if (Pasos > 0 && Pasos % 6 == 0)
            {
                var (nx, ny) = BuscarCasillaVaciaAdyacente(tablero);
                if (nx != -1)
                {
                    tablero[nx, ny] = new Leon(nx, ny);
                    Console.WriteLine($"León nacido en ({nx},{ny})");
                }
            }
        }
    }
}
