using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATH_project
{
    public partial class QrForm : System.Windows.Forms.Form
    {
        public QrForm()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrcodedata = qrGenerator.CreateQrCode("https://docs.google.com/forms/d/e/1FAIpQLSfMSqtXilHj5f5spfbVQKqGuW4B4AF3PkJAAlgCXO8Xw6FKAA/viewform", QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrcodedata);
            Bitmap qrImage = qrcode.GetGraphic(6);
            QRPIC.Image = qrImage;
        }

    }
}
