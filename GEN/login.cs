namespace GEN    
{
    using System.Net.Mail;
    using System.Net;
    using System.Runtime.InteropServices;
    using SAPbobsCOM;

    public partial class login : Form
    {
        public static SAPbobsCOM.Company oSBO;
        public static string v_usuario;
        public login()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //agarramos las variables par ael login
            string usuario = txtUsuLogin.Text;
            string pass = txtPassLogin.Text;
            if(string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Usuario no puede quedarr vacío","Atención",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Ingresar contraseña", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Cursor = Cursors.WaitCursor;

            //funcion para logearse al sap
            try 
            {
                conex.conexion(usuario, pass);

                if (oSBO.Connected == true)
                {
                    v_usuario = usuario;
                    txtUsuLogin.Text = "";
                    txtPassLogin.Text = "";
                    this.Hide();
                    menu mn = new menu();
                    mn.Show();
                }
                else
                {
                    MessageBox.Show("Error al logearse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            
            
        }

        private void login_Load(object sender, EventArgs e)
        {
            txtUsuLogin.Select();
        }
        #region MOVER FORM
        //para mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
                               
            try
            {
                if (Convert.ToInt32(e.KeyChar) == 13)
                {
                    //agarramos las variables par ael login
                    string usuario = txtUsuLogin.Text;
                    string pass = txtPassLogin.Text;
                    if (string.IsNullOrEmpty(usuario))
                    {
                        MessageBox.Show("Usuario no puede quedarr vacío", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(pass))
                    {
                        MessageBox.Show("Ingresar contraseña", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Cursor = Cursors.WaitCursor;
                    conex.conexion(usuario, pass);

                    if (oSBO.Connected == true)
                    {
                        v_usuario = usuario;
                        txtUsuLogin.Text = "";
                        txtPassLogin.Text = "";
                        this.Hide();
                        menu mn = new menu();
                        mn.Show();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            Cursor= Cursors.Default;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}