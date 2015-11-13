using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfApplicationReport.View
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += MoveThumb_DragDelta;
        }

        private void MoveThumb_DragDelta(Object sender, DragDeltaEventArgs e)
        {
            var designerItem = DataContext as Control;
            if (designerItem == null) return;
            double left = Canvas.GetLeft(designerItem);
            double top = Canvas.GetTop(designerItem);
            Canvas.SetLeft(designerItem, left + e.HorizontalChange);
            Canvas.SetTop(designerItem, top + e.VerticalChange);
        }
    }
}