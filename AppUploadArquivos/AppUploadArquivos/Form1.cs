using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace AppUploadArquivos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.ShowDialog();

            localhost.WebServiceSoapClient service = new localhost.WebServiceSoapClient();

            System.IO.FileStream fileStream = System.IO.File.Open(openFile.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            byte[] arquivoByte = new byte[fileStream.Length];

            fileStream.Read(arquivoByte, 0, Convert.ToInt32(fileStream.Length));

            fileStream.Close();

            String resultado = service.UploadArquivo(openFile.SafeFileName, arquivoByte);

            MessageBox.Show(resultado);

            lblMsg1.Text = $"Nome do Arquivo: {openFile.SafeFileName}";
            lblMsg1.Visible = true;

            if (arquivoByte.Length <= 999)
            {
                lblMsg2.Text = $"Tamanho do Arquivo: {arquivoByte.Length}B";
                lblMsg2.Visible = true;
            }
            else if (arquivoByte.Length <= 999999)
            {
                lblMsg2.Text = $"Tamanho do Arquivo: {(arquivoByte.Length) / 1000}kB";
                lblMsg2.Visible = true;
            }
            else
            {
                lblMsg2.Text = $"Tamanho do Arquivo: {(arquivoByte.Length) / 1000000}MB";
                lblMsg2.Visible = true;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(openFile.SafeFileName);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            lblMsg3.Text = $"Código HASH: {hashString}";
            lblMsg3.Visible = true;
        }
    }
}
