using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using AT;
using Newtonsoft.Json;

namespace Domain
{
	public class FileLoanRepository : ILoanRepository
	{
		

		public FileLoanRepository()
		{
			var jsonContent = File.ReadAllText(@"..\..\data.json");
			if (string.IsNullOrEmpty(jsonContent))
			{
				var fileStorageModel = new FileStorageModel();
				fileStorageModel.AdminAmount = new Dictionary<LoanProduct, double>();
				fileStorageModel.LoanPayouts = new List<LoanPayoutModel>();
				fileStorageModel.LoanrePayments = new List<LoanRePaymentModel>();
				fileStorageModel.AdminAmount.Add(LoanProduct.SmallLoan, 0);
				fileStorageModel.AdminAmount.Add(LoanProduct.LargeLoan, 100);
				fileStorageModel.AdminAmount.Add(LoanProduct.FastLoan, 500);
				//_adminAmount = fileStorageModel.AdminAmount;
				File.WriteAllText(@"..\..\data.json", JsonConvert.SerializeObject(fileStorageModel));
			}

		}

		public void RegisterPayout(LoanPayoutModel payoutModel)
		{

			var fileStorageModel = readFromFile();

			var adminFee = fileStorageModel.AdminAmount[payoutModel.LoanProduct];
			payoutModel.AdministrtionFee = adminFee;
			fileStorageModel.LoanPayouts.Add(payoutModel);


			writeToFile(fileStorageModel);
		}

		private FileStorageModel readFromFile()
		{
			var jsonContent = File.ReadAllText(@"..\..\data.json");
			return JsonConvert.DeserializeObject<FileStorageModel>(jsonContent);
		}

		private void writeToFile(FileStorageModel fileStorageModel)
		{
			File.WriteAllText(@"..\..\data.json", JsonConvert.SerializeObject(fileStorageModel));
		}

		public void RegisterRepayment(LoanRePaymentModel rePaymentModel)
		{
			var fileStorageModel = readFromFile();

			fileStorageModel.LoanrePayments.Add(rePaymentModel);

			writeToFile(fileStorageModel); 
		}

		public LoanPayoutModel GetLoanPayoutModel(string pNum)
		{
			var fileStorageModel = readFromFile();

			return fileStorageModel.LoanPayouts.FirstOrDefault(x => x.PersonNumber == pNum);
		}

		public LoanRePaymentModel GetRepaymentDetails(string pNum)
		{
			var fileStorageModel = readFromFile();

			return fileStorageModel.LoanrePayments.FirstOrDefault(x => x.PersonNumber == pNum);
		}

		public FileStorageModel AllData => readFromFile();
	}

	public class FileStorageModel
	{
		public List<LoanPayoutModel> LoanPayouts { get; set; }
		public List<LoanRePaymentModel> LoanrePayments { get; set; }
		public Dictionary<LoanProduct, double> AdminAmount { get; set; }
	}
}