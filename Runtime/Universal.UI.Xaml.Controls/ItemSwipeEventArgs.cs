using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universal.UI.Xaml.Controls
{
    public class ItemSwipeEventArgs : EventArgs
    {
        public object SwipedItem { get; private set; }

        public SwipeListDirection Direction { get; private set; }

        public ItemSwipeEventArgs(object item, SwipeListDirection direction)
        {
            SwipedItem = item;
            Direction = direction;
        }
    }
}
