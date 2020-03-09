﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1_201800714
{
    public partial class Form1 : Form
    {
        //Contador de pestañas
        private int cont_pestaña = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void AbrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\Users\\Fernando\\OneDrive\\Documentos";
            ofd.Filter = "Archivos ER (*.er)|*.er|Todos los Archivos (*.*)|*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                //Verificando existencia de archivo
                try
                {
                    string ruta = ofd.FileName;
                    var flujo = ofd.OpenFile();
                    string contenido;
                    using (StreamReader lector = new StreamReader(flujo))
                    {
                        contenido = lector.ReadToEnd();
                    }
                    bool existe = tabControl1.SelectedTab.HasChildren;
                    if (existe)
                    {
                        tabControl1.SelectedTab.Controls.RemoveAt(0);
                        RichTextBox nuevaCajaTexto = new RichTextBox();

                        nuevaCajaTexto.Dock = DockStyle.Fill;
                        nuevaCajaTexto.Text = contenido;
                        tabControl1.SelectedTab.Controls.Add(nuevaCajaTexto);
                        tabControl1.SelectedTab.Controls.SetChildIndex(nuevaCajaTexto, 0);
                    }
                    else
                    {
                        RichTextBox nuevaCajaTexto = new RichTextBox();
                        nuevaCajaTexto.Dock = DockStyle.Fill;
                        nuevaCajaTexto.Text = contenido;
                        tabControl1.SelectedTab.Controls.Add(nuevaCajaTexto);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("Ha ocurrido un error en la carga.");
                }
            }
        }
        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control item in tabControl1.SelectedTab.Controls)
            {
                Boolean a = typeof(RichTextBox).IsInstanceOfType(item);
                if (a)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Archivos ER | *.er";
                    saveFileDialog.ShowDialog();
                    String ruta = saveFileDialog.FileName;
                    String contenido = item.Text;
                    File.WriteAllText(ruta, contenido);
                }
            }
        }


        private void agregarPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cont_pestaña++;
            TabPage pestañaN = new CustomTab(cont_pestaña);
            tabControl1.TabPages.Add(pestañaN);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        
    }
    public class CustomTab : TabPage
    {
        private int num;
        public CustomTab(int contarPestaña)
        {
            this.num = contarPestaña;
            RichTextBox cajadetexto = new RichTextBox();
            this.Controls.Add(cajadetexto);
            this.Text = "Pestaña " + num;
            cajadetexto.Dock = DockStyle.Fill;
        }
    }
}
