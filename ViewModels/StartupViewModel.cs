using System;

namespace FakeDeleter.ViewModels
{
    internal class StartupViewModel
    {
        public event Action? ButtonPressed;

        #region Commands
        public Command SwitchViewCommand { get; }
        #endregion

        public StartupViewModel()
        {
            SwitchViewCommand = new((_) => ButtonPressed?.Invoke(), (_) => true);
        }
    }
}
