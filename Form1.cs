﻿using System;
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
        private string configDir = null;
        private string vsProjDir = null;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        public Form1(){
            AllocConsole();
            InitializeComponent();
            startBtn.Enabled = false;
            loadInfo.BackColor = Color.Red;
            vsInfo.BackColor = Color.Red;
        }

        private void confInfo_Click(object sender, EventArgs e){
            FileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            fd.Filter = "Config Methane file type (*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK){
                string filePath = fd.FileName;
                loadInfo.BackColor = Color.Green;
                configPath = filePath;

                configDir = Path.GetDirectoryName(configPath);
            }
        }

        private void loadvsconfig_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            fd.Filter = "Pliki projektu C# (*.vcxproj)|*.vcxproj";
            if (fd.ShowDialog() == DialogResult.OK){
                string filePath = fd.FileName;
                vsProjPath = filePath;
                vsProjDir = Path.GetDirectoryName(vsProjPath);

                vsInfo.BackColor = Color.Green;
            }
            if(configPath != null && vsProjPath != null) {
                startBtn.Enabled=true;
            }
        }

        private void startBtn_Click(object sender, EventArgs e) {
            if (configPath != null && vsProjPath != null) {
                InterPreter inter = new InterPreter(configPath ,vsProjPath,configDir,vsProjDir);
                inter.Run();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e) {
            configPath = null;
            vsProjPath = null;
            loadInfo.BackColor = Color.Red;
            vsInfo.BackColor = Color.Red;
            startBtn.Enabled = false;
        }
    }
}
