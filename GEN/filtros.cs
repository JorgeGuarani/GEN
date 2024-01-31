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
    public partial class filtros : Form
    {
        public filtros()
        {
            InitializeComponent();

            SAPbobsCOM.Recordset oCat;
            oCat = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oCat.DoQuery("SELECT \"Name\" FROM \"@JCATEGORIA_EMPLEADO\" ");
            while (!oCat.EoF)
            {
                cbCateeg.Items.Add(oCat.Fields.Item(0).Value.ToString());
                oCat.MoveNext();
            }

        }

        //FUNCION PARA MOVER EL FORM
        #region MOVER FORM
        //para mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            nomina.filtro_confirmar = true;
            nomina.filtro_leg = txtLegajo.Text;
            nomina.filtro_nom = txtNom.Text;
            nomina.filtro_ape = txtApe.Text;
            nomina.filtro_enrol = txtEnrol.Text;
            nomina.filtro_sup = txtSup.Text;
            nomina.filtro_corp = txtCorp.Text;
            nomina.filtro_comp = cbComp.GetItemText(cbComp.SelectedItem);
            nomina.filtro_categ = cbCateeg.GetItemText(cbCateeg.SelectedItem);           
            
            //fin prueba
            Close();
        }
    }
}
