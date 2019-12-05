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
}