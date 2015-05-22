using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universal.UI.Xaml.Controls
{
    /// <summary>
    /// Represents the method that will handle an <see cref="SwipeListView.ItemSwipe"/> event.
    /// </summary>
    /// <param name="sender">The object where the handler is attached.</param>
    /// <param name="e">Event data for the event.</param>
    public delegate void ItemSwipeEventHandler(object sender, ItemSwipeEventArgs e);
}
