using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library.Helpers
{
    public static class JsonHelpers
    {
        public static string GetJsonString(string jsonFilePath)
        {
            try
            {
                return File.ReadAllText(jsonFilePath);
            }
            catch (Exception)
            {
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"Nepavyko įkelti produktų. Failas \"{jsonFilePath}\" nerastas.");
                }
                else
                {
                    throw new Exception($"Nepavyko įkelti produktų. Patikrinkite, ar failas \"{jsonFilePath}\" nėra atidarytas. Jeigu yra, uždarykite jį.");
                }
            }
        }

    }
}
