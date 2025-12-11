using safariV1.utiles;
using System;
using System.Drawing;
using System.IO;


namespace Safari.Modelo
{
    /// <summary>
    /// Gacela: herbívoro. Come plantas, se reproduce cada 4 pasos, muere si no come 3 pasos.
    /// </summary>
    public class Gacela : Animal

    {
     
        
        private Random rnd = new Random();
      

        public Gacela(int x, int y) : base("Gacela", "Herbivoro", x, y)
        {
          // Llamada a la funcion de cargar imagen para gacela.
          Imagenes imgUtil = new Imagenes();
            Imagen = imgUtil.CargarImagen("gacela.ico");

        }
        
        /// <summary>
        /// Busca plantas en las 8 casillas adyacentes y si encuentra, se mueve allí y come.
        /// Si no hay, se mueve aleatoriamente a una casilla vacía adyacente.
        /// Devuelve true si ha comido.
        /// </summary>
        public override bool Alimentar(SerVivo[,] tablero)
        {
            //examen 3
         

            if (!EstaVivo) return false;

            // buscar planta adyacente (8 direcciones)
            int filas = tablero.GetLength(0);
            int cols = tablero.GetLength(1);
            // dx y dy moviemiento de la gazela respecto a su posición.
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {   // para que no revise la casilla en la que se encuntra.
                    if (dx == 0 && dy == 0) continue;
                    // las casillas que estan abyacentes.
                    int nx = PosX + dx;
                    int ny = PosY + dy;
                    //para que no salga del tablero. 
                    if (nx >= 0 && nx < filas && ny >= 0 && ny < cols)
                    {
                        if (tablero[nx, ny] is Planta planta && planta.EstaVivo)
                        {
                            // come la planta: mover gacela a esa casilla y eliminar planta
                            tablero[PosX, PosY] = null;
                            PosX = nx; PosY = ny;
                            tablero[PosX, PosY] = this;
                            Console.WriteLine($" Gacela en ({PosX},{PosY}) ha comido una planta.");
                            PasosSinComer = 0;
                            return true;
                        
                        }
                    }
                }
            }

            // no hay comida: moverse aleatoriamente a casilla vacía adyacente
            if (MoverAleatorio(tablero))
            {
                // no ha comido
                PasosSinComer++;
            }
            else
            {
                // si no se puede mover, incrementa también pasosSinComer
                PasosSinComer++;
            }

            // comprobar si muere por hambre
            ComprobarHambre();
            return false;
        }

        /// <summary>
        /// Reproducción cada 4 pasos: si toca, intenta nacer en casilla vacía adyacente.
        /// </summary>
        public override void Reproducirse(SerVivo[,] tablero)
        {
            if (!EstaVivo) return;
            if (Pasos > 0 && Pasos % 4 == 0)
            {
                var (nx, ny) = BuscarCasillaVaciaAdyacente(tablero);
                if (nx != -1)
                {
                    tablero[nx, ny] = new Gacela(nx, ny);
                    Console.WriteLine($" Gacela nacida en ({nx},{ny})");
                }
            }
        }
    }
}
