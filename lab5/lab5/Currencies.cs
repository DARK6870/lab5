using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Currencies
    {
        List<CurrencyData> result = new List<CurrencyData>();
        public void AddCardCurrencyData()
        {
            result.Clear();
            result.Add(new CurrencyData
            {
                name = "USD",
                price = 18.0378,
                buy = 17.9251,
                sell = 18.1653
            });

            result.Add(new CurrencyData
            {
                name = "MDL",
                price = 1,
                buy = 1,
                sell = 1
            });

            result.Add(new CurrencyData
            {
                name = "EUR",
                price = 20.5404,
                buy = 20.3091,
                sell = 20.6114
            });

            result.Add(new CurrencyData
            {
                name = "RUB",
                price = 0.2356,
                buy = 0.2294,
                sell = 0.2406
            });

            result.Add(new CurrencyData
            {
                name = "UAH",
                price = 0.6339,
                buy = 0.4389,
                sell = 0.6817
            });

            result.Add(new CurrencyData
            {
                name = "RON",
                price = 4.1543,
                buy = 4.0598,
                sell = 4.2105
            });
        }

        public void AddMaibData()
        {
            result.Clear();

            // USD
            result.Add(new CurrencyData
            {
                name = "USD",
                price = 18.0378,
                buy = 17.9200,
                sell = 18.1200
            });

            result.Add(new CurrencyData
            {
                name = "MDL",
                price = 1,
                buy = 1,
                sell = 1
            });

            // EUR
            result.Add(new CurrencyData
            {
                name = "EUR",
                price = 20.5404,
                buy = 20.3000,
                sell = 20.5600
            });

            // RUB
            result.Add(new CurrencyData
            {
                name = "RUB",
                price = 0.2356,
                buy = 0.2300,
                sell = 0.2400
            });

            // RON
            result.Add(new CurrencyData
            {
                name = "RON",
                price = 4.1543,
                buy = 4.0700,
                sell = 4.2000
            });

            // UAH
            result.Add(new CurrencyData
            {
                name = "UAH",
                price = 0.6339,
                buy = 0.4400,
                sell = 0.6800
            });

            // GBP
            result.Add(new CurrencyData
            {
                name = "GBP",
                price = 24.5412,
                buy = 24.2000,
                sell = 24.7000
            });

            // CHF
            result.Add(new CurrencyData
            {
                name = "CHF",
                price = 19.7134,
                buy = 19.2000,
                sell = 19.7900
            });
        }

        public List<CurrencyData> GetAllCurrencies()
        {
            return result;
        }

        public List<string> GetCurrencyNames()
        {
            List<string> names = result.Select(p => p.name).ToList();
            return names;
        }

        public CurrencyData ConvertToMDL(string name, double amount)
        {
            CurrencyData data = new CurrencyData();
            CurrencyData? currency = result.FirstOrDefault(p => p.name == name);
            if (currency != null)
            {
                data.buy = amount * currency.buy;
                data.sell = amount * currency.sell;
                data.price = amount * currency.price;
            }

            return data;
        }

        public CurrencyData ConvertFromMDL(string name, double amount)
        {
            CurrencyData data = new CurrencyData();
            CurrencyData? currency = result.FirstOrDefault(p => p.name == name);
            if (currency != null)
            {
                data.buy = amount / currency.buy;
                data.sell = amount / currency.sell;
                data.price = amount / currency.price;
            }

            return data;
        }

    }
}