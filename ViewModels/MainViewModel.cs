﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using FakeDeleter.Models;

namespace FakeDeleter.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Changed(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ObservableCollection<string> Dirs { get; } = new();
        public ObservableCollection<string> Files { get; } = new();

        private int progress;
        public int Progress
        {
            get => progress;
            set
            {
                if (progress != value)
                {
                    progress = value;
                    Changed(nameof(Progress));
                }
            }
        }

        private int total;
        public int Total
        {
            get => total;
            set
            {
                if (total != value)
                {
                    total = value;
                    Changed(nameof(Total));
                }
            }
        }

        private string time = string.Empty;
        public string Time
        {
            get => time;
            set
            {
                if (time != value)
                {
                    time = value;
                    Changed(nameof(Time));
                }
            }
        }
        #endregion

        #region Commands
        public Command LookupDirectoryCommand { get; }
        private bool IsLookupDirectoryRunning = false;

        public Command RunTimerCommand { get; }
        #endregion

        #region Timers
        private DispatcherTimer dirsTimer;
        private DispatcherTimer filesTimer;
        private DispatcherTimer countDownTimer;
        #endregion

        public MainViewModel()
        {
            dirsTimer = new();
            filesTimer = new();
            countDownTimer = new();

            dirsTimer.Interval = TimeSpan.FromMilliseconds(20);
            filesTimer.Interval = TimeSpan.FromMilliseconds(20);
            countDownTimer.Interval = TimeSpan.FromSeconds(1);

            LookupDirectoryCommand = new(async (_) => await LookupDirectoryAsync(), (_) => !IsLookupDirectoryRunning);
            RunTimerCommand = new((_) => RunTimer(), (_) => true);

            LookupDirectoryCommand.Execute(null);
            RunTimerCommand.Execute(null);
        }

        private async Task LookupDirectoryAsync()
        {
            IsLookupDirectoryRunning = true;

            Dirs.Clear();
            Files.Clear();

            List<string> dirs = (await Disc.GetDirsAsync($"{Environment.ExpandEnvironmentVariables("%USERPROFILE%")}\\Videos")).ToList();
            List<string> files = new();

            Total = await Disc.TotalFilesCountAsync($"{Environment.ExpandEnvironmentVariables("%USERPROFILE%")}\\Videos");

            dirsTimer.Tick += async (sender, args) =>
            {
                if (dirs.Count > 0)
                {
                    Files.Clear();

                    //Progress = 0;

                    files = (await Disc.GetFilesAsync(dirs.First())).ToList();
                    //Total = files.Count;

                    Dirs.Add(dirs.First());
                    dirs.Remove(dirs.First());

                    dirsTimer.Stop();
                    filesTimer.Start();
                }
                else
                {
                    dirsTimer.Stop();
                    IsLookupDirectoryRunning = false;
                }
            };

            filesTimer.Tick += (sender, args) =>
            {
                if (files.Count > 0)
                {
                    //Progress = Files.Count;
                    Progress += 1;

                    Files.Add(files.First());
                    files.Remove(files.First());
                }
                else
                {
                    filesTimer.Stop();
                    dirsTimer.Start();
                }
            };

            dirsTimer.Start();
        }

        private void RunTimer()
        {
            DateTime time = new(2000, 1, 1, 0, 5, 0);

            Time = time.ToString("mm:ss");

            countDownTimer.Tick += (sender, args) =>
            {
                time = time.AddSeconds(-1);
                Time = time.ToString("mm:ss");
            };

            countDownTimer.Start();
        }
    }
}