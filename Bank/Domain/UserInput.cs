using System;
using System.Linq;
using AT;

namespace Domain
{
	public class UserInput
	{
		private readonly LoanManager _loanManager;
		private readonly ILoanRepository _loanRepository;

		public UserInput(LoanManager loanManager, ILoanRepository loanRepository)
		{
			_loanManager = loanManager;
			_loanRepository = loanRepository;
		}

		public void Get()
		{
			var withinOpenHour = true;
			while (withinOpenHour)
			{
				Console.Clear();
				Console.WriteLine("What do you want to do");
				Console.WriteLine("1. Register payout");
				Console.WriteLine("2. Register Repayments");
				Console.WriteLine("3. View existing customers");
				var option = Console.ReadLine();
				var pNum = string.Empty;
				var loanProduct = string.Empty;
				var payoutDate = DateTime.MinValue;
				var input = string.Empty;
				switch (option)
				{
					case "1":
						Console.WriteLine("Enter personal number");
						pNum = Console.ReadLine();
						Console.WriteLine("with plan number (1)Small, (2)large, (3)fast");
						loanProduct = Console.ReadLine();
						Console.WriteLine("Enter the payout date (should be in the future) yyyyMMdd hh:mm");
						var strLine = Console.ReadLine();

						if (!DateTime.TryParseExact(strLine, "yyyyMMdd hh:mm", System.Globalization.CultureInfo.InvariantCulture,
							System.Globalization.DateTimeStyles.AdjustToUniversal, out payoutDate))
						{
							Console.WriteLine("I am in stress going to main menu");
							continue;
						}

						try
						{
							_loanManager.RegisterCustomerPayout(pNum, (LoanProduct)int.Parse(loanProduct), payoutDate);
						}
						catch (RegistrationFailedException e)
						{
							Console.WriteLine(e.Reason);
						}

						Console.WriteLine("Do you want to continue (y) or close the bank (c)");
						input = Console.ReadLine();
						if (input == "c")
							withinOpenHour = false;
						break;

					case "2":
						Console.WriteLine("Enter personal number");
						pNum = Console.ReadLine();
						Console.WriteLine("Enter the repayment date yyyyMMdd hh:mm");

						strLine = Console.ReadLine();
						var repaymentDate = DateTime.MinValue;
						if (!DateTime.TryParseExact(strLine, "yyyyMMdd hh:mm", System.Globalization.CultureInfo.InvariantCulture,
							System.Globalization.DateTimeStyles.AdjustToUniversal, out repaymentDate))
						{
							Console.WriteLine("I am in stress going to main menu");
							continue;
						}
						try
						{
							_loanManager.RegisterCustomerRepayment(pNum, repaymentDate);
						}
						catch (RegistrationFailedException e)
						{
							Console.WriteLine(e.Reason);
						}

						Console.WriteLine("Do you want to continue (y) or close the bank (c)");
						input = Console.ReadLine();
						if (input == "c")
							withinOpenHour = false;
						break;

					case "3":
						Console.Clear();

						var allData = _loanRepository.AllData;
						var counter = 1;
						foreach (var payout in allData.LoanPayouts)
						{
							Console.WriteLine("===================================================");
							Console.WriteLine("Customer " + counter++);
							Console.WriteLine("  Payout information");
							Console.WriteLine("    Personal Number :" + payout.PersonNumber);
							Console.WriteLine("    Payout date: " + payout.PayoutDate);
							Console.WriteLine("    loan product: " + payout.LoanProduct);
							Console.WriteLine("    loan product Number : " + (int)payout.LoanProduct);

							var repayment = allData.LoanrePayments.FirstOrDefault(x => x.PersonNumber.Equals(payout.PersonNumber));
							if (repayment != null)
							{
								Console.WriteLine("  Repayment Information");
								Console.WriteLine("    Repayment date :" + repayment.RepaymentDate);
								Console.WriteLine("    Repayment amount :" + repayment.RepaymentAmount);
							}
							else
							{
								Console.WriteLine("  There is no repayment information");
							}
						}

						Console.WriteLine("Do you want to continue (y) or close the bank (c)");
						input = Console.ReadLine();
						if (input == "c")
							withinOpenHour = false;
						break;
				}
			}
		}
	}
}