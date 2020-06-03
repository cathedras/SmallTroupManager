using System.Windows;
using System.Windows.Controls;
using SmallTroupManager.Model;
namespace SmallTroupManager.Resources
{
    public class ListColTemp : DataTemplateSelector
    {
        public DataTemplate EditTemplate { get; set; }
        public DataTemplate ShowTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (((RepertoireItem)item).CurState == State.Edit)
            {
                return EditTemplate;
            }
            else if(((RepertoireItem)item).CurState == State.Show)
            {
                return ShowTemplate;
            }
            else
            {
                return base.SelectTemplate(item, container);
            }
           
        }
    }

    public class ListColTemplateManager
    {
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(ListColTemplateManager),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnLayoutManagerEnabledChanged)));


        private static void OnLayoutManagerEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ListView listView = dependencyObject as ListView;
            if (listView != null)
            {
                bool enabled = (bool)e.NewValue;
                if (enabled)
                {
                    
                }
            }
        }
    }
}