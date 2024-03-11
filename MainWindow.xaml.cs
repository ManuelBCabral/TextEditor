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
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ManuelBelausteguiAssignmentM8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String currentFilePath = "new.text";
        Boolean modified = false;
        TextDocument textDoc;

        public MainWindow()
        {
            InitializeComponent();
            textDoc = new TextDocument();
            saveas_menuItem.IsEnabled = false;
        }

        private void new_menuItem_Click(object sender, RoutedEventArgs e)
        {
            if (handleSaveRequest())
            {
                textDoc.newDocument();
                Text_textBox.Text= textDoc.Text;
                currentFilePath = "";
                setModifiedState();
            }
        }

        private void open_menuItem_Click(object sender, RoutedEventArgs e)
        {
            if (handleSaveRequest())
            {
                startLoad();
            }
        }

        private void save_menuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if (currentFilePath=="") {
                saveas_menuItem_Click(sender, e);
                
            }
            else
            {
                textDoc.Text = Text_textBox.Text;
                textDoc.SaveText();
                unsetModifiedState();
                save_menuItem.IsEnabled = false;
            }
        }

        private void saveas_menuItem_Click(object sender, RoutedEventArgs e)
        {
            textDoc.Text = Text_textBox.Text;
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textDoc.SaveTextAs(saveFileDialog.FileName);
                currentFilePath= saveFileDialog.FileName;
                unsetModifiedState() ;
                saveas_menuItem.IsEnabled = false;
            }
        }
        private Boolean handleSaveRequest()
        {
            if (!modified) return true;
            if (currentFilePath == "new.text") return true;
            string messageBoxText = "The file needs to be saved.\nDo you want to save?";
            string caption = "Text Editor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            // Process message box results
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes: // Save document and exit
                    save_menuItem_Click(this, null);
                    return true;

                case MessageBoxResult.No: // Exit without saving
                    return true;

                case MessageBoxResult.Cancel: // Don't exit
                    return false;
            }
            return false;
        }
        private void startLoad()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textDoc.loadText(openFileDialog.FileName);
                Text_textBox.Text = textDoc.Text;
                currentFilePath = openFileDialog.FileName;
                unsetModifiedState();
            }
        }
    /*    private void startSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (currentFilePath != "")
            {
                saveFileDialog.FileName = System.IO.Path.GetFileName(currentFilePath);
                saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(currentFilePath);
            }

            saveFileDialog.DefaultExt = ".txt"; // Default file extension
            saveFileDialog.Filter = "Text files (*.txt)|*.txt"; // Filter files by extension
            // Show save file dialog box
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textDoc.SaveText(saveFileDialog.FileName, Text_textBox.Text);
                currentFilePath = saveFileDialog.FileName;
                unsetModifiedState();
            }
        }*/
        private void setModifiedState()
        {
            modified = true;
            save_menuItem.IsEnabled = true;
        }

        private void unsetModifiedState()
        {
            modified = false;
            save_menuItem.IsEnabled = false;
        }

        private void exit_menuItem_Click(object sender, RoutedEventArgs e)
        {
            if (handleSaveRequest())
            {
                Close();
            }
        }

        private void about_menuItem_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "This text editor is designed and implemented by Manuel Belaustegui-Cabral for Assignment M8 for C#/.NET development\n";
            string caption = "About";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            // Display message box
            System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            // No need to process message box results since OK is the only option.
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            setModifiedState();
            save_menuItem.IsEnabled = true;
            if (currentFilePath == "")
            {
                saveas_menuItem.IsEnabled = true;
            }
        }
    }
}
