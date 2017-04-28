﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universal.UI.Xaml.Controls
{
    public enum SwipeListBehavior
    {
        /// <summary>
        /// Triggered when swipe reaches 3/5 of the width of the item. Once triggered side menu is expanded, <see cref="SwipeListViewItem"/> state is not restored.
        /// </summary>
        Expand,

        /// <summary>
        /// Triggered when swipe reaches 2/5 of the width of the item. Once triggered side menu is collapsed and <see cref="SwipeListViewItem"/> state is restored.
        /// </summary>
        Collapse,

        /// <summary>
        /// Triggered when swipe reaches 2/5 of the width of the item. Once triggered side menu will continue to stay open, <see cref="SwipeListViewItem"/> state is not restored.
        /// </summary>
        Persist,

        /// <summary>
        /// Swipe is disabled.
        /// </summary>
        Disabled
    }
}
