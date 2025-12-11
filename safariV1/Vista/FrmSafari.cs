using Safari.Controlador;
using Safari.Modelo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Safari.Vista
{
    public partial class FrmSafari : Form
    {
        private SafariController safari;
        private TableLayoutPanel tablero;
        private Label lblPaso;
        private Label lblModo;
        private int pasoContador = 0;
        private int filas = 10, columnas = 10;
        private String modo;

        public FrmSafari()
        {
            Text = "Safari";
            Width = 900;
            Height = 500;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(600, 400);

            // Crear controles
            CrearMenu();
            CrearPanelBotones();
            CrearTablero();

            // Inicializar simulación
            safari = new SafariController(filas, columnas);
            safari.Inicializar();

            ActualizarVista();
        }

        // ===================== MENU =====================
        private void CrearMenu()
        {
            MenuStrip menu = new MenuStrip();

            // Acciones
            ToolStripMenuItem acciones = new ToolStripMenuItem("Acciones");
            ToolStripMenuItem pasoMenu = new ToolStripMenuItem("Paso");
            ToolStripMenuItem pasoDiez = new ToolStripMenuItem("10 pasos");
            pasoMenu.Click += BtnAvanzar_Click;
            pasoDiez.Click += BtnDiez_Click;
            ToolStripMenuItem reiniciarMenu = new ToolStripMenuItem("Reiniciar");
            reiniciarMenu.Click += BtnReiniciar_Click;
            acciones.DropDownItems.Add(pasoMenu);
            acciones.DropDownItems.Add(pasoDiez);
            acciones.DropDownItems.Add(reiniciarMenu);

            // Ayuda
            ToolStripMenuItem ayuda = new ToolStripMenuItem("Ayuda");
            ayuda.Click += (s, e) =>
            {
                MessageBox.Show("Simulación de Safari:\n- Paso: avanza un turno\n- Reiniciar: reinicia el tablero",
                                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Acerca de
            ToolStripMenuItem acercaDe = new ToolStripMenuItem("Acerca de");
            acercaDe.Click += (s, e) =>
            {
                MessageBox.Show("Simulación de Safari creada por Bryan David Gonzalez Martinez",
                                "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            menu.Items.Add(acciones);
            menu.Items.Add(ayuda);
            menu.Items.Add(acercaDe);

            menu.Dock = DockStyle.Top;
            Controls.Add(menu); // agregar primero
        }

        // ===================== PANEL DE BOTONES TIPO "DIV" =====================
        private void CrearPanelBotones()
        {
            FlowLayoutPanel panelBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,          // ocupa todo el ancho automáticamente
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };

            // Altura relativa: 10% del formulario
            panelBotones.Height = (int)(this.ClientSize.Height * 0.1);

            //Examen 2
            //boton 10 pasos 
            Button btnDiez = new Button
            {
                Text = "10 pasos",
                AutoSize = true,
                Height = 30

            };
            btnDiez.Click += BtnDiez_Click;
            

            // Botón Paso
            Button btnAvanzar = new Button
            {
                Text = "Paso",
                AutoSize = true,
                Height = 30
            };
            btnAvanzar.Click += BtnAvanzar_Click;

            // Botón Reiniciar
            Button btnReiniciar = new Button
            {
                Text = "Reiniciar",
                AutoSize = true,
                Height = 30
            };
            btnReiniciar.Click += BtnReiniciar_Click;

            // Label Pasos
            lblPaso = new Label
            {
                Text = "Pasos: 0",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(10, 8, 0, 0)
            };
            //examen 4
            //Label de modo
            lblModo = new Label
            {
                Text= $"Modo : {modo}",
                 AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(10, 8, 0, 0)
            };
            panelBotones.Controls.Add(btnAvanzar);
            panelBotones.Controls.Add(btnDiez);
            panelBotones.Controls.Add(btnReiniciar);
            panelBotones.Controls.Add(lblPaso);
            panelBotones.Controls.Add(lblModo);

            Controls.Add(panelBotones);
            panelBotones.BringToFront(); // asegurar que quede arriba del tablero
        }





        // ===================== TABLERO =====================
        private void CrearTablero()
        {
            tablero = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = columnas,
                RowCount = filas
            };

            // Configurar estilos de columna y fila
            for (int i = 0; i < columnas; i++)
                tablero.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnas));
            for (int j = 0; j < filas; j++)
                tablero.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / filas));

            // --- NUEVO: Pre-llenar el tablero con PictureBoxes vacíos ---
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    PictureBox pb = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent // Opcional, para que se vea limpio
                    };
                    // Asignamos el evento CLICK aquí una sola vez
                    pb.MouseClick += Pb_MouseClick;

                    // Lo añadimos al tablero en su posición (columna j, fila i)
                    tablero.Controls.Add(pb, j, i);
                }
            }

            Controls.Add(tablero);
            tablero.BringToFront();
        }

        // ===================== BOTONES =====================
        private void BtnReiniciar_Click(object sender, EventArgs e)
        {
            safari = new SafariController(filas, columnas);
            safari.Inicializar();

            pasoContador = 0;
            lblPaso.Text = $"Pasos: {pasoContador}";

            ActualizarVista();
        }

        private void BtnAvanzar_Click(object sender, EventArgs e)
        {
            pasoContador++;
            safari.AvanzarPaso();
            lblPaso.Text = $"Pasos: {pasoContador}";
            ActualizarVista();
        }
        //Examen 2
        private void BtnDiez_Click(object sender, EventArgs e)
        {
            pasoContador = pasoContador + 10;
            safari.AvanzarPasoDiez();
            lblPaso.Text = $"Pasos: {pasoContador}";
            ActualizarVista();

        }



        // ===================== ACTUALIZAR TABLERO =====================
        private void ActualizarVista()
        {
            // Congela la lógica de diseño para evitar parpadeos y lentitud
            tablero.SuspendLayout();

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    // Recuperamos el PictureBox que ya creamos en esa posición
                    PictureBox pb = (PictureBox)tablero.GetControlFromPosition(j, i);

                    var ser = safari.Tablero[i, j];

                    if (ser != null && ser.Imagen != null)
                    {
                        pb.Image = ser.Imagen;
                        pb.Tag = ser; 

                    
                    }
                    else
                    {
                        // Si no hay ser, limpiamos la imagen
                        pb.Image = null;
                        pb.Tag = null;
                    }
                }
            }

            // Reactiva el pintado
            tablero.ResumeLayout();
        }

        private void FrmSafari_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmSafari
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "FrmSafari";
            this.Load += new System.EventHandler(this.FrmSafari_Load_1);
            this.ResumeLayout(false);

        }

        private void FrmSafari_Load_1(object sender, EventArgs e)
        {

        }

        private void Pb_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox pb && pb.Tag is SerVivo s)
            {
                MessageBox.Show(s.ToString(), "Información del ser", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}


