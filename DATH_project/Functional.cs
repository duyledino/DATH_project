using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;

namespace DATH_project
{
    public class Functional
    {
        public order find(List<order> orders,String findID)
        {
            foreach(order o in orders)
            {
                if(o.OrderId == findID) return o;
            }
            return null;
        }
        public bool writeFile(string fileName,List<order> ds)
        {
            try
            {
                FileStream f = new FileStream(fileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, ds);
                f.Close();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }
        public bool readFile(string fileName,out List<order> getDs)
        {
            try
            {
                FileStream f = new FileStream(fileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                getDs = bf.Deserialize(f) as List<order>;
                f.Close();
                return true;
            }
            catch (Exception)
            {
                getDs = null;
                return false;
            }
        }
        public bool PrintReceipt(order order)
        {
            try
            {
                string data = "";
                data += "QUÁN THỨC UỐNG MẸ NẤU 😋\n\n";
                data += $"Mã Đơn Hàng: {order.IdNumber.ToString()}\n\n";
                data += $"Thời gian mua hàng: {order.orderDate.ToString()}\n\n";
                data += $"Họ Tên Khách Hang: {order.customerName.ToString()}\n\n";
                data += $"SĐT: {order.Phone.ToString()}\n\n";
                data += $"Đơn mua: \n\n";
                for (int i = 0; i < order.Drink.Count; i++)
                {
                    data += $"+ {order.Drink[i].ToString()}\n SL: {order.Quantity[i].ToString()}\n\n";
                }
                data += $"Thành tiền: {order.Total} VNĐ\n\n";
                data += "\t\t\tKý Tên: ";
                data += "\t\t\t\tChủ Quán ";
                File.WriteAllText("..\\..\\..\\..\\Receipt.txt", data);
                return true;
            }catch(Exception)
            {
                return false;
            }

        }
    }
}
