using System;
using System.Collections.Generic;

namespace Safari.Modelo
{
    /// <summary>
    /// Clase que representa el Modelo de datos del tablero.
    /// Contiene la matriz y métodos seguros para manipularla.
    /// </summary>
    public class TableroSimulacion
    {
        public SerVivo[,] Grilla { get; private set; }
        public int Filas { get; private set; }
        public int Columnas { get; private set; }

        public TableroSimulacion(int filas, int columnas)
        {
            Filas = filas;
            Columnas = columnas;
            Grilla = new SerVivo[filas, columnas];
        }

        public SerVivo ObtenerSer(int f, int c)
        {
            if (EsCoordenadaValida(f, c))
                return Grilla[f, c];
            return null;
        }

        public void ColocarSer(int f, int c, SerVivo ser)
        {
            if (EsCoordenadaValida(f, c))
            {
                Grilla[f, c] = ser;
                if (ser != null)
                {
                    ser.PosX = f;
                    ser.PosY = c;
                }
            }
        }

        public void MoverSer(int fOrigen, int cOrigen, int fDestino, int cDestino)
        {
            if (EsCoordenadaValida(fOrigen, cOrigen) && EsCoordenadaValida(fDestino, cDestino))
            {
                var ser = Grilla[fOrigen, cOrigen];
                Grilla[fOrigen, cOrigen] = null; // Vaciar origen
                Grilla[fDestino, cDestino] = ser; // Ocupar destino

                if (ser != null)
                {
                    ser.PosX = fDestino;
                    ser.PosY = cDestino;
                }
            }
        }

        public void EliminarSer(int f, int c)
        {
            if (EsCoordenadaValida(f, c))
                Grilla[f, c] = null;
        }

        public bool EsCoordenadaValida(int f, int c)
        {
            return f >= 0 && f < Filas && c >= 0 && c < Columnas;
        }
    }
}