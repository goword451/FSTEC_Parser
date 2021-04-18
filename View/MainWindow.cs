using FSTEC.Objects;
using FSTEC.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FSTEC.View
{
    class MainWindow : ViewModelBase
    {
        private static ObservableCollection<DisplayedDanger> dangers = DisplayedDangers.DisplayedDangersCollection;
        private static List<DisplayedDanger> pageDangers;
        private static bool fileIsBroken = false;
        private static int page = 1;
        public int Page
        {
            get
            {
                return page;
            }
            set
            {
                page = value;
                OnPropertyChanged("page");
            }
        }
        public List<DisplayedDanger> PageDangers
        {
            get
            {
                if (pageDangers == null)
                    return DisplayedDangersByPage();
                return pageDangers;
            }
            set
            {
                pageDangers = value;
                OnPropertyChanged("pageDangers");
            }
        }

        private List<DisplayedDanger> DisplayedDangersByPage()
        {
            int dangersOnPage = Settings.Default.DangersOnPage;
            return new List<DisplayedDanger>(dangers.Skip((page - 1) * dangersOnPage)
                                                .Take(dangersOnPage));
        }

        private static void Initialize()
        {
            if (!FileExcel.CheckExist())
            {
                string message = "Не удалось найти файл базы данных\n" +
                                 "Произвести загрузку данных?";
                MessageBoxResult boxResult = MessageBox.Show(message, "Не удалось найти файл", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                if (boxResult == MessageBoxResult.Yes)
                    TryToDownloadFile();
            }
            else
            {
                try
                {
                    dangers = DisplayedDangers.DisplayDangersFromDangers(FileExcel.ParseFileExcel());
                    fileIsBroken = false;
                }
                catch
                {
                    MessageBox.Show("\nЛокальный файл базы данных повреждён.\nУдалите файл локальной базы данных local в папке lib программы и выполните загрузку нового файла в пункте меню «Правка», команда «Обновить файл»", "Не удалось считать файл!", MessageBoxButton.OK, MessageBoxImage.Error);
                    fileIsBroken = true;
                }
            }
        }
        static MainWindow()
        {
            Initialize();
        }

        #region FirstPageCommand

        RelayCommand firstPage;
        public ICommand FirstPage
        {
            get
            {
                if (firstPage == null)
                    firstPage = new RelayCommand(ExecuteFirstPageCommand, CanExecuteFirstPageCommand);
                return firstPage;
            }
        }

        public void ExecuteFirstPageCommand(object parameter)
        {
            Page = 1;
            PageDangers = DisplayedDangersByPage();
        }

        public bool CanExecuteFirstPageCommand(object parameter)
        {
            if (Page == 1)
                return false;
            return true;
        }
        #endregion

        #region LastPageCommand

        RelayCommand lastPage;
        public ICommand LastPage
        {
            get
            {
                if (lastPage == null)
                    lastPage = new RelayCommand(ExecuteLastPageCommand, CanExecuteLastPageCommand);
                return lastPage;
            }
        }

        public void ExecuteLastPageCommand(object parameter)
        {
            Page = dangers.Count / Settings.Default.DangersOnPage + 1;
            PageDangers = DisplayedDangersByPage();
        }

        public bool CanExecuteLastPageCommand(object parameter)
        {
            if (Page == dangers.Count / Settings.Default.DangersOnPage + 1)
                return false;
            return true;
        }
        #endregion

        #region SaveCommand

        RelayCommand saveFileCommand;
        public ICommand SaveFile
        {
            get
            {
                if (saveFileCommand == null)
                    saveFileCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
                return saveFileCommand;
            }
        }

        public void ExecuteSaveCommand(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "thrlist_saved.xlsx";
            saveFileDialog.Filter = "Книга Excel (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    if (!FileExcel.SaveFile(saveFileDialog.FileName))
                        MessageBox.Show("Не удалось сохранить файл", "Не удалось сохранить файл", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    else
                        MessageBox.Show("Файл был успешно сохранён!", "Файл сохранён", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить файл", "Не удалось сохранить файл", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
        }

        public bool CanExecuteSaveCommand(object parameter)
        {
            return FileExcel.CheckExist() && !fileIsBroken;
        }
        #endregion

        #region ExitCommand
        RelayCommand exitCommand;
        public ICommand Exit
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new RelayCommand(ExecuteExitCommand, CanExecuteExitCommand);
                return exitCommand;
            }
        }

        public void ExecuteExitCommand(object parameter)
        {
            Environment.Exit(0);
        }

        public bool CanExecuteExitCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region UpdateFileCommand

        RelayCommand updateFileCommand;
        public ICommand UpdateFile
        {
            get
            {
                if (updateFileCommand == null)
                    updateFileCommand = new RelayCommand(ExecuteUpdateFileCommand, CanExecuteUpdateFileCommand);
                return updateFileCommand;
            }
        }

        public void ExecuteUpdateFileCommand(object parameter)
        {
            if (FileExcel.CheckExist() && fileIsBroken == false)
            {
                var lastDangers = FileExcel.Dangers;
                if (TryToDownloadFile())
                {
                    new UpdateViewModel(Update.GetUpdates(lastDangers, FileExcel.Dangers));
                    Page = 1;
                    PageDangers = DisplayedDangersByPage();
                }
            }
            else
            {
                Initialize();
                PageDangers = DisplayedDangersByPage();
            }
        }

        public bool CanExecuteUpdateFileCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region NextPageCommand

        RelayCommand nextPageCommand;
        public ICommand NextPageCommand
        {
            get
            {
                if (nextPageCommand == null)
                    nextPageCommand = new RelayCommand(ExecuteNextPageCommand, CanExecuteNextPageCommand);
                return nextPageCommand;
            }
        }

        public void ExecuteNextPageCommand(object parameter)
        {
            Page++;
            PageDangers = DisplayedDangersByPage();
        }

        public bool CanExecuteNextPageCommand(object parameter)
        {
            if (Page == dangers.Count / Settings.Default.DangersOnPage + 1)
                return false;
            return true;
        }
        #endregion

        #region PreviousPageCommand

        RelayCommand previousPageCommand;
        public ICommand PreviousPageCommand
        {
            get
            {
                if (previousPageCommand == null)
                    previousPageCommand = new RelayCommand(ExecutePreviousPageCommand, CanExecutePreviousPageCommand);
                return previousPageCommand;
            }
        }

        public void ExecutePreviousPageCommand(object parameter)
        {
            Page--;
            PageDangers = DisplayedDangersByPage();
        }

        public bool CanExecutePreviousPageCommand(object parameter)
        {
            if (Page == 1)
                return false;
            return true;
        }
        #endregion

        #region DownloadFile
        private static bool TryToDownloadFile()
        {
            if (Network.CheckInternetConnection())
            {
                try
                {
                    Network.DownloadFile();
                    MessageBox.Show("Файл успешно загружен!", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
                    dangers = DisplayedDangers.DisplayDangersFromDangers(FileExcel.ParseFileExcel());
                    page = 1;
                    fileIsBroken = false;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n", "Ошибка при загрузке файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                string message = "Отсутствует подключение к сети Интернет или указан неверный URL адрес.\n" +
                                 "Проверьте ваше интернет соединение и корректность URL адреса, затем повторите загрузку";
                MessageBox.Show(message, "Ошибка загрузки файла", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return false;
            }
        }
        #endregion
    }
}
