using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEN
{
   
    public partial class menu : Form
    {
        public static bool auxMax;
        public menu()
        {
            InitializeComponent();
            auxMax = false;
        }

        private void menu_Load(object sender, EventArgs e)
        {
            SAPbobsCOM.Recordset oUsuario;
            oUsuario = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oUsuario.DoQuery("SELECT \"U_NAME\" FROM OUSR WHERE \"USER_CODE\"='"+ login.v_usuario.ToString() + "'");
            string v_usuario = oUsuario.Fields.Item(0).Value.ToString();
            int index = v_usuario.IndexOf(" ");
            int cantidad = v_usuario.Length;
            string v_nombre = v_usuario.Remove(index, cantidad - index);
            string v_apellido = v_usuario.Remove(0, index);
            lblUsuario.Text = v_nombre;
            lblapellido.Text = v_apellido;
            btnCal.Enabled = false;
            btnPermi.Enabled = false;
        }

        #region MOVER FORM
        //para mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        #endregion

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(auxMax==false)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMax.Visible = false;
                btnMax.Visible = true;
                auxMax= true;
                return;
            }
            if (auxMax == true)
            {
                this.WindowState = FormWindowState.Normal;
                btnMax.Visible = false;
                btnMax.Visible = true;
                auxMax = false;
                return;
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea salir del sistema?","Salir",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(respuesta == DialogResult.Yes)
            {
                Application.Exit();
                login.oSBO.Disconnect();
            }
           
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea salir del sistema?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
                login.oSBO.Disconnect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nomina nomi = new nomina();
            AddOwnedForm(nomi);
            nomi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            asociacion aso = new asociacion();
            AddOwnedForm(aso);
            aso.Show();
        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
