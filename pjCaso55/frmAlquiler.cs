using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjCaso55
{
    public partial class frmAlquiler : Form
    {
        public frmAlquiler()
        {
            InitializeComponent();
            tHora.Enabled = true;
        }

        private void frmAlquiler_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void tHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //Capturar los datos
            string habitacion = cboTipo.Text;
            int dias = int.Parse(txtDias.Text);

            //Asignacion de costo por habitacion 
            double costo = 0;
            switch (habitacion)
            {
                case "Simple": costo = 30; break;
                case "Doble": costo = 50; break;
                case "Matrimonial": costo = 120; break;
                case "Presidencial": costo = 300; break;
            }

            //Realizando los calculos
            double subtotal = costo * dias;

            double descuento = 0;
            if (dias <= 5)
                descuento = 0;
            else if (dias > 5 && dias <= 10)
                descuento = 0.05 * subtotal;
            else if (dias > 10 && dias <= 30)
                descuento = 0.1 * subtotal;
            else if (dias > 30)
                descuento = 0.2 * subtotal;

            double neto = subtotal - descuento;
            
            //Enviar los datos al registro de alquiler
            ListViewItem fila = new ListViewItem(habitacion);
            fila.SubItems.Add(costo.ToString("0.00"));
            fila.SubItems.Add(dias.ToString());
            fila.SubItems.Add(subtotal.ToString("0.00"));
            fila.SubItems.Add(descuento.ToString("0.00"));
            fila.SubItems.Add(neto.ToString("0.00"));
            lvAlquiler.Items.Add(fila);

            //Determinar las estadisticas 
            int i = 0;
            int cSimple = 0, cDoble = 0, cMatrimonial = 0, cPresidencial = 0;
            double tSimple = 0, tDoble = 0, tMatrimonial = 0, tPresidencial = 0;

            do {
                string tipo = lvAlquiler.Items[i].SubItems[0].Text;
                switch (tipo)
                {
                    case "Simple":
                        cSimple += int.Parse(lvAlquiler.Items[i].SubItems[2].Text);
                        tSimple+= double.Parse(lvAlquiler.Items[i].SubItems[5].Text);
                        break;
                    case "Doble":
                        cDoble += int.Parse(lvAlquiler.Items[i].SubItems[2].Text);
                        tDoble += double.Parse(lvAlquiler.Items[i].SubItems[5].Text);
                        break;

                    case "Matrimonial":
                        cMatrimonial += int.Parse(lvAlquiler.Items[i].SubItems[2].Text);
                        tMatrimonial += double.Parse(lvAlquiler.Items[i].SubItems[5].Text);
                        break;
                    case "Presidencial":
                        cPresidencial += int.Parse(lvAlquiler.Items[i].SubItems[2].Text);
                        tPresidencial += double.Parse(lvAlquiler.Items[i].SubItems[5].Text);
                        break;
                }
                i++;
            } while (i<lvAlquiler.Items.Count);

            //Imprimir las estadisticas
            //Imprimiendo las estadisticas
            lvEstadisticas.Items.Clear();
            string[] elementosFila = new string[3];
            ListViewItem row;

            elementosFila[0] = "Habitacion Simple";
            elementosFila[1] = cSimple.ToString();
            elementosFila[2] = tSimple.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Habitacion Doble";
            elementosFila[1] = cDoble.ToString();
            elementosFila[2] = tDoble.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Habitacion Matrimonial";
            elementosFila[1] = cMatrimonial.ToString();
            elementosFila[2] = tMatrimonial.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Habitacion Presidencial";
            elementosFila[1] = cPresidencial.ToString();
            elementosFila[2] = tPresidencial.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Esta seguro de salir?",
                                  "Seguros",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Exclamation);
            if (r == DialogResult.Yes) this.Close();
        }
    }
}
