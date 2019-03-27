

using System.Reflection;
using System.Threading.Tasks;
using DevExpress.XtraBars.Alerter;

namespace DownTube
{
    using System;
    using System.Windows.Forms;
    using System.IO;

    using VideoLibrary;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var service = Client.For(YouTube.Default);
            var video = service.GetVideo(textBox1.Text);
            txtName.Text = video.Title;
            txtRes.Text = video.Resolution.ToString();
            
        }

        private async void DownloadVideo(string uri)
        {
           
            marqueeProgressBarControl1.Visible = true;
            
            var service = Client.For(YouTube.Default);
            var video = service.GetVideo(uri);

            string path = Path.Combine("D:\\Videos", video.FullName);
            byte[] videoBytes = await video.GetBytesAsync();
            File.WriteAllBytes(path, videoBytes);
            Cursor.Current = Cursors.Default;
            //toastNotificationsManager1.ShowNotification("a43f4cca-6605-4096-9f89-2801ec1fa96f");
            AlertInfo info = new AlertInfo("Video Descargado", $"El video \"{txtName.Text}\" ha sido descargado ");
            alertControl1.Show(this,info);
            marqueeProgressBarControl1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DownloadVideo(textBox1.Text);
        }

       
    }
}
