using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAPbobsCOM;
using SAPbouiCOM;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GEN
{
    public partial class nomina : System.Windows.Forms.Form
    {
        //datatable empleado
        System.Data.DataTable dtEmp = new System.Data.DataTable("EMP");
        System.Data.DataColumn EMPleg = new System.Data.DataColumn("col1");
        System.Data.DataColumn EMPnom = new System.Data.DataColumn("col2");
        System.Data.DataColumn EMPape = new System.Data.DataColumn("col3");
        System.Data.DataColumn EMPenr = new System.Data.DataColumn("col4");
        System.Data.DataColumn EMPcat = new System.Data.DataColumn("col5");
        System.Data.DataColumn EMPsup = new System.Data.DataColumn("col6");
        System.Data.DataColumn EMPtel = new System.Data.DataColumn("col7");
        System.Data.DataColumn EMPcom = new System.Data.DataColumn("col8");
        System.Data.DataTable dtEmp2 = new System.Data.DataTable("EMP2");
        public static bool v_reload = false;
        //variable
        public static int v_filaNomina = 0;
        public static string v_superiorNomina = null;
        public static string filtro_leg = null;
        public static string filtro_nom = null;
        public static string filtro_ape = null;
        public static string filtro_enrol = null;
        public static string filtro_sup = null;
        public static string filtro_corp = null;
        public static string filtro_comp = null;
        public static string filtro_categ = null;
        public static bool filtro_confirmar = false;


        public System.Data.DataTable v_dt = new System.Data.DataTable();
        public nomina()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region MOVER FORM
        //para mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region EVENTOS

        #region FORM NOMINA
        //funcion para cargar grilla completa
        private void cargarGrilla()
        {
            dtNomina.Rows.Clear();
            SAPbobsCOM.Recordset oGrilla;
            oGrilla = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrilla.DoQuery("SELECT \"U_LEGAJO\",\"U_NOMBRE\",\"U_APELLIDO\",\"U_COD_ENROLAMIENTO\",\"U_U_FECHA_BAJA\",\"U_COD_CATEGORIA\",\"U_SUPERIOR\",\"U_CELULAR\",\"U_TEL_CO\",\"U_TEL_CO_FECHA\" FROM \"@JEMPLEADOS\"");
            while (!oGrilla.EoF)
            {
                dtNomina.Rows.Add(oGrilla.Fields.Item(0).Value.ToString(),
                                  oGrilla.Fields.Item(1).Value.ToString(),
                                  oGrilla.Fields.Item(2).Value.ToString(),
                                  oGrilla.Fields.Item(3).Value.ToString(),
                                  oGrilla.Fields.Item(4).Value.ToString(),
                                  oGrilla.Fields.Item(5).Value.ToString(),
                                  oGrilla.Fields.Item(6).Value.ToString(),
                                  oGrilla.Fields.Item(7).Value.ToString(),
                                  oGrilla.Fields.Item(8).Value.ToString(),
                                  oGrilla.Fields.Item(9).Value.ToString());

               

                oGrilla.MoveNext();

            }

        }

        //funcion para cargar grilla con busqueda
        private void BusquedaDnamica(string v_busq)
        {
            dtNomina.Rows.Clear();
            SAPbobsCOM.Recordset oGrilla;
            oGrilla = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            oGrilla.DoQuery("SELECT \"U_LEGAJO\",\"U_NOMBRE\",\"U_APELLIDO\",\"U_COD_ENROLAMIENTO\",\"U_U_FECHA_BAJA\",\"U_COD_CATEGORIA\",\"U_SUPERIOR\",\"U_CELULAR\",\"U_TEL_CO\",\"U_TEL_CO_FECHA\" FROM \"@JEMPLEADOS\" WHERE " +
                            " \"U_LEGAJO\" LIKE '%" + v_busq + "%' " +
                            " OR \"U_NOMBRE\" LIKE '%" + v_busq + "%'" +
                            " OR \"U_APELLIDO\" LIKE '%" + v_busq + "%'" +
                            " OR \"U_COD_ENROLAMIENTO\" LIKE '%" + v_busq + "%'" +
                            " OR \"U_COD_CATEGORIA\" LIKE '%" + v_busq + "%'" +
                            " OR \"U_SUPERIOR\" LIKE '%" + v_busq + "%'" +
                            " OR \"U_TEL_CO\" LIKE '%" + v_busq + "%'");

            while (!oGrilla.EoF)
            {
                dtNomina.Rows.Add(oGrilla.Fields.Item(0).Value.ToString(),
                                  oGrilla.Fields.Item(1).Value.ToString(),
                                  oGrilla.Fields.Item(2).Value.ToString(),
                                  oGrilla.Fields.Item(3).Value.ToString(),
                                  oGrilla.Fields.Item(4).Value.ToString(),
                                  oGrilla.Fields.Item(5).Value.ToString(),
                                  oGrilla.Fields.Item(6).Value.ToString(),
                                  oGrilla.Fields.Item(7).Value.ToString(),
                                  oGrilla.Fields.Item(8).Value.ToString(),
                                  oGrilla.Fields.Item(9).Value.ToString());
                oGrilla.MoveNext();

            }
        }

        //Funcion para la busqueda dinamica (al momento de escribir)
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscarNom.Text))
            {
                //recargarGrilla();
            }
            else
            {
                //DataView vnom = dtEmp.DefaultView;
                //vnom.RowFilter = string.Format("Legajo like '%{0}%' OR Nombre like '%{0}%' OR Apellido like '%{0}%' OR Enrolamiento like '%{0}%' OR Categoría like '%{0}%' OR Superior like '%{0}%' OR Corporativo like '%{0}%' ", txtBuscarNom.Text);

                DataView vnom2 = dtEmp2.DefaultView;
                vnom2.RowFilter = string.Format("Legajo like '%{0}%' OR Nombre like '%{0}%' OR Apellido like '%{0}%' OR Enrolamiento like '%{0}%' OR Categoria like '%{0}%' OR Superior like '%{0}%' OR Corporativo like '%{0}%' OR Compania like '%{0}%'  ", txtBuscarNom.Text);
                dgvNomina.Rows.Clear();
                foreach (DataRowView row in vnom2)
                {
                    string pp = row[0].ToString();
                    string pp1 = row[1].ToString();
                    string pp2 = row[2].ToString();
                    string pp3 = row[3].ToString();
                    string pp4 = row[4].ToString();
                    string pp5 = row[5].ToString();
                    string pp6 = row[6].ToString();
                    string pp7 = row[7].ToString();

                    dgvNomina.Rows.Add(pp, pp1, pp2, pp3, pp4, pp5, pp6, pp7);
                }               


            }




        }

        //funcion para cerrar el formulario
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        //funcion para actualizar la tabla JEMPLEADO
        private void actualizarJempleado(string v_legajo, string v_categ, string v_superior, string v_celular, string v_compania)
        {
            try
            {
                Recordset oBusCate;
                oBusCate = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oBusCate.DoQuery("select \"Code\" from \"@JCATEGORIA_EMPLEADO\" where \"Name\"='"+v_categ+"'");
                string v_codCate = oBusCate.Fields.Item(0).Value.ToString();

                Recordset oActJE;
                oActJE = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                oActJE.DoQuery("UPDATE \"@JEMPLEADOS\" SET \"U_COD_CATEG\"='" + v_codCate + "',\"U_CATEGORIA\"='" + v_categ + "',\"U_SUPERIOR\"='" + v_superior + "',\"U_CELULAR\"='" + v_celular + "',\"U_TEL_CO\"='" + v_compania + "' WHERE \"U_LEGAJO\"='" + v_legajo + "' ");
                MessageBox.Show("Se actualizo correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al actualizar registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //funcion al presiona el boton actualizar
        private void button1_Click(object sender, EventArgs e)
        {
            //int v_filaselec = dtNomina.SelectedRows.Count;
            //if (v_filaselec == 0)
            //{
            //    MessageBox.Show("Seleccione fila para actualizar!!");
            //    return;
            //}
            //foreach (DataGridViewRow row in dtNomina.Rows)
            //{

            //    if (row.Selected == true)
            //    {
            //        string v_legajo = row.Cells[0].Value.ToString();
            //        string v_categ = row.Cells[4].Value.ToString();
            //        string v_superior = row.Cells[5].Value.ToString();
            //        string v_celular = row.Cells[6].Value.ToString();
            //        string v_compania = row.Cells[7].Value.ToString();
            //        string v_cate = row.Cells[8].Value.ToString(); //dtNomina.Rows[row.Index].Cells[8].Value.ToString();
            //        //agarramos solo el codigo
            //        int can = 0;
            //        string superior_v = null;
            //        int index = v_superior.IndexOf("-");
            //        if(index>0)
            //        {
            //            can = v_superior.Length;

            //            superior_v = v_superior.Remove(index, can - index);
            //            superior_v = superior_v.Replace(" ", string.Empty);
            //        }                                       
            //        actualizarJempleado(v_legajo, v_cate, superior_v, v_celular, v_compania);
            //        dtEmp.Clear();
            //        txtBuscarNom.Text = "";
            //        recargarGrilla();
            //    }
            //}
            int v_filaselec = dgvNomina.SelectedRows.Count;
            if (v_filaselec == 0)
            {
                MessageBox.Show("Seleccione fila para actualizar!!");
                return;
            }
            foreach (DataGridViewRow row in dgvNomina.Rows)
            {

                if (row.Selected == true)
                {
                    string v_legajo = row.Cells[0].Value.ToString();
                    //string v_categ = row.Cells[4].Value.ToString();
                    string v_superior = row.Cells[4].Value.ToString();
                    string v_celular = row.Cells[5].Value.ToString();
                    string v_compania = row.Cells[6].Value.ToString();
                    string v_cate = row.Cells[7].Value.ToString(); //dtNomina.Rows[row.Index].Cells[8].Value.ToString();
                    //agarramos solo el codigo
                    int can = 0;
                    string superior_v = null;
                    int index = v_superior.IndexOf("-");
                    if (index > 0)
                    {
                        can = v_superior.Length;

                        superior_v = v_superior.Remove(index, can - index);
                        superior_v = superior_v.Replace(" ", string.Empty);
                    }
                    actualizarJempleado(v_legajo, v_cate, superior_v, v_celular, v_compania);
                    //dtEmp2.Clear();
                    txtBuscarNom.Text = "";
                    recargarGrilla();
                }
            }
        }
        #endregion



        #endregion

        private void nomina_Load(object sender, EventArgs e)
        {
            dtNomina.Visible = false;
            dtEmp.Reset();
            dtEmp.Columns.Add("Legajo");
            dtEmp.Columns.Add("Nombre");
            dtEmp.Columns.Add("Apellido");
            dtEmp.Columns.Add("Enrolamiento");
            dtEmp.Columns.Add("Categoría");
            dtEmp.Columns.Add("Superior");
            dtEmp.Columns.Add("Corporativo");
            //dtEmp.Columns.Add("Compañía",typeof(System.Windows.Forms.ComboBox));
            dtEmp2.Reset();
            dtEmp2.Columns.Add("Legajo");
            dtEmp2.Columns.Add("Nombre");
            dtEmp2.Columns.Add("Apellido");
            dtEmp2.Columns.Add("Enrolamiento");
            dtEmp2.Columns.Add("Superior");
            dtEmp2.Columns.Add("Corporativo");
            dtEmp2.Columns.Add("Compania");
            dtEmp2.Columns.Add("Categoria");



            SAPbobsCOM.Recordset oGrilla;
            oGrilla = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            //oGrilla.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_ENROLAMIENTO\",T0.\"U_COD_CATEGORIA\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\" FROM \"@JEMPLEADOS\" T0 LEFT JOIN \"@JEMPLEADOS\" T1 ON T1.\"U_LEGAJO\"=T0.\"U_SUPERIOR\" ");
            oGrilla.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_ENROLAMIENTO\",T0.\"U_COD_CATEGORIA\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\",T0.\"U_CATEGORIA\",T0.\"U_TEL_CO\" FROM \"@JEMPLEADOS\" T0 LEFT JOIN \"@JEMPLEADOS\" T1 ON T1.\"U_LEGAJO\"=T0.\"U_SUPERIOR\" order by T0.\"DocEntry\" asc\r\n");
            int v_filaNomi = 0;
            while (!oGrilla.EoF)
            {
                dtEmp2.Rows.Add(oGrilla.Fields.Item(0).Value.ToString(),
                                oGrilla.Fields.Item(1).Value.ToString(),
                                oGrilla.Fields.Item(2).Value.ToString(),
                                oGrilla.Fields.Item(3).Value.ToString(),
                                oGrilla.Fields.Item(5).Value.ToString(),
                                oGrilla.Fields.Item(6).Value.ToString(),
                                oGrilla.Fields.Item(8).Value.ToString(),
                                oGrilla.Fields.Item(7).Value.ToString());


                //dtEmp.Rows.Add(oGrilla.Fields.Item(0).Value.ToString(),
                //                  oGrilla.Fields.Item(1).Value.ToString(),
                //                  oGrilla.Fields.Item(2).Value.ToString(),
                //                  oGrilla.Fields.Item(3).Value.ToString(),
                //                  oGrilla.Fields.Item(4).Value.ToString(),
                //                  oGrilla.Fields.Item(5).Value.ToString(),
                //                  oGrilla.Fields.Item(6).Value.ToString());
                //oGrilla.Fields.Item(7).Value.ToString());
                //dtNomina.Rows.Add();
                //dtNomina.Rows[v_filaNomi].Cells[1].Value = oGrilla.Fields.Item(0).Value.ToString();
                oGrilla.MoveNext();
                v_filaNomi++;
            }
            //dgvNomina.AutoResizeColumns();
            //dgvNomina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dtNomina.DataSource = dtEmp;

            int empcount = dtEmp2.Rows.Count;
            //string pp = dtEmp2.Rows[0]["Compania"].ToString();
            int count = 0;
            while (count < empcount)
            {
                dgvNomina.Rows.Add(dtEmp2.Rows[count]["Legajo"].ToString(), dtEmp2.Rows[count]["Nombre"].ToString(), dtEmp2.Rows[count]["Apellido"].ToString(), dtEmp2.Rows[count]["Enrolamiento"].ToString(),
                               dtEmp2.Rows[count]["Superior"].ToString(), dtEmp2.Rows[count]["Corporativo"].ToString(), dtEmp2.Rows[count]["Compania"].ToString(), dtEmp2.Rows[count]["Categoria"].ToString());
                count++;
            }


            dgvNomina.EnableHeadersVisualStyles = false;
            dgvNomina.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvNomina.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dgvNomina.Rows[0].Cells[1].Style.BackColor = Color.LightGreen;
            //inhabilitar las columnas
            int v_filaNomina = dgvNomina.Rows.Count;
            int v_con = 0;
            while (v_con < v_filaNomina)
            {
                dgvNomina.Rows[v_con].Cells[0].Style.BackColor = Color.WhiteSmoke;
                dgvNomina.Rows[v_con].Cells[1].Style.BackColor = Color.WhiteSmoke;
                dgvNomina.Rows[v_con].Cells[2].Style.BackColor = Color.WhiteSmoke;
                dgvNomina.Rows[v_con].Cells[3].Style.BackColor = Color.WhiteSmoke;               
                v_con++;
            }
            //agregar combobox a la grilla
            //DataGridViewComboBoxColumn cbCell = new DataGridViewComboBoxColumn();
            //cbCell.DisplayIndex = 8;
            //cbCell.HeaderText = "Compañia";
            //cbCell.Items.Add("CLARO");
            //cbCell.Items.Add("TIGO");
            //cbCell.Items.Add("PERSONAL");
            //cbCell.Name = "Compañía";
            //dtNomina.Columns.Add(cbCell);


            //int v_filaCia = 0;
            ////cargar las compañias de cada empleado
            //SAPbobsCOM.Recordset oGrilla2;
            //oGrilla2 = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            //oGrilla2.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_ENROLAMIENTO\",T0.\"U_COD_CATEGORIA\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\",T0.\"U_CATEGORIA\",T0.\"U_TEL_CO\" FROM \"@JEMPLEADOS\" T0 LEFT JOIN \"@JEMPLEADOS\" T1 ON T1.\"U_LEGAJO\"=T0.\"U_SUPERIOR\" order by T0.\"DocEntry\" asc\r\n ");
            //while (!oGrilla2.EoF)
            //{
            //    string v_cia = oGrilla2.Fields.Item(8).Value.ToString();
            //    if (!string.IsNullOrEmpty(v_cia))
            //    {
            //        dtNomina.Rows[v_filaCia].Cells[7].Value = v_cia;
            //    }
            //    v_filaCia++;
            //    oGrilla2.MoveNext();
            //}

            ////cargar las categoria de cada empleado
            //SAPbobsCOM.Recordset oCat;
            //oCat = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            //oCat.DoQuery("SELECT \"Name\" FROM \"@JCATEGORIA_EMPLEADO\" ");
            //DataGridViewComboBoxColumn cbCell2 = new DataGridViewComboBoxColumn();
            //cbCell2.DisplayIndex = 9;
            //cbCell2.HeaderText = "Categoría";
            //while (!oCat.EoF)
            //{
            //    cbCell2.Items.Add(oCat.Fields.Item(0).Value.ToString());
            //    oCat.MoveNext();
            //}
            //dtNomina.Columns.Add(cbCell2);
            //dtNomina.Columns[4].Visible= false;
            //SAPbobsCOM.Recordset oCatSelect;
            //oCatSelect = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            //oCatSelect.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_ENROLAMIENTO\",T0.\"U_COD_CATEGORIA\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\",T0.\"U_CATEGORIA\",T0.\"U_TEL_CO\" FROM \"@JEMPLEADOS\" T0 LEFT JOIN \"@JEMPLEADOS\" T1 ON T1.\"U_LEGAJO\"=T0.\"U_SUPERIOR\" order by T0.\"DocEntry\" asc\r\n");
            //int v_filaCat = 0;
            //while (!oCatSelect.EoF)
            //{
            //    string v_cat = oCatSelect.Fields.Item(7).Value.ToString();
            //    if (!v_cat.Equals("A DEFINIR"))
            //    {
            //        dtNomina.Rows[v_filaCat].Cells[8].Value = v_cat;
            //    }
            //    v_filaCat++;
            //    oCatSelect.MoveNext();
            //}


        }

        private void recargarGrilla()
        {
            dtEmp2.Clear();
            dtEmp2.Rows.Clear();           

            SAPbobsCOM.Recordset oGrilla;
            oGrilla = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
            //oGrilla.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_ENROLAMIENTO\",T0.\"U_COD_CATEGORIA\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\" FROM \"@JEMPLEADOS\" T0 LEFT JOIN \"@JEMPLEADOS\" T1 ON T1.\"U_LEGAJO\"=T0.\"U_SUPERIOR\" ");
            oGrilla.DoQuery("SELECT T0.\"U_LEGAJO\",T0.\"U_NOMBRE\",T0.\"U_APELLIDO\",T0.\"U_COD_ENROLAMIENTO\",T0.\"U_COD_CATEGORIA\",T0.\"U_SUPERIOR\"||' - '||T1.\"U_NOMBRE\",T0.\"U_CELULAR\",T0.\"U_CATEGORIA\",T0.\"U_TEL_CO\" FROM \"@JEMPLEADOS\" T0 LEFT JOIN \"@JEMPLEADOS\" T1 ON T1.\"U_LEGAJO\"=T0.\"U_SUPERIOR\" order by T0.\"DocEntry\" asc\r\n");
            int v_filaNomi = 0;
            while (!oGrilla.EoF)
            {
                dtEmp2.Rows.Add(oGrilla.Fields.Item(0).Value.ToString(),
                                oGrilla.Fields.Item(1).Value.ToString(),
                                oGrilla.Fields.Item(2).Value.ToString(),
                                oGrilla.Fields.Item(3).Value.ToString(),
                                oGrilla.Fields.Item(5).Value.ToString(),
                                oGrilla.Fields.Item(6).Value.ToString(),
                                oGrilla.Fields.Item(8).Value.ToString(),
                                oGrilla.Fields.Item(7).Value.ToString());


                //dtEmp.Rows.Add(oGrilla.Fields.Item(0).Value.ToString(),
                //                  oGrilla.Fields.Item(1).Value.ToString(),
                //                  oGrilla.Fields.Item(2).Value.ToString(),
                //                  oGrilla.Fields.Item(3).Value.ToString(),
                //                  oGrilla.Fields.Item(4).Value.ToString(),
                //                  oGrilla.Fields.Item(5).Value.ToString(),
                //                  oGrilla.Fields.Item(6).Value.ToString());
                //oGrilla.Fields.Item(7).Value.ToString());
                //dtNomina.Rows.Add();
                //dtNomina.Rows[v_filaNomi].Cells[1].Value = oGrilla.Fields.Item(0).Value.ToString();
                oGrilla.MoveNext();
                v_filaNomi++;
            }
            //dgvNomina.AutoResizeColumns();
            //dgvNomina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dtNomina.DataSource = dtEmp;

            int empcount = dtEmp2.Rows.Count;
            //string pp = dtEmp2.Rows[0]["Compania"].ToString();
            int count = 0;
            while (count < empcount)
            {
                dgvNomina.Rows.Add(dtEmp2.Rows[count]["Legajo"].ToString(), dtEmp2.Rows[count]["Nombre"].ToString(), dtEmp2.Rows[count]["Apellido"].ToString(), dtEmp2.Rows[count]["Enrolamiento"].ToString(),
                               dtEmp2.Rows[count]["Superior"].ToString(), dtEmp2.Rows[count]["Corporativo"].ToString(), dtEmp2.Rows[count]["Compania"].ToString(), dtEmp2.Rows[count]["Categoria"].ToString());
                count++;
            }



            dgvNomina.EnableHeadersVisualStyles = false;
            dgvNomina.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvNomina.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dgvNomina.Rows[0].Cells[1].Style.BackColor = Color.LightGreen;
            //inhabilitar las columnas
            int v_filaNomina = dgvNomina.Rows.Count;
            int v_con = 0;
            while (v_con < v_filaNomina)
            {
                dgvNomina.Rows[v_con].Cells[0].Style.BackColor = Color.WhiteSmoke;
                dgvNomina.Rows[v_con].Cells[1].Style.BackColor = Color.WhiteSmoke;
                dgvNomina.Rows[v_con].Cells[2].Style.BackColor = Color.WhiteSmoke;
                dgvNomina.Rows[v_con].Cells[3].Style.BackColor = Color.WhiteSmoke;
                v_con++;
            }





        }

        public class Modelo
        {
            public string legajo { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string enrolamiento { get; set; }
            public string fechabaja { get; set; }
            public string categoria { get; set; }
            public string superior { get; set; }
            public string corporativo { get; set; }
            public string compania { get; set; }
            public string fechavto { get; set; }
        }

        private void dtNomina_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                v_filaNomina = e.RowIndex;
                superior supe = new superior();
                AddOwnedForm(supe);
                supe.Show();
            }
            
        }

        private void dtNomina_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                string v_texto = dtNomina.Rows[e.RowIndex].Cells[6].Value.ToString();
                if (!string.IsNullOrEmpty(v_texto))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(v_texto, "[0-9]"))
                    {
                        MessageBox.Show("Solo se admiten números!!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtNomina.Rows[e.RowIndex].Cells[6].Value = "";
                        dtNomina.CurrentCell = dtNomina.Rows[e.RowIndex].Cells[6];
                        return;
                    }
                    int v_canTexto = v_texto.Length;
                    if (v_canTexto > 12)
                    {
                        dtNomina.Rows[e.RowIndex].Cells[6].Value = "";
                        MessageBox.Show("El número no debe exeder 12 caracteres", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (v_canTexto < 12)
                    {
                        dtNomina.Rows[e.RowIndex].Cells[6].Value = "";
                        MessageBox.Show("El número debe tener 12 caracteres", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


            }
        }

        private void dtNomina_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (dtNomina.CurrentCell.ColumnIndex == 6)
            //{
            //    string v_texto = dtNomina.Rows[dtNomina.CurrentRow.Index].Cells[6].Value.ToString();
            //    if (!string.IsNullOrEmpty(v_texto))
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(v_texto, "[0-9]"))
            //        {
            //            MessageBox.Show("Solo se admiten números!!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            dtNomina.Rows[dtNomina.CurrentRow.Index].Cells[6].Value = "";
            //        }
            //    }

            //}
        }

        private void dtNomina_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.ColumnIndex == 6)
            //{
            //    string v_texto = dtNomina.Rows[e.RowIndex].Cells[6].Value.ToString();
            //    if (!string.IsNullOrEmpty(v_texto))
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(v_texto, "[0-9]"))
            //        {
            //            MessageBox.Show("Solo se admiten números!!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            dtNomina.Rows[e.RowIndex].Cells[6].Value = "";
            //        }
            //    }

            //}
        }

        private void dtNomina_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (dtNomina.CurrentCell.ColumnIndex == 6)
            //{
            //    string v_texto = dtNomina.Rows[dtNomina.CurrentRow.Index].Cells[6].Value.ToString();
            //    if (!string.IsNullOrEmpty(v_texto))
            //    {
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(v_texto, "[0-9]"))
            //        {
            //            MessageBox.Show("Solo se admiten números!!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            dtNomina.Rows[dtNomina.CurrentRow.Index].Cells[6].Value = "";
            //        }
            //    }

            //}
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtNomina.Rows)
            {
                int v_cant = 0;
                int v_gridcan = dtNomina.RowCount;
                while (v_cant < v_gridcan)
                {
                    string v_legajo = row.Cells[0].Value.ToString();
                    SAPbobsCOM.Recordset oDatos;
                    oDatos = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oDatos.DoQuery("select \"U_TEL_CO\",\"U_CATEGORIA\" from \"@JEMPLEADOS\" WHERE \"U_LEGAJO\"='" + v_legajo + "'");
                    string v_compa = oDatos.Fields.Item(0).Value.ToString();
                    string v_categoria = oDatos.Fields.Item(1).Value.ToString();
                    dtNomina.Rows[row.Index].Cells[7].Value = v_compa;
                    dtNomina.Rows[row.Index].Cells[8].Value = v_categoria;
                    v_cant++;
                }
            }
        }

        private void nomina_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtBuscarNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                int v_cant = 0;
                int v_gridcan = dtNomina.RowCount-1;
                  
                    while (v_cant < v_gridcan)
                    {
                        string v_legajo = dtNomina.Rows[v_cant].Cells[0].Value.ToString();
                        SAPbobsCOM.Recordset oDatos;
                        oDatos = (SAPbobsCOM.Recordset)login.oSBO.GetBusinessObject(BoObjectTypes.BoRecordset);
                        oDatos.DoQuery("select \"U_TEL_CO\",\"U_CATEGORIA\" from \"@JEMPLEADOS\" WHERE \"U_LEGAJO\"='" + v_legajo + "'");
                        string v_compa = oDatos.Fields.Item(0).Value.ToString();
                        string v_categoria = oDatos.Fields.Item(1).Value.ToString();
                        dtNomina.Rows[v_cant].Cells[7].Value = v_compa;
                        if(v_categoria!="A DEFINIR")
                        {
                            dtNomina.Rows[v_cant].Cells[8].Value = v_categoria;
                        }
                        
                        v_cant++;
                    }
                
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            filtros filtro = new filtros();
            AddOwnedForm(filtro);
            filtro.Show();
        }

        private void dgvNomina_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                v_filaNomina = e.RowIndex;
                superior supe = new superior();
                AddOwnedForm(supe);
                supe.Show();
            }
        }
    }
}
