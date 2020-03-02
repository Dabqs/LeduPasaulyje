using LeduPasaulyjeData.Library.Helpers;
using LeduPasaulyjeData.Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library
{
    public class InvoiceDataAccess
    {
       // readonly string invoiceNoJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), InvoiceNumber.json"));
        readonly string invoiceNoJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\InvoiceNumber.json"));

        public InvoiceModel GetInvoiceData()
        {
            if (!File.Exists(invoiceNoJsonFilePath))
            {
                throw new FileNotFoundException($"Nepavyko rasti failo '{invoiceNoJsonFilePath}'. Jeigu failas egzistuoja, įsitikinkite, kad jo pavadinimas yra teisingas.");
            }
            return JsonConvert.DeserializeObject<InvoiceModel>(JsonHelpers.GetJsonString(invoiceNoJsonFilePath));
        }
        public void UpdateInvoiceData()
        {
            InvoiceModel invoiceModel = GetInvoiceData();
            invoiceModel.InvoiceNo++;
            invoiceModel.LastSavedNo++;

            string jsonOutput = JsonConvert.SerializeObject(invoiceModel, Formatting.Indented);
            File.WriteAllText(invoiceNoJsonFilePath, jsonOutput);
        }
    }
}
