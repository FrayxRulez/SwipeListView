using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Universal.UI.Xaml.Controls
{
    public class SwipeListView : ListView
    {
        public SwipeListView()
        {
            DefaultStyleKey = typeof(SwipeListView);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var container = new SwipeListViewItem();
            SetItemBinding(container, "ItemLeftContentTemplate", SwipeListViewItem.LeftContentTemplateProperty);
            SetItemBinding(container, "ItemLeftBackground", SwipeListViewItem.LeftBackgroundProperty);
            SetItemBinding(container, "ItemLeftBehavior", SwipeListViewItem.LeftBehaviorProperty);

            SetItemBinding(container, "ItemRightContentTemplate", SwipeListViewItem.RightContentTemplateProperty);
            SetItemBinding(container, "ItemRightBackground", SwipeListViewItem.RightBackgroundProperty);
            SetItemBinding(container, "ItemRightBehavior", SwipeListViewItem.RightBehaviorProperty);
            //container.LeftContentTemplate = ItemLeftContentTemplate;
            //container.LeftBackground = ItemLeftBackground;
            //container.LeftBehavior = ItemLeftBehavior;

            //container.RightContentTemplate = ItemRightContentTemplate;
            //container.RightBackground = ItemRightBackground;
            //container.RightBehavior = ItemRightBehavior;

            container.ItemSwipe += Item_ItemSwipe;

            return container;
        }

        private void SetItemBinding(SwipeListViewItem item, string origin, DependencyProperty destination)
        {
            var binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath(origin);

            BindingOperations.SetBinding(item, destination, binding);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var container = element as SwipeListViewItem;
            if (container != null)
            {
                container.ResetSwipe();
                //container.LeftContentTemplate = ItemLeftContentTemplate;
                //container.LeftBackground = ItemLeftBackground;
                //container.LeftBehavior = ItemLeftBehavior;

                //container.RightContentTemplate = ItemRightContentTemplate;
                //container.RightBackground = ItemRightBackground;
                //container.RightBehavior = ItemRightBehavior;

                //container.ItemSwipe -= Item_ItemSwipe;
                //container.ItemSwipe += Item_ItemSwipe;
            }

            base.PrepareContainerForItemOverride(element, item);
        }

        private void Item_ItemSwipe(object sender, ItemSwipeEventArgs e)
        {
            if (ItemSwipe != null)
                ItemSwipe(this, e);
        }

        public event ItemSwipeEventHandler ItemSwipe;

        #region ItemLeftContentTemplate
        public DataTemplate ItemLeftContentTemplate
        {
            get { return (DataTemplate)GetValue(ItemLeftContentTemplateProperty); }
            set { SetValue(ItemLeftContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemLeftContentTemplateProperty =
            DependencyProperty.Register("ItemLeftContentTemplate", typeof(DataTemplate), typeof(SwipeListView), new PropertyMetadata(null));
        #endregion

        #region ItemLeftBackground
        public Brush ItemLeftBackground
        {
            get { return (Brush)GetValue(ItemLeftBackgroundProperty); }
            set { SetValue(ItemLeftBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ItemLeftBackgroundProperty =
            DependencyProperty.Register("ItemLeftBackground", typeof(Brush), typeof(SwipeListView), new PropertyMetadata(null));
        #endregion

        #region ItemLeftBehavior
        public SwipeListBehavior ItemLeftBehavior
        {
            get { return (SwipeListBehavior)GetValue(ItemLeftBehaviorProperty); }
            set { SetValue(ItemLeftBehaviorProperty, value); }
        }

        public static readonly DependencyProperty ItemLeftBehaviorProperty =
            DependencyProperty.Register("ItemLeftBehavior", typeof(SwipeListBehavior), typeof(SwipeListView), new PropertyMetadata(SwipeListBehavior.Collapse));
        #endregion

        #region ItemRightContentTemplate
        public DataTemplate ItemRightContentTemplate
        {
            get { return (DataTemplate)GetValue(ItemRightContentTemplateProperty); }
            set { SetValue(ItemRightContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemRightContentTemplateProperty =
            DependencyProperty.Register("ItemRightContentTemplate", typeof(DataTemplate), typeof(SwipeListView), new PropertyMetadata(null));
        #endregion

        #region ItemRightBackground
        public Brush ItemRightBackground
        {
            get { return (Brush)GetValue(ItemRightBackgroundProperty); }
            set { SetValue(ItemRightBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ItemRightBackgroundProperty =
            DependencyProperty.Register("ItemRightBackground", typeof(Brush), typeof(SwipeListView), new PropertyMetadata(null));
        #endregion

        #region ItemRightBehavior
        public SwipeListBehavior ItemRightBehavior
        {
            get { return (SwipeListBehavior)GetValue(ItemRightBehaviorProperty); }
            set { SetValue(ItemRightBehaviorProperty, value); }
        }

        public static readonly DependencyProperty ItemRightBehaviorProperty =
            DependencyProperty.Register("ItemRightBehavior", typeof(SwipeListBehavior), typeof(SwipeListView), new PropertyMetadata(SwipeListBehavior.Expand));
        #endregion
    }
}
