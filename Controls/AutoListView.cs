using System.Collections.Specialized;
using System.Windows.Controls;

namespace FakeDeleter.Controls
{
    internal class AutoListView : ListView
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (Items != null && Items.Count > 0)
            {
                ScrollIntoView(Items[^1]);
            }
        }
    }
}
