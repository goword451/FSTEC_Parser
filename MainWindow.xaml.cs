using System;
using OfficeOpenXml;
using System.Windows;
using System.Windows.Controls;
using FSTEC.Properties;
using FSTEC.View;

namespace FSTEC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            InitializeMainWindowSettings();
        }
        private void InitializeMainWindowSettings()
        {
            this.Width = Settings.Default.Width;
            this.Height = Settings.Default.Height;
            this.WindowState = Settings.Default.WindowState;
        }
        private void DataGridMain_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {

                case "DisplayedId":
                    e.Column.Header = "Идентификатор УБИ";
                    e.Column.Width = DataGridLength.SizeToHeader;
                    break;
                case "Name":
                    e.Column.Header = "Наименование УБИ";
                    e.Column.Width = 650;
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Settings.Default.Height = this.Height;
            Settings.Default.Width = this.Width;
            Settings.Default.Save();
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            Settings.Default.WindowState = this.WindowState;
            Settings.Default.Save();
        }
        
    }
}