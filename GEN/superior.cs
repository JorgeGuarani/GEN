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
    public partial class superior : Form
    {
        //datatable asociacion
        DataTable dtbEmp = new DataTable("ASO");
        DataColumn EMPcod = new DataColumn("col1");
        DataColumn empnom = new DataColumn("col2");
        DataGridViewRow dgvrow = new DataGridViewRow();
        
        public superior()
        {
            InitializeComponent();
        }

        private void superior_Load(object sender, EventArgs e)
        {
            dtbEmp.Columns.Add("Codigo");
            dtbEmp.Columns.Add("Nombre");

            SAPbobsCOM.Recordset oGrillaEmp;
            oGrillaEmp = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrillaEmp.DoQuery("SELECT \"U_LEGAJO\",\"U_NOMBRE\"||', '||\"U_APELLIDO\" FROM \"@JEMPLEADOS\"");
            while (!oGrillaEmp.EoF)
            {
                dtbEmp.Rows.Add(oGrillaEmp.Fields.Item(0).Value.ToString(),
                                  oGrillaEmp.Fields.Item(1).Value.ToString());

                oGrillaEmp.MoveNext();
            }
            dtEmpleado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtEmpleado.DataSource = dtbEmp;
            dtEmpleado.EnableHeadersVisualStyles = false;
            dtEmpleado.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtEmpleado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView vemp = dtbEmp.DefaultView;
            vemp.RowFilter = "Nombre like '%"+txtbuscar.Text+"%'";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtEmpleado.Rows)
            {
                if (row.Selected == true)
                {
                    nomina.v_superiorNomina = row.Cells[0].Value.ToString() +" - "+ row.Cells[1].Value.ToString();
                    nomina nomi = Owner as nomina;
                    //foreach (DataGridViewRow fila in nomi.dtNomina.Rows)
                    //{
                    //    if(fila.Index == nomina.v_filaNomina)
                    //    {
                    //        fila.Cells[5].Value = nomina.v_superiorNomina;
                    //    }
                    //}
                    //this.Close();
                    //nomi.dtNomina.CurrentCell = nomi.dtNomina.Rows[nomina.v_filaNomina].Cells[5];
                    //nomi.dtNomina.BeginEdit(false);
                    foreach (DataGridViewRow fila in nomi.dgvNomina.Rows)
                    {
                        if (fila.Index == nomina.v_filaNomina)
                        {
                            fila.Cells[4].Value = nomina.v_superiorNomina;
                        }
                    }
                    this.Close();
                    nomi.dgvNomina.CurrentCell = nomi.dgvNomina.Rows[nomina.v_filaNomina].Cells[5];
                    nomi.dgvNomina.BeginEdit(false);
                }
            }
            
        }
    }
}
