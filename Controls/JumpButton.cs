using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace FakeDeleter.Controls
{
    internal class JumpButton : Button
    {
        public event Action? MouseOver;

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            MouseOver?.Invoke();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            MouseOver?.Invoke();
        }
    }
}
