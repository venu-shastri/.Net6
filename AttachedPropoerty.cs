namespace ConsoleApp1
{
    public class DependencyObject
    {
        Dictionary<string, object> localStore = new Dictionary<string, object>();
        public object GetValue(string propertyName) {
            return localStore[propertyName];

        }
        public void SetValue(string propertyName, object value) {
            localStore[propertyName] = value;
        }
        

    }
    public class UIElement:DependencyObject
    {
        public string Name { get; set; }
    }
    public class Button:UIElement
    {

    }
  public class Grid
    {
        public Grid()
        {
            this.Children = new();
        }
        //DP.......Attached
        public static string RowProperty;
        public static void SetRowProperty(DependencyObject child, int value)
        {
            child.SetValue(nameof(Grid.RowProperty), value);

        }
        public static int GetRowProperty(DependencyObject child)
        {
             return (int)child.GetValue(nameof(Grid.RowProperty));

        }
        public static string ColumnProperty;
        public static void SetColumnProperty(DependencyObject child, int value)
        {
            child.SetValue(nameof(Grid.ColumnProperty), value);

        }
        public static int GetColumnProperty(DependencyObject child)
        {
            return (int)child.GetValue(nameof(Grid.ColumnProperty));

        }
        public List<UIElement> Children { get; set; }
        public void Arrange()
        {
            foreach(var child in Children)
            {
              int row=  GetRowProperty(child);
                int column= GetColumnProperty(child);
                string name = child.Name;
                Console.WriteLine($"{name}:({row},{column})");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Grid table = new Grid();
            Button child1 = new Button();
            Grid.SetRowProperty(child1, 0);//buying the ticket
            Grid.SetColumnProperty(child1, 0);

            Button child2 = new Button();
            Grid.SetRowProperty(child2, 0);
            Grid.SetColumnProperty(child2, 1);

            table.Children.Add(child1);
            table.Children.Add(child2);

            table.Arrange();//Movie


        }
    }
}
