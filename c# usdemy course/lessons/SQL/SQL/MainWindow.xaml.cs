using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace SQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //very big function
            string connectionString = ConfigurationManager.ConnectionStrings["SQL.Properties.settings.DANTORIALSDBConnectionString"].ConnectionString;
            /*

1. `ConfigurationManager`: This is a class provided by the .
            NET Framework that allows you to access configuration settings in your application.

2. `ConnectionStrings`: This is a property of the `ConfigurationManager` class, specifically used for accessing connection 
            strings defined in the configuration file of your application.

3. `"SQL.Properties.settings.DANTORIALSDBConnectionString"`: This is the name of the connection string you want to retrieve. 
            Connection strings are often stored in the configuration file (usually `app.config` or `web.config` for desktop or web applications respectively). In this case, it seems like the connection string is stored under the name `"SQL.Properties.settings.DANTORIALSDBConnectionString"`.

4. `.ConnectionString`: This part accesses the actual connection string value associated with the specified name.
            The `ConnectionString` property returns the connection string as a string.

5. `string connectionString = ...`: Finally, the retrieved connection string is assigned to a variable named `
            connectionString`, which you can use in your code to establish a connection to the SQL Server database.

In summary, this line of code retrieves the connection string named "SQL.Properties.settings.DANTORIALSDBConnectionString" 
            from the configuration settings of your application and assigns it to the variable `connectionString`. This connection string is typically used to connect to a SQL Server database in a WPF (Windows Presentation Foundation) application.*/


        }
    }
}
