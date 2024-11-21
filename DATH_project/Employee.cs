using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATH_project
{
    public enum pos
    {
        Admin, Manager,Employee
    }
    [Serializable]
    public class Employee
    {
        private string id;
        private string name;
        private pos position;
        private string key;
        public string Id{
            get{ return id; } set{ id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public pos Position
        {
            get { return position; }
            set { position = value; }
        }
        public string Key
        {
            get { return key; }
            set{ key = value; }
        }
        public Employee() { }
        public Employee(string id, string name, pos position,string key)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.key = key;
        }
    }
    public class EFunc
    {   
        public void writeFile(string fileName,List<Employee> temp)
        {
            try
            {
                FileStream f = new FileStream(fileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, temp);
                f.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Không thể lưu dữ liệu. Lỗi: {e}", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

        }
        public void readFile(string fileName, out List<Employee> emp)
        {
            FileStream f = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                emp = bf.Deserialize(f) as List<Employee>;
            }
            catch (Exception)
            {
                emp = null;
                MessageBox.Show("Chưa có dữ liệu!!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            f.Close();
        }
    }
}
