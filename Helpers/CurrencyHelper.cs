namespace Reunite.Helpers
{
    public class CurrencyHelper
    {

        public static int ConvertToMinorUnit(decimal amount, string currency)
        {
            currency = currency.ToLower();

            return currency switch
            {
                "usd" => (int) Math.Round(amount * 100),
                "egp" => (int) Math.Round(amount * 100),
                "eur" => (int) Math.Round(amount * 100),
            };
        }

    }
}