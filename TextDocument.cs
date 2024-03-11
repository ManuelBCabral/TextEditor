using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Shapes;
using System.Windows;


namespace ManuelBelausteguiAssignmentM8
{
    internal class TextDocument
    {
        public string Text { get; set; }
        private string filepath;

        public void OpenDoc()
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filepath = openFile.FileName;
                Text = File.ReadAllText(filepath);
            }
        }
        public void SaveText()
        {
            try
            {
                File.WriteAllText(filepath, Text);
            }
            catch(IOException)
            {
                string messageBoxText = "IO Exception";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch(Exception NullException) {
                string messageBoxText = "Make sure you create a new document or edit a valid document";
                string caption = "Null Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        public void SaveTextAs(String path)
        {
            try
            {
                filepath = path;
                File.WriteAllText(filepath, Text);
            }
            catch (IOException)
            {
                string messageBoxText = "IO Exception";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        public void loadText(String filePath)
        {
            try
            {
                filepath = filePath;
                Text = File.ReadAllText(filePath);
            }
            catch (IOException)
            {
                string messageBoxText = "IO Exception";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch ( Exception NullException)
            {
                string messageBoxText = "Make sure you create a new document or edit a valid document";
                string caption = "Null Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
            /*var fStreamText = new FileStream(filePath, FileMode.Open);
            var binFormatterEmployee = new BinaryFormatter();
            var TextDoc = new TextDocument();
            TextDoc = (TextDocument)binFormatterEmployee.Deserialize(fStreamText);
            fStreamText.Close();
            Text = TextDoc.Text;*/
        }
        public void newDocument()
        {
            Text = string.Empty;
            filepath = "";
        }


    }
}
