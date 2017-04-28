﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if SILVERLIGHT
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.Foundation;
#endif

namespace Universal.UI.Xaml.Controls
{
#if SILVERLIGHT
    public class SwipeListViewItem : ListBoxItem
#else
    public class SwipeListViewItem : ListViewItem
#endif
    {
        private TranslateTransform ContentDragTransform;
        private TranslateTransform LeftTransform;
        private TranslateTransform RightTransform;

        private Border LeftContainer;
        private Border RightContainer;

        private Grid DragBackground;
        private RectangleGeometry DragClip;
        private TranslateTransform DragClipTransform;
        private Border DragContainer;

        private SwipeListView _parent;

        public SwipeListViewItem()
        {
            DefaultStyleKey = typeof(SwipeListViewItem);
        }

        internal SwipeListViewItem(SwipeListView parent)
            : this()
        {
            _parent = parent;
        }

#if SILVERLIGHT
        public
#else
        protected
#endif
            override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentDragTransform = (TranslateTransform)GetTemplateChild("ContentDragTransform");
            LeftTransform = (TranslateTransform)GetTemplateChild("LeftTransform");
            RightTransform = (TranslateTransform)GetTemplateChild("RightTransform");

            LeftContainer = (Border)GetTemplateChild("LeftContainer");
            RightContainer = (Border)GetTemplateChild("RightContainer");

            DragBackground = (Grid)GetTemplateChild("DragBackground");
            DragClip = (RectangleGeometry)GetTemplateChild("DragClip");
            DragClipTransform = (TranslateTransform)GetTemplateChild("DragClipTransform");
            DragContainer = (Border)GetTemplateChild("DragContainer");
        }

        /// <summary>
        /// Resets the <see cref="SwipeListViewItem"/> swipe state.
        /// </summary>
        public void ResetSwipe()
        {
            if (DragBackground != null)
            {
                DragBackground.Background = null;
                DragClip.Rect = new Rect(0, 0, 0, 0);
                DragClipTransform.X = 0;

                ContentDragTransform.X = 0;
                LeftTransform.X = -(LeftContainer.ActualWidth + 20);
                RightTransform.X = (RightContainer.ActualWidth + 20);
            }
        }

