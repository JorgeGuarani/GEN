using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;


namespace GEN
{
    internal class conex
    {
        public static void conexion(string usuario, string pass)
        {
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            oCompany.Server = "saphana:30015";
            oCompany.DbServerType = BoDataServerTypes.dst_HANADB;
            oCompany.UseTrusted = false;
            oCompany.DbUserName = "SYSTEM";
            oCompany.DbPassword = "Admin123";
            oCompany.CompanyDB = "FG_PROD";
            oCompany.UserName = usuario;//"manager";
            oCompany.Password = pass;// "54321";
            oCompany.language = BoSuppLangs.ln_Spanish_La;
            //oCompany.LicenseServer = "192.168.20.1:30015";
            int conexion = oCompany.Connect();
            if(conexion != 0 )
            {
                MessageBox.Show("Error de conexión!! Verificar Usuario y Contraseña");
                return;
            }
            else
            {
                login.oSBO = oCompany;
            }
            
        }


    }
}
