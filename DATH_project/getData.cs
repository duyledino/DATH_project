using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATH_project.Components;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace DATH_project
{
    public class getData
    {
        static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static readonly string ApplicationName = "Your Application Name";
        static readonly string SpreadsheetId = "1J6Z6nuvSYQQPOrFUqMHbc08EbTdaJrrqVJQ3TxSX8lI";
        static readonly string SheetName = "Form Responses 1"; // Adjust the sheet name as needed
        static SheetsService service;
        public void get(List<order> list)
        {
            GoogleCredential credential;

            // Load the credentials from a JSON file
            using (var stream = new FileStream("..\\..\\..\\..\\json\\menu-439207-d7e027343471.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            // Create the Google Sheets API service
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Read data from the Google Sheet
            ReadEntries(list);
        }
        public double UpdateId(List<order> orders)
        {
            return orders.Count == 0 ?  1 : orders[orders.Count - 1].IdNumber + 1;

        }
        public void ReadEntries(List<order> list)
        {
            List<order> orders = new List<order>();
            Functional func = new Functional();
            var range = $"{SheetName}!A1:G"; // Adjust range as needed
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;
            if (values != null && values.Count > 0)
            {
                for(int i = 1; i < values.Count; i++)
                {
                    int j = 0;
                    var row = values[i];
                    double idNumber = UpdateId(orders);
                    DateTime time;
                    time = DateTime.ParseExact(row[j++].ToString(),"MM/dd/yyyy HH:mm:ss", null);
                    String name = row[j++].ToString();
                    String phone = row[j++].ToString();
                    var getDrink = values[0];
                    List<WidgetData> drinks = new List<WidgetData>();List<string> quantity = new List<string>();
                    int count = 0;
                    for(int k=0;k<3;k++,j++)
                    {
                        if (row[j].ToString() != "")
                        {
                            WidgetData temp  = new WidgetData($"SP0{count++}", values[0][j].ToString() , 25000, int.Parse(row[j].ToString()));
                            drinks.Add(temp);
                            quantity.Add(row[j].ToString());
                        }
                    }
                    String total = row[j].ToString();
                    orders.Add(new order(idNumber,name, phone, drinks, quantity, time,total));
                }
            }
            func.writeFile("qrData.dat",orders);
        }
    }
}
