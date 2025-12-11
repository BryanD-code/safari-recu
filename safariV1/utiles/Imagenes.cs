using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safariV1.utiles
{
    internal class Imagenes
    {
        public Bitmap CargarImagen(string img)
        {
            Bitmap imagen = null;
            try
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Img", img);
                if (File.Exists(ruta))
                {
                    // Crear el icono
                    Icon icon = new Icon(ruta);
                   
                    // Convertir a Bitmap y clonarlo para que no dependa del Icono
                    imagen = new Bitmap(icon.ToBitmap());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cargando " + img + ": " + ex.Message);
            }
            return imagen;
        }
    }
}
