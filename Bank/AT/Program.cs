using System;
using System.CodeDom;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.Owin.Hosting;

namespace AT
{
	class Program
	{
		static void Main(string[] args)
		{
			ILoanRepository loanrepo = new MemoryLoanRepository();
			var now = new Now();
			var lm = new LoanManager(loanrepo,now);

			var userInput = new UserInput(lm, loanrepo); //a bad practice here where i am passing the repo which is also in lm
			userInput.Get();

		}
	}

	
}
