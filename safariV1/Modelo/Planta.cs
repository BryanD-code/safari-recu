using safariV1.utiles;
using System;
using System.Drawing;
using System.IO;

namespace Safari.Modelo
{
    /// <summary>
    /// Planta: se reproduce cada 2 pasos. No se mueve ni come.
    /// </summary>
    public class Planta : SerVivo
    {
        public Planta(int x, int y) : base("Planta", "Planta", x, y)
        {
            Imagenes imgUtil = new Imagenes();
            Imagen = imgUtil.CargarImagen("planta.ico");
        }

        /// <summary>
        /// Plantas no comen.
        /// </summary>
        public override bool Alimentar(SerVivo[,] tablero) => false;

        /// <summary>
        /// Si han pasado múltiplos de 2 pasos, intentan nacer en una casilla vacía adyacente.
        /// </summary>
        public override void Reproducirse(SerVivo[,] tablero)
        {
            if (!EstaVivo) return;
            if (Pasos > 0 && Pasos % 2 == 0)
            {
                // Buscar casilla vacía adyacente (4 direcciones preferidas)
                (int, int)[] dirs = { (-1, 0), (1, 0), (0, -1), (0, 1) };
                var rnd = new Random();
                // mezclar orden
                for (int i = 0; i < dirs.Length; i++)
                {
                    int j = rnd.Next(i, dirs.Length);
                    var tmp = dirs[i]; dirs[i] = dirs[j]; dirs[j] = tmp;
                }

                foreach (var (dx, dy) in dirs)
                {
                    int nx = PosX + dx;
                    int ny = PosY + dy;
                    if (nx >= 0 && nx < tablero.GetLength(0) && ny >= 0 && ny < tablero.GetLength(1) && tablero[nx, ny] == null)
                    {
                        tablero[nx, ny] = new Planta(nx, ny);
                        Console.WriteLine($" Planta nacida en ({nx},{ny})");
                        break;
                    }
                }
            }
        }
    }
}
