using System;

namespace Safari.Modelo
{
    /// <summary>
    /// Clase intermedia que contiene comportamiento común a animales
    /// (gacela y leon): control de hambre y utilidad básica.
    /// </summary>
    public abstract class Animal : SerVivo
    {
        // Contador de pasos sin comer; si llega a 3 => muere
        public int PasosSinComer { get; set; }

        protected Animal(string nombre, string tipo, int x, int y) : base(nombre, tipo, x, y)
        {
            PasosSinComer = 0;
        }

        /// <summary>
        /// Método auxiliar: busca en las 8 casillas adyacentes un ser del tipo buscado.
        /// Devuelve las coordenadas si lo encuentra, o (-1,-1) si no.
        /// </summary>
        protected (int, int) BuscarComidaAdyacente<T>(SerVivo[,] tablero) where T : SerVivo
        {
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
                        if (tablero[nx, ny] is T target && target != null && target.EstaVivo)
                            return (nx, ny);
                    }
                }
            }
            return (-1, -1);
        }

        /// <summary>
        /// Busca una casilla vacía adyacente (8 direcciones) y devuelve coordenadas o (-1,-1).
        /// </summary>
        protected (int, int) BuscarCasillaVaciaAdyacente(SerVivo[,] tablero)
        {
            int filas = tablero.GetLength(0);
            int cols = tablero.GetLength(1);
            var rnd = new Random();

            // comprobar las 8 posibles en orden aleatorio para dispersar reproducción/movimiento
            (int, int)[] dirs = new (int, int)[]
            {
                (-1,-1),(-1,0),(-1,1),(0,-1),(0,1),(1,-1),(1,0),(1,1)
            };

            // barajar
            for (int i = 0; i < dirs.Length; i++)
            {
                int j = rnd.Next(i, dirs.Length);
                var tmp = dirs[i]; dirs[i] = dirs[j]; dirs[j] = tmp;
            }

            foreach (var (dx, dy) in dirs)
            {
                int nx = PosX + dx;
                int ny = PosY + dy;
                if (nx >= 0 && nx < filas && ny >= 0 && ny < cols && tablero[nx, ny] == null)
                    return (nx, ny);
            }
            return (-1, -1);
        }

        /// <summary>
        /// Movimiento aleatorio a una casilla vacía adyacente si existe.
        /// </summary>
        protected bool MoverAleatorio(SerVivo[,] tablero)
        {
            var (nx, ny) = BuscarCasillaVaciaAdyacente(tablero);
            if (nx != -1)
            {
                tablero[PosX, PosY] = null;
                PosX = nx;
                PosY = ny;
                tablero[PosX, PosY] = this;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Si un animal no come en 3 pasos seguidos, muere.
        /// </summary>
        protected void ComprobarHambre()
        {
            if (PasosSinComer >= 3 &&  Nombre!= "Cazador" )
            {
                Morir();
                Console.WriteLine($" {Nombre} en ({PosX},{PosY}) ha muerto de hambre.");
            }
            //examane 2
            else if (PasosSinComer >= 6)
            {
                Morir();
                Console.WriteLine($" {Nombre} en ({PosX},{PosY}) ha muerto de hambre.");
            }
        }
        // Dia 2 turnos, noche 1 turno --> no puede mover, cazar ni moverse, si reproducirse o morir. 
        //Examane 3
        protected bool Modo()
        {
            if (Pasos > 0 && Pasos % 2 != 0)
            {
                return false;
            }
            return true;
        }



    }
    }