        private SwipeListDirection _direction = SwipeListDirection.None;

#if SILVERLIGHT
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
#else
        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
#endif
        {
#if SILVERLIGHT
            var delta = e.DeltaManipulation.Translation;
            var cumulative = e.CumulativeManipulation.Translation;
#else
            if (e.PointerDeviceType != Windows.Devices.Input.PointerDeviceType.Touch || 
                _parent?.SelectionMode == ListViewSelectionMode.Multiple ||
                _parent?.SelectionMode == ListViewSelectionMode.Extended)
            {
                e.Complete();
                return;
            }

            var delta = e.Delta.Translation;
            var cumulative = e.Cumulative.Translation;
#endif

            var target = ((ActualWidth / 5) * 1);

            if (_direction == SwipeListDirection.None)
            {
                _direction = delta.X > 0 
                    ? SwipeListDirection.Left 
                    : SwipeListDirection.Right;

                DragBackground.Background = _direction == SwipeListDirection.Left 
                    ? LeftBackground 
                    : RightBackground;

                LeftTransform.X = -(LeftContainer.ActualWidth + 20);
                RightTransform.X = (RightContainer.ActualWidth + 20);

                DragClip.Rect = new Rect(_direction == SwipeListDirection.Left ? -ActualWidth : ActualWidth, 0, ActualWidth, ActualHeight);

                if (_direction == SwipeListDirection.Left && LeftBehavior != SwipeListBehavior.Disabled)
                {
                    DragBackground.Background = LeftBackground;

                    LeftContainer.Visibility = Visibility.Visible;
                    RightContainer.Visibility = Visibility.Collapsed;
                }
                else if (_direction == SwipeListDirection.Right && RightBehavior != SwipeListBehavior.Disabled)
                {
                    DragBackground.Background = RightBackground;

                    LeftContainer.Visibility = Visibility.Collapsed;
                    RightContainer.Visibility = Visibility.Visible;
                }
                else
                {
                    e.Complete();
                    return;
                }
            }

            if (_direction == SwipeListDirection.Left)
            {
                var area1 = LeftBehavior == SwipeListBehavior.Collapse ? 1.5 : 2.5;
                var area2 = LeftBehavior == SwipeListBehavior.Collapse ? 2 : 3;

                ContentDragTransform.X = Math.Max(0, Math.Min(cumulative.X, ActualWidth));
                DragClipTransform.X = Math.Max(0, Math.Min(cumulative.X, ActualWidth));

                if (ContentDragTransform.X < target * area1)
                {
                    LeftTransform.X += (delta.X / 1.5);
                }
                else if (ContentDragTransform.X >= target * area1 && ContentDragTransform.X < target * area2)
                {
                    LeftTransform.X += (delta.X * 2.5);
                }
                else
                {
                    LeftTransform.X = Math.Max(0, Math.Min(cumulative.X, ActualWidth)) - LeftContainer.ActualWidth;
                }

                if (ContentDragTransform.X == 0 && delta.X < 0)
                {
                    _direction = SwipeListDirection.None;
                }
            }
            else if (_direction == SwipeListDirection.Right)
            {
                var area1 = RightBehavior == SwipeListBehavior.Collapse ? 1.5 : 2.5;
                var area2 = RightBehavior == SwipeListBehavior.Collapse ? 2 : 3;

                ContentDragTransform.X = Math.Max(-ActualWidth, Math.Min(cumulative.X, 0));
                DragClipTransform.X = Math.Max(-ActualWidth, Math.Min(cumulative.X, 0));

                if (ContentDragTransform.X > -(target * area1))
                {
                    RightTransform.X += (delta.X / 1.5);
                }
                else if (ContentDragTransform.X <= -(target * area1) && ContentDragTransform.X > -(target * area2))
                {
                    RightTransform.X += (delta.X * 2.5);
                }
                else
                {
                    RightTransform.X = Math.Max(-ActualWidth, Math.Min(cumulative.X, 0)) + RightContainer.ActualWidth;
                }

                if (ContentDragTransform.X == 0 && delta.X > 0)
                {
                    _direction = SwipeListDirection.None;
                }
            }

            //e.Handled = true;
            //base.OnManipulationDelta(e);
        }

#if SILVERLIGHT
        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
#else
        protected override void OnManipulationCompleted(ManipulationCompletedRoutedEventArgs e)
#endif
        {
            var target = (ActualWidth / 5) * 2;
            if ((_direction == SwipeListDirection.Left && LeftBehavior == SwipeListBehavior.Expand) ||
                (_direction == SwipeListDirection.Right && RightBehavior == SwipeListBehavior.Expand))
            {
                target = (ActualWidth / 5) * 3;
            }

            Storyboard currentAnim;

            if (_direction == SwipeListDirection.Left && ContentDragTransform.X >= target)
            {
                if (LeftBehavior == SwipeListBehavior.Collapse)
                    currentAnim = CollapseAnimation(SwipeListDirection.Left, true);
                else if (LeftBehavior == SwipeListBehavior.Persist)
                    currentAnim = PersistAnimation(SwipeListDirection.Left, target);
                else
                    currentAnim = ExpandAnimation(SwipeListDirection.Left);
            }
            else if (_direction == SwipeListDirection.Right && ContentDragTransform.X <= -target)
            {
                if (RightBehavior == SwipeListBehavior.Collapse)
                    currentAnim = CollapseAnimation(SwipeListDirection.Right, true);
                else if (RightBehavior == SwipeListBehavior.Persist)
                    currentAnim = PersistAnimation(SwipeListDirection.Right, target);
                else
                    currentAnim = ExpandAnimation(SwipeListDirection.Right);
            }
            else
            {
                currentAnim = CollapseAnimation(SwipeListDirection.Left, false);
            }

            currentAnim.Begin();
            _direction = SwipeListDirection.None;

            //e.Handled = true;
            //base.OnManipulationCompleted(e);
        }

