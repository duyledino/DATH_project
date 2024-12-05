using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DATH_project.Components;

namespace DATH_project
{
    [Serializable]
    public class order
    {
        private double idNumber;
        private string nameOfCustomer;
        private List<WidgetData> drink;
        private String phone;
        private DateTime date;
        private List<string> quantity; // lưu số lượng lấy từ google form
        private String total;
        private int totalQuantity;
        public String Total { get => total; set => total = value; }
        public List<WidgetData> Drink { get => drink; set => drink = value; }
        public String OrderId { get {
                return this.idNumber >= 10 ? $"DH{this.idNumber}" : $"DH0{this.IdNumber}";
            }
            set { idNumber = double.Parse(value.Remove(0,2)); }
        }
        public double IdNumber { get => idNumber; set => idNumber = value; }
        public int TotalQuantity
        {
            get { return totalQuantity; }
        }
        public String customerName {
            get { return nameOfCustomer; } set { nameOfCustomer = value; }
        }
        public DateTime orderDate {
            get { return date; } set { date = value; }
        }
        public List<string> Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public string Phone
        {
            get { return phone; } set { phone = value; }
        }
        public bool Find(String find)
        {
            return this.OrderId == find ? true : false;
        }
        public int getTotalQuantity(List<WidgetData> drinks)
        {
            int total = 0;
            for (int i = 0; i < drinks.Count; i++)
            {
                total += drinks[i].quantity;
            }
            return total;
        }

        public order() { }
        public order(double idNumber,string nameOfCustomer,String phone, List<WidgetData> drink,List<string> quantity, DateTime date,String total)
        {
            this.idNumber = idNumber;
            this.nameOfCustomer = nameOfCustomer;
            this.phone = phone;
            this.Drink = drink;
            this.date = date;
            this.quantity = quantity;
            this.Total = total;
            this.totalQuantity = getTotalQuantity(this.drink);
        }
    }
}
