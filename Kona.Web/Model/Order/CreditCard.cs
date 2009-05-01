using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kona.Data
{



    [Serializable]
    public class CreditCard : PaymentMethod
    {

        public string CardType { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string VerificationCode { get; set; }

        public int ExpirationYear{ get; set; }
        public int ExpirationMonth{ get; set; }
        public DateTime Expiration {
            get {
                //sets the date to the last day of the month
                DateTime result = new DateTime(ExpirationYear, ExpirationMonth, DateTime.DaysInMonth(ExpirationYear,ExpirationMonth));

                return result;
            }
            set { }
        }


        public CreditCard() { }


        public CreditCard(string cardType, string nameOnCard, string accountNumber, int expMonth, int expYear, string verificationCode)
        {

            this.Name = nameOnCard;
            this.CardType = cardType;
            this.AccountNumber = accountNumber;
            this.VerificationCode = verificationCode;
            this.ExpirationMonth = expMonth;
            this.ExpirationYear = expYear;
        }


        /// <summary>
        /// Masks the credit card using XXXXXX and appends the last 4 digits
        /// </summary>
        /// <param name="cardNumber">The credit card number</param>
        /// <returns>System.String</returns>
        public string MaskedNumber
        {
            get
            {
                string result = "****";
                if (AccountNumber.Length > 8)
                {
                    string lastFour = AccountNumber.Substring(AccountNumber.Length - 4, 4);
                    result = "**** **** **** " + lastFour;
                }
                return result;
            }

        }


        /// <summary>
        /// Validates the passed-in card, making sure that the card is not expired
        /// and that it passed the Luhn Algorigthm
        /// </summary>
        /// <param name="card">The Credit Card to validate</param>
        public bool IsValid()
        {

            bool isValid = false;

            isValid = Expiration >= DateTime.Today;

            if (isValid)
            {

                //string empty cars, and set to an array
                char[] cardChars = AccountNumber.Replace(" ", "").ToCharArray();

                int sum = 0;
                int currentDigit = 0;
                bool alternate = false;

                //use the Luhn Algorithm to validate the card number
                //http://en.wikipedia.org/wiki/Luhn_algorithm
                //count from left to right
                for (int i = cardChars.Length - 1; i >= 0; i--)
                {
                    if (alternate)
                    {
                        int.TryParse(cardChars[i].ToString(), out currentDigit);
                        currentDigit *= 2;
                        if (currentDigit > 9)
                        {
                            currentDigit -= 9;
                        }
                    }
                    sum += currentDigit;
                    alternate = !alternate;
                }

                isValid=sum % 10 == 0;

            }
            return isValid;

        }
    }
}