        private Storyboard CollapseAnimation(SwipeListDirection direction, bool raise)
        {
            var animDrag = CreateDouble(0, 300, ContentDragTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
            var animClip = CreateDouble(0, 300, DragClipTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
            var animLeft = CreateDouble(-(LeftContainer.ActualWidth + 20), 300, LeftTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
            var animRight = CreateDouble((RightContainer.ActualWidth + 20), 300, RightTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });

            var currentAnim = new Storyboard();
            currentAnim.Children.Add(animDrag);
            currentAnim.Children.Add(animClip);
            currentAnim.Children.Add(animLeft);
            currentAnim.Children.Add(animRight);

            currentAnim.Completed += (s, args) =>
            {
                DragBackground.Background = null;

                ContentDragTransform.X = 0;
                LeftTransform.X = -(LeftContainer.ActualWidth + 20);
                RightTransform.X = (RightContainer.ActualWidth + 20);

                Grid.SetColumn(DragBackground, 1);
                Grid.SetColumnSpan(DragBackground, 1);

            };

            if (raise)
            {
                if (ItemSwipe != null)
                    ItemSwipe(this, new ItemSwipeEventArgs(Content, direction));

                if (_parent != null)
                    _parent.RaiseItemSwipe(new ItemSwipeEventArgs(Content, direction));
            }

            return currentAnim;
        }

        private Storyboard ExpandAnimation(SwipeListDirection direction)
        {
            var currentAnim = new Storyboard();
            if (direction == SwipeListDirection.Left)
            {
                var animDrag = CreateDouble(ActualWidth + 100, 300, ContentDragTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animClip = CreateDouble(ActualWidth, 300, DragClipTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animLeft = CreateDouble(ActualWidth + 100, 300, LeftTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseIn });
                var animRight = CreateDouble(ActualWidth + 100, 300, RightTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseIn });

                currentAnim.Children.Add(animDrag);
                currentAnim.Children.Add(animClip);
                currentAnim.Children.Add(animLeft);
                currentAnim.Children.Add(animRight);
            }
            else if (direction == SwipeListDirection.Right)
            {
                var animDrag = CreateDouble(-ActualWidth - 100, 300, ContentDragTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animClip = CreateDouble(-ActualWidth, 300, DragClipTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animLeft = CreateDouble(-ActualWidth - 100, 300, LeftTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseIn });
                var animRight = CreateDouble(-ActualWidth - 100, 300, RightTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseIn });

                currentAnim.Children.Add(animDrag);
                currentAnim.Children.Add(animClip);
                currentAnim.Children.Add(animLeft);
                currentAnim.Children.Add(animRight);
            }

            currentAnim.Completed += (s, args) =>
            {
                if (ItemSwipe != null)
                    ItemSwipe(this, new ItemSwipeEventArgs(Content, direction));

                if (_parent != null)
                    _parent.RaiseItemSwipe(new ItemSwipeEventArgs(Content, direction));
            };

            return currentAnim;
        }



        private Storyboard PersistAnimation(SwipeListDirection direction, double target)
        {
            var currentAnim = new Storyboard();
            if (direction == SwipeListDirection.Left)
            {
                var animDrag = CreateDouble(target, 300, ContentDragTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animClip = CreateDouble(target, 300, DragClipTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animLeft = CreateDouble(-LeftContainer.ActualWidth + target, 300, LeftTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animRight = CreateDouble((RightContainer.ActualWidth + target), 300, RightTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });

                currentAnim.Children.Add(animDrag);
                currentAnim.Children.Add(animClip);
                currentAnim.Children.Add(animLeft);
                currentAnim.Children.Add(animRight);
            }
            else if (direction == SwipeListDirection.Right)
            {
                var animDrag = CreateDouble(-target, 300, ContentDragTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animClip = CreateDouble(-target, 300, DragClipTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animLeft = CreateDouble(-LeftContainer.ActualWidth - target, 300, LeftTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });
                var animRight = CreateDouble((RightContainer.ActualWidth - target), 300, RightTransform, "TranslateTransform.X", new ExponentialEase { EasingMode = EasingMode.EaseOut });

                currentAnim.Children.Add(animDrag);
                currentAnim.Children.Add(animClip);
                currentAnim.Children.Add(animLeft);
                currentAnim.Children.Add(animRight);
            }

            currentAnim.Completed += (s, args) =>
            {
                if (ItemSwipe != null)
                    ItemSwipe(this, new ItemSwipeEventArgs(Content, direction));

                if (_parent != null)
                    _parent.RaiseItemSwipe(new ItemSwipeEventArgs(Content, direction));
            };

            return currentAnim;
        }

        private DoubleAnimation CreateDouble(double to, int duration, DependencyObject target, string path, EasingFunctionBase easing)
        {
            var anim = new DoubleAnimation();
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
            anim.EasingFunction = easing;

            Storyboard.SetTarget(anim, target);
#if SILVERLIGHT
            Storyboard.SetTargetProperty(anim, new PropertyPath(path));
#else
            Storyboard.SetTargetProperty(anim, path);
#endif

            return anim;
        }


        public void CollapseSwipeContent()
        {
            var animation = CollapseAnimation(SwipeListDirection.Left, false);
            animation.Begin();
        }

        /// <summary>
        /// Occurs when the item is swiped from left or right.
        /// </summary>
        public event ItemSwipeEventHandler ItemSwipe;

        #region LeftContentTemplate
        public DataTemplate LeftContentTemplate
        {
            get { return (DataTemplate)GetValue(LeftContentTemplateProperty); }
            set { SetValue(LeftContentTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the LeftContentTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty LeftContentTemplateProperty =
            DependencyProperty.Register("LeftContentTemplate", typeof(DataTemplate), typeof(SwipeListViewItem), new PropertyMetadata(null));
        #endregion

        #region LeftBackground
        public Brush LeftBackground
        {
            get { return (Brush)GetValue(LeftBackgroundProperty); }
            set { SetValue(LeftBackgroundProperty, value); }
        }

        /// <summary>
        /// Identifies the LeftBackground dependency property.
        /// </summary>
        public static readonly DependencyProperty LeftBackgroundProperty =
            DependencyProperty.Register("LeftBackground", typeof(Brush), typeof(SwipeListViewItem), new PropertyMetadata(null));
        #endregion

        #region LeftBehavior
        public SwipeListBehavior LeftBehavior
        {
            get { return (SwipeListBehavior)GetValue(LeftBehaviorProperty); }
            set { SetValue(LeftBehaviorProperty, value); }
        }

        /// <summary>
        /// Identifies the LeftBehavior dependency property.
        /// </summary>
        public static readonly DependencyProperty LeftBehaviorProperty =
            DependencyProperty.Register("LeftBehavior", typeof(SwipeListBehavior), typeof(SwipeListViewItem), new PropertyMetadata(SwipeListBehavior.Collapse));
        #endregion

        #region RightContentTemplate
        public DataTemplate RightContentTemplate
        {
            get { return (DataTemplate)GetValue(RightContentTemplateProperty); }
            set { SetValue(RightContentTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the RightContentTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty RightContentTemplateProperty =
            DependencyProperty.Register("RightContentTemplate", typeof(DataTemplate), typeof(SwipeListViewItem), new PropertyMetadata(null));
        #endregion

        #region RightBackground
        public Brush RightBackground
        {
            get { return (Brush)GetValue(RightBackgroundProperty); }
            set { SetValue(RightBackgroundProperty, value); }
        }

        /// <summary>
        /// Identifies the RightBackground dependency property.
        /// </summary>
        public static readonly DependencyProperty RightBackgroundProperty =
            DependencyProperty.Register("RightBackground", typeof(Brush), typeof(SwipeListViewItem), new PropertyMetadata(null));
        #endregion

        #region RightBehavior
        public SwipeListBehavior RightBehavior
        {
            get { return (SwipeListBehavior)GetValue(RightBehaviorProperty); }
            set { SetValue(RightBehaviorProperty, value); }
        }

        /// <summary>
        /// Identifies the RightBehavior dependency property.
        /// </summary>
        public static readonly DependencyProperty RightBehaviorProperty =
            DependencyProperty.Register("RightBehavior", typeof(SwipeListBehavior), typeof(SwipeListViewItem), new PropertyMetadata(SwipeListBehavior.Expand));
        #endregion
    }
}
