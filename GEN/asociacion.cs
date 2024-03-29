﻿using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEN
{
    public partial class asociacion : System.Windows.Forms.Form
    {
        //variables para socio de negocio
        string v_sn = null;
        string v_snNombre = null;
        string v_sndire = null;
        string v_snciu = null;
        //variables para empleado
        string v_legajo = null;
        string v_nombreEmp = null;
        string v_apeEmp = null;
        string v_supEmp = null;
        //variables del empleado a borrar
        string v_boLegajo = null;
        string v_boSN = null;
        //datatable SN
        System.Data.DataTable dtbSN = new System.Data.DataTable("SN");
        System.Data.DataColumn SNcod = new System.Data.DataColumn("col1");
        System.Data.DataColumn SNnom = new System.Data.DataColumn("col2");
        System.Data.DataColumn SNdire = new System.Data.DataColumn("col3");
        System.Data.DataColumn SNciu = new System.Data.DataColumn("col4");
        System.Data.DataColumn SNlat = new System.Data.DataColumn("col5");
        System.Data.DataColumn SNlon = new System.Data.DataColumn("col6");
        //datatable empleado
        System.Data.DataTable dtEmp = new System.Data.DataTable("EMP");
        System.Data.DataColumn EMPleg = new System.Data.DataColumn("col1");
        System.Data.DataColumn EMPnom = new System.Data.DataColumn("col2");
        System.Data.DataColumn EMPape = new System.Data.DataColumn("col3");
        System.Data.DataColumn EMPtel = new System.Data.DataColumn("col4");
        System.Data.DataColumn EMPsup = new System.Data.DataColumn("col5");
        System.Data.DataColumn EMPcat = new System.Data.DataColumn("col6");
        //datatable asociacion
        System.Data.DataTable dtbAso = new System.Data.DataTable("ASO");
        System.Data.DataColumn ASOsn = new System.Data.DataColumn("col1");
        System.Data.DataColumn ASOnom = new System.Data.DataColumn("col2");
        System.Data.DataColumn ASOdire = new System.Data.DataColumn("col3");
        System.Data.DataColumn ASOciu = new System.Data.DataColumn("col4");
        System.Data.DataColumn ASOleg = new System.Data.DataColumn("col5");
        System.Data.DataColumn ASOempnom = new System.Data.DataColumn("col6");
        System.Data.DataColumn ASOempape = new System.Data.DataColumn("col7");
        System.Data.DataColumn ASOempsup = new System.Data.DataColumn("col8");


        public asociacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //si no se selecciono ningun SN
            int v_filaselec = dtSocioN.SelectedRows.Count;
            if (v_filaselec == 0)
            {
                MessageBox.Show("Seleccione un Socio de Negocio!!","Atención",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            //si no se selecciono ningun empleado
            int v_filaselecEmp = dtEmpleado.SelectedRows.Count;
            if (v_filaselecEmp == 0)
            {
                MessageBox.Show("Seleccione empleado!!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //si se selecciono varios SN y empleados
            if(v_filaselec>1 && v_filaselecEmp > 1)
            {
                MessageBox.Show("La asociación no puede ser de muchos a muchos!!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //dtbAso.Clear();
            dtAsociacion.Rows.Clear();
            //si es asociacion 1 a 1
            if (v_filaselec == v_filaselecEmp)
            {
                foreach (DataGridViewRow row in dtSocioN.Rows)
                {

                    if (row.Selected == true)
                    {
                        v_sn = row.Cells[0].Value.ToString();
                        v_snNombre = row.Cells[1].Value.ToString();
                        v_sndire = row.Cells[2].Value.ToString();
                        v_snciu = row.Cells[3].Value.ToString();
                    }
                }
                foreach (DataGridViewRow row in dtEmpleado.Rows)
                {

                    if (row.Selected == true)
                    {
                        v_legajo = row.Cells[0].Value.ToString();
                        v_nombreEmp = row.Cells[1].Value.ToString();
                        v_apeEmp = row.Cells[2].Value.ToString();
                        v_supEmp = row.Cells[4].Value.ToString();
                    }
                }
                //agregar datos a la grilla de asociacion
                dtAsociacion.Rows.Add(v_sn, v_snNombre, v_sndire, v_snciu, v_legajo, v_nombreEmp, v_apeEmp, v_supEmp);
            }
            //si hay mas de un SN seleccionado
            if(v_filaselec > v_filaselecEmp)
            {
                foreach (DataGridViewRow row in dtEmpleado.Rows)
                {

                    if (row.Selected == true)
                    {
                        v_legajo = row.Cells[0].Value.ToString();
                        v_nombreEmp = row.Cells[1].Value.ToString();
                        v_apeEmp = row.Cells[2].Value.ToString();
                        v_supEmp = row.Cells[4].Value.ToString();
                    }
                }
                foreach (DataGridViewRow row in dtSocioN.Rows)
                {

                    if (row.Selected == true)
                    {
                        v_sn = row.Cells[0].Value.ToString();
                        v_snNombre = row.Cells[1].Value.ToString();
                        v_sndire = row.Cells[2].Value.ToString();
                        v_snciu = row.Cells[3].Value.ToString();
                        dtAsociacion.Rows.Add(v_sn, v_snNombre, v_sndire, v_snciu, v_legajo, v_nombreEmp, v_apeEmp, v_supEmp);
                    }
                }
            }
            //si hay mas de un empleado seleccionado
            if(v_filaselec < v_filaselecEmp)
            {
                foreach (DataGridViewRow row in dtSocioN.Rows)
                {

                    if (row.Selected == true)
                    {
                        v_sn = row.Cells[0].Value.ToString();
                        v_snNombre = row.Cells[1].Value.ToString();
                        v_sndire = row.Cells[2].Value.ToString();
                        v_snciu = row.Cells[3].Value.ToString();
                    }
                }
                foreach (DataGridViewRow row in dtEmpleado.Rows)
                {

                    if (row.Selected == true)
                    {
                        v_legajo = row.Cells[0].Value.ToString();
                        v_nombreEmp = row.Cells[1].Value.ToString();
                        v_apeEmp = row.Cells[2].Value.ToString();
                        v_supEmp = row.Cells[4].Value.ToString();
                        dtAsociacion.Rows.Add(v_sn, v_snNombre, v_sndire, v_snciu, v_legajo, v_nombreEmp, v_apeEmp, v_supEmp);
                    }
                }
            }

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string v_textBtn = btnCambiar.Text;
            if (v_textBtn.Equals("Cambiar") || v_textBtn.Equals("Nuevo"))
            {
                tabControl1.SelectTab(1);
                btnCambiar.Text = "Grabar";
                //agarramos la fila seleccionada
                foreach (DataGridViewRow row in dtAsoCentral.Rows)
                {
                    if (row.Selected == true)
                    {
                        v_boSN = row.Cells[0].Value.ToString();
                        v_boLegajo = row.Cells[4].Value.ToString();
                    }
                }
            }
            if (v_textBtn.Equals("Grabar"))
            {
                try
                {                   
                    int v_lineas = dtAsociacion.RowCount -1;
                    int v_index = 0;
                    if (v_lineas == 0)
                    {
                        MessageBox.Show("Debe asociar socio de negocio con empleado!!","Atención",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    while (v_index < v_lineas)
                    {
                        //agarramos las variables
                        string prueba = dtAsociacion.Rows[v_index].Cells[0].Value.ToString();
                        string v_legajoEmp = dtAsociacion.Rows[v_index].Cells[4].Value.ToString();
                        string v_empleado = dtAsociacion.Rows[v_index].Cells[5].Value.ToString() + ", " + dtAsociacion.Rows[v_index].Cells[6].Value.ToString(); 
                        string v_SN = dtAsociacion.Rows[v_index].Cells[0].Value.ToString();
                        string v_SNdesc = dtAsociacion.Rows[v_index].Cells[1].Value.ToString();
                        //buscamos el ultimo codigo

                        SAPbobsCOM.Recordset oMaxcod;
                        oMaxcod = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                        oMaxcod.DoQuery("select max(\"DocEntry\")+1 from \"FG_PROD\".\"@IMPPDV\" ");
                        string v_cod =  oMaxcod.Fields.Item(0).Value.ToString();

                        //armamos el query e insertamos los datos
                        string v_query = "INSERT INTO \"FG_PROD\".\"@IMPPDV\" (\"Code\",\"DocEntry\",\"Object\",\"U_LEG_IMP\",\"U_IMPULSADOR\",\"U_PDV\",\"U_PDV_DESC\") VALUES ('" + v_cod + "','" + v_cod + "','IMPPDV','" + v_legajoEmp + "','" + v_empleado + "','" + v_SN + "','" + v_SNdesc + "') ";
                        SAPbobsCOM.Recordset oInsert;
                        oInsert = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                        oInsert.DoQuery(v_query);
                        v_index++;
                        enviarcorreo(v_SN, v_boSN, v_boLegajo, v_legajoEmp);
                    }
                    //borramos el registro
                    if (!string.IsNullOrEmpty(v_boLegajo))
                    {
                        SAPbobsCOM.Recordset oBorrar;
                        oBorrar = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                        oBorrar.DoQuery("DELETE FROM \"FG_PROD\".\"@IMPPDV\" WHERE \"U_PDV\"='" + v_boSN + "' AND \"U_LEG_IMP\"='" + v_boLegajo + "'");
                        v_boLegajo = "";
                        v_boSN = "";
                    }
                    

                    //dtAsoCentral.Rows.Clear();
                    dtbAso.Clear();
                    cargarGrillaASO();
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("Error al insertar registro",MessageBoxButtons.OK,MessageBoxIcon.Error,"Error");
                }
               
                
                //seteamos los campos
                btnCambiar.Text = "Nuevo";
                tabControl1.SelectTab(0);
                dtAsociacion.Rows.Clear();
                dtAsociacion.Refresh();
                txtBusqueda.Text = "";
            }
        }

        private void asociacion_Load(object sender, EventArgs e)
        {
            //SAPbobsCOM.Recordset oBorrar;
            //oBorrar = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            //oBorrar.DoQuery("DELETE FROM \"FG_PROD\".\"@IMPPDV\" WHERE \"Code\"='7'");
            //oBorrar.DoQuery("DELETE FROM \"FG_PROD\".\"@IMPPDV\" WHERE \"Code\"='10'");
            //cargar grilla de socio de negocio
            dtbSN.Columns.Add("SN");
            dtbSN.Columns.Add("Nombre");
            dtbSN.Columns.Add("Dirección");
            dtbSN.Columns.Add("Ciudad");
            dtbSN.Columns.Add("Latitud");
            dtbSN.Columns.Add("Longitud");           

            SAPbobsCOM.Recordset oGrillaSN;
            oGrillaSN = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrillaSN.DoQuery("SELECT T0.\"CardCode\",T0.\"CardName\",T1.\"Street\",T1.\"County\",T0.\"U_Latitud\",T0.\"U_Longitud\" FROM OCRD T0 INNER JOIN CRD1 T1 ON T0.\"CardCode\"=T1.\"CardCode\" WHERE \"U_jCANAL\" in ('1','2') AND T0.\"CardType\"='C'");
            while (!oGrillaSN.EoF)
            {                
                dtbSN.Rows.Add(oGrillaSN.Fields.Item(0).Value.ToString(),
                                  oGrillaSN.Fields.Item(1).Value.ToString(),
                                  oGrillaSN.Fields.Item(2).Value.ToString(),
                                  oGrillaSN.Fields.Item(3).Value.ToString(),
                                  oGrillaSN.Fields.Item(4).Value.ToString(),
                                  oGrillaSN.Fields.Item(5).Value.ToString());

                oGrillaSN.MoveNext();
            }
            //dtSocioN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtSocioN.DataSource = dtbSN;
            dtSocioN.EnableHeadersVisualStyles = false;
            dtSocioN.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtSocioN.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            


            //cargar grilla de empleado
            dtEmp.Columns.Add("Legajo");
            dtEmp.Columns.Add("Nombre");
            dtEmp.Columns.Add("Apellido");
            dtEmp.Columns.Add("Categoría");
            dtEmp.Columns.Add("Superior");
            dtEmp.Columns.Add("Teléfono");
            SAPbobsCOM.Recordset oGrillaEmp;
            oGrillaEmp = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrillaEmp.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_CATEGORIA\"||' - '||T0.\"U_CATEGORIA\" as \"Categoria\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\" FROM \"@JEMPLEADOS\" T0 INNER JOIN \"@JEMPLEADOS\" T1 ON T0.\"U_SUPERIOR\"=T1.\"U_LEGAJO\"");
            while (!oGrillaEmp.EoF)
            {
                dtEmp.Rows.Add(oGrillaEmp.Fields.Item(0).Value.ToString(),
                                  oGrillaEmp.Fields.Item(1).Value.ToString(),
                                  oGrillaEmp.Fields.Item(2).Value.ToString(),
                                  oGrillaEmp.Fields.Item(3).Value.ToString(),
                                  oGrillaEmp.Fields.Item(4).Value.ToString(),
                                  oGrillaEmp.Fields.Item(5).Value.ToString());

                oGrillaEmp.MoveNext();
            }
            dtEmpleado.DataSource = dtEmp;
            dtEmpleado.EnableHeadersVisualStyles = false;
            dtEmpleado.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtEmpleado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //cargar grilla de asociación
            dtbAso.Columns.Add("SN");
            dtbAso.Columns.Add("Descripción");
            dtbAso.Columns.Add("Dirección");
            dtbAso.Columns.Add("Ciudad");
            dtbAso.Columns.Add("Legajo");
            dtbAso.Columns.Add("Nombre");
            dtbAso.Columns.Add("Apellido");
            dtbAso.Columns.Add("Superior");

            SAPbobsCOM.Recordset oGrillaASO;
            oGrillaASO = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrillaASO.DoQuery("select T0.\"U_PDV\",T0.\"U_PDV_DESC\",T1.\"Street\",T1.\"County\",T0.\"U_LEG_IMP\",T2.\"U_NOMBRE\",T2.\"U_APELLIDO\",T3.\"U_LEGAJO\"||' - '||T3.\"U_NOMBRE\" as \"Superior\" "+
                "from \"FG_PROD\".\"@IMPPDV\" T0 " +
                "INNER JOIN OCRD T4 ON T4.\"CardCode\"=T0.\"U_PDV\" "+
                "INNER JOIN CRD1 T1 ON T4.\"CardCode\"=t1.\"CardCode\" "+
                "INNER JOIN \"@JEMPLEADOS\" T2 ON T0.\"U_LEG_IMP\"=T2.\"U_LEGAJO\" "+
                "INNER JOIN \"@JEMPLEADOS\" T3 ON T2.\"U_SUPERIOR\"=T3.\"U_LEGAJO\" GROUP BY T0.\"U_PDV\",T0.\"U_PDV_DESC\",T1.\"Street\",T1.\"County\",T0.\"U_LEG_IMP\",T2.\"U_NOMBRE\",T2.\"U_APELLIDO\",T3.\"U_LEGAJO\",T3.\"U_NOMBRE\"");
            while (!oGrillaASO.EoF)
            {
                dtbAso.Rows.Add(oGrillaASO.Fields.Item(0).Value.ToString(),
                                  oGrillaASO.Fields.Item(1).Value.ToString(),
                                  oGrillaASO.Fields.Item(2).Value.ToString(),
                                  oGrillaASO.Fields.Item(3).Value.ToString(),
                                  oGrillaASO.Fields.Item(4).Value.ToString(),
                                  oGrillaASO.Fields.Item(5).Value.ToString(),
                                  oGrillaASO.Fields.Item(6).Value.ToString(),
                                  oGrillaASO.Fields.Item(7).Value.ToString());

                oGrillaASO.MoveNext();
            }
            //dtSocioN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtAsoCentral.DataSource = dtbAso;
            dtAsoCentral.EnableHeadersVisualStyles = false;
            dtAsoCentral.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtAsoCentral.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //grilla de asociacion
            dtAsociacion.EnableHeadersVisualStyles = false;
            dtAsociacion.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtAsociacion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView vsn =  dtbSN.DefaultView;
            vsn.RowFilter = string.Format("Nombre like '%{0}%' OR SN like '%{0}%' OR Dirección like '%{0}%' OR Ciudad like '%{0}%'", txtbusSN.Text);
            
        }

        private void txtBusEmp_TextChanged(object sender, EventArgs e)
        {
            DataView vemp = dtEmp.DefaultView;
            vemp.RowFilter = string.Format("Nombre like '%{0}%' OR Apellido like '%{0}%' OR Legajo like '%{0}%' OR Teléfono like '%{0}%' OR Superior like '%{0}%' OR Categoría like '%{0}%'", txtBusEmp.Text);
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DataView vaso = dtbAso.DefaultView;
            vaso.RowFilter = string.Format("SN like '%{0}%' OR Descripción like '%{0}%' OR Dirección like '%{0}%' OR Ciudad like '%{0}%' OR Legajo like '%{0}%' OR Nombre like '%{0}%' OR Apellido like '%{0}%' OR Superior like '%{0}%'",txtBusqueda.Text);
        }

        //funcion para cargar grilla de asociacion
        private void cargarGrillaASO()
        {
            //cargar grilla de asociación
            //dtbAso.Columns.Add("SN");
            //dtbAso.Columns.Add("Descripción");
            //dtbAso.Columns.Add("Dirección");
            //dtbAso.Columns.Add("Ciudad");
            //dtbAso.Columns.Add("Legajo");
            //dtbAso.Columns.Add("Nombre");
            //dtbAso.Columns.Add("Apellido");
            //dtbAso.Columns.Add("Superior");

            SAPbobsCOM.Recordset oGrillaASO;
            oGrillaASO = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrillaASO.DoQuery("select T0.\"U_PDV\",T0.\"U_PDV_DESC\",T1.\"Street\",T1.\"County\",T0.\"U_LEG_IMP\",T2.\"U_NOMBRE\",T2.\"U_APELLIDO\",T3.\"U_LEGAJO\"||' - '||T3.\"U_NOMBRE\" as \"Superior\" " +
                "from \"FG_PROD\".\"@IMPPDV\" T0 " +
                "INNER JOIN OCRD T4 ON T4.\"CardCode\"=T0.\"U_PDV\" " +
                "INNER JOIN CRD1 T1 ON T4.\"CardCode\"=t1.\"CardCode\" " +
                "INNER JOIN \"@JEMPLEADOS\" T2 ON T0.\"U_LEG_IMP\"=T2.\"U_LEGAJO\" " +
                "INNER JOIN \"@JEMPLEADOS\" T3 ON T2.\"U_SUPERIOR\"=T3.\"U_LEGAJO\" GROUP BY T0.\"U_PDV\",T0.\"U_PDV_DESC\",T1.\"Street\",T1.\"County\",T0.\"U_LEG_IMP\",T2.\"U_NOMBRE\",T2.\"U_APELLIDO\",T3.\"U_LEGAJO\",T3.\"U_NOMBRE\"");
            while (!oGrillaASO.EoF)
            {
                dtbAso.Rows.Add(oGrillaASO.Fields.Item(0).Value.ToString(),
                                  oGrillaASO.Fields.Item(1).Value.ToString(),
                                  oGrillaASO.Fields.Item(2).Value.ToString(),
                                  oGrillaASO.Fields.Item(3).Value.ToString(),
                                  oGrillaASO.Fields.Item(4).Value.ToString(),
                                  oGrillaASO.Fields.Item(5).Value.ToString(),
                                  oGrillaASO.Fields.Item(6).Value.ToString(),
                                  oGrillaASO.Fields.Item(7).Value.ToString());

                oGrillaASO.MoveNext();
            }
            //dtSocioN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtAsoCentral.DataSource = dtbAso;
            
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void asociacion_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtAsoCentral_SelectionChanged(object sender, EventArgs e)
        {
            int v_canSelect = dtAsoCentral.SelectedRows.Count;
            if(v_canSelect > 0)
            {
                btnCambiar.Text = "Cambiar";
            }
            else
            {
                btnCambiar.Text = "Nuevo";
                v_boLegajo = "";
                v_boSN = "";
            }
        }

        private void eliminarFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //agarramos la fila seleccionada
            foreach (DataGridViewRow row in dtAsoCentral.Rows)
            {
                if (row.Selected == true)
                {
                   //dtAsoCentral.Rows.Remove(row);
                    string v_sn = row.Cells[0].Value.ToString();
                    string v_imp = row.Cells[4].Value.ToString();
                    //eliminamos la linea de la base de datos
                    SAPbobsCOM.Recordset oEliminar;
                    oEliminar = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oEliminar.DoQuery("DELETE FROM \"FG_PROD\".\"@IMPPDV\" WHERE \"U_LEG_IMP\"='"+v_imp+"' AND \"U_PDV\"='"+v_sn+"' ");

                    MessageBox.Show("Fila eliminada","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    dtbAso.Clear();
                    cargarGrillaASO();
                }
            }
        }


        private void enviarcorreo(string sn, string snanterior, string legajoanterior, string legajo)
        {
            try
            {
                //busqueda de descripcion PDV actual
                SAPbobsCOM.Recordset oSN;
                oSN = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oSN.DoQuery("SELECT \"CardName\" FROM OCRD WHERE \"CardCode\"='"+sn+"'");
                string pdvActual = oSN.Fields.Item(0).Value.ToString();
                //busqueda de descripcion PDV anterior
                SAPbobsCOM.Recordset oSNant;
                oSNant = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oSNant.DoQuery("SELECT \"CardName\" FROM OCRD WHERE \"CardCode\"='"+ snanterior + "'");
                string pdvAnterior = oSNant.Fields.Item(0).Value.ToString();
                //busqueda de LEGAJO ACTUAL
                SAPbobsCOM.Recordset oLegAct;
                oLegAct = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oLegAct.DoQuery("SELECT \"U_APELLIDO\", \"U_NOMBRE\", \"U_SUPERIOR\" FROM \"@JEMPLEADOS\" WHERE \"U_LEGAJO\"='"+ legajo + "'");
                string legActualNom = oLegAct.Fields.Item(0).Value.ToString();
                string legActualApe = oLegAct.Fields.Item(1).Value.ToString();
                string legActualSup = oLegAct.Fields.Item(2).Value.ToString();
                //busqueda de LEGAJO ANTERIOR
                SAPbobsCOM.Recordset oLegAnt;
                oLegAnt = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oLegAnt.DoQuery("SELECT \"U_APELLIDO\", \"U_NOMBRE\", \"U_SUPERIOR\" FROM \"@JEMPLEADOS\" WHERE \"U_LEGAJO\"='" + legajoanterior + "'");
                string legAntNom = oLegAnt.Fields.Item(0).Value.ToString();
                string legAntApe = oLegAnt.Fields.Item(1).Value.ToString();
                string legAntSup = oLegAnt.Fields.Item(2).Value.ToString();
                //busqueda de SUPERIOR
                SAPbobsCOM.Recordset oSup;
                oSup = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oSup.DoQuery("SELECT \"U_APELLIDO\", \"U_NOMBRE\" FROM \"@JEMPLEADOS\" WHERE \"U_LEGAJO\"='" + legActualSup + "'");
                string supApe = oSup.Fields.Item(0).Value.ToString();
                string supNom = oSup.Fields.Item(1).Value.ToString();;

                string correoOrigen = "hermes.news@fguarani.com.py"; 
                string pass = "Guarani@321**";
                string correoDestino = "hermes.rrhh@fguarani.com.py";
                string cuerpo = "<b> Notificacion de Cambio de PDV </b> <br><br>" +                              
                                "<b> Fecha:  </b>" + DateTime.Now.ToString("dd/MM/yyyy") + "<br>" +
                                "<b> ________________________________________________________________ </b><br>" +
                                "<b> Supervisor:  </b>" + legActualSup + " - "+ supApe+", "+ supNom + "<br>" +
                                "<b> PDV actual:  </b>" + sn + " - " + pdvActual + " <br>" +
                                "<b> Cod. legajo actual:  </b>" + legajo + " - " + legActualApe + ", "+ legActualNom + " <br>" +
                                "<b> ________________________________________________________________ </b><br>" +
                                "<b> PDV anterior:  </b>" + snanterior + " - "+ pdvAnterior;

                MailMessage correo = new MailMessage(correoOrigen, correoDestino, "Frigorífico Guarani", "Cambio de PDV");
                correo.Body = cuerpo;
                correo.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                SmtpClient smtp = new SmtpClient("smtp.office365.com");
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(correoOrigen, pass);

                try
                {
                    smtp.Send(correo);
                }
                catch (Exception e)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
