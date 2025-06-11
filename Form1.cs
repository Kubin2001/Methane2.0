using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Methane2._0{
    public partial class Form1 : Form{
        private string configPath = null;
        private string vsProjPath = null;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        public Form1(){
            AllocConsole();
            InitializeComponent();
        }

        private void confInfo_Click(object sender, EventArgs e){
            FileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            fd.Filter = "Config Methane file type (*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK){
                string filePath = fd.FileName;
                loadInfo.Text += filePath;
                configPath = filePath;
            }
        }

        private void loadvsconfig_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            fd.Filter = "Pliki projektu C# (*.vcxproj)|*.vcxproj";
            if (fd.ShowDialog() == DialogResult.OK){
                string filePath = fd.FileName;
                vsInfo.Text += filePath;
                vsProjPath = filePath;
            }
        }

        private void startBtn_Click(object sender, EventArgs e) {
            if (configPath != null && vsProjPath != null) {
                InterPreter inter = new InterPreter(configPath ,vsProjPath);
                inter.Run();
            }
        }
    }
}
