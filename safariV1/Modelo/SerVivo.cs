using System;
using System.Drawing;

namespace Safari.Modelo
{
    /// <summary>
    /// Clase base abstracta para todos los seres vivos del safari.
    /// Contiene atributos comunes y la firma de los métodos que deben implementar las subclases.
    /// </summary>
    public abstract class SerVivo
    {
        // Propiedades públicas (actúan como getters/setters)
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Planta", "Herbívoro", "Carnívoro" (opcional)
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Pasos { get; set; }        // número de pasos desde inicio
        public bool EstaVivo { get; set; }   // estado de vida
        public Image Imagen { get; protected set; } // imagen para la UI

        /// <summary>
        /// Constructor básico
        /// </summary>
        protected SerVivo(string nombre, string tipo, int x, int y)
        {
            Nombre = nombre;
            Tipo = tipo;
            PosX = x;
            PosY = y;
            Pasos = 0;
            EstaVivo = true;
        }

        /// <summary>
        /// Incrementa el contador de pasos del ser.
        /// </summary>
        public virtual void DarPaso()
        {
            Pasos++;
        }

        /// <summary>
        /// Mata al ser (cambia su estado).
        /// </summary>
        public virtual void Morir()
        {
            EstaVivo = false;
        }

        /// <summary>
        /// Intenta alimentarse. Debe devolver true si ha comido en este paso (para resetear hambre).
        /// Implementación específica en subclases.
        /// </summary>
        /// <param name="tablero">Tablero (matriz de SerVivo)</param>
        /// <returns>true si ha comido</returns>
        public abstract bool Alimentar(SerVivo[,] tablero);

        /// <summary>
        /// Intenta reproducirse (si corresponde). Debe colocar el nuevo ser en el tablero si hay hueco.
        /// </summary>
        /// <param name="tablero">Tablero donde añadir la cría</param>
        public abstract void Reproducirse(SerVivo[,] tablero);

        /// <summary>
        /// Representación textual del ser vivo.
        /// </summary>
        public override string ToString()
        {
            return $"{Nombre}({Tipo}) [{(EstaVivo ? "Vivo" : "Muerto")}] Pos=({PosX},{PosY}) Pasos={Pasos}";
        }
    }
}
