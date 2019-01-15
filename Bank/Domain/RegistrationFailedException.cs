using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class RegistrationFailedException : Exception
	{
		//could add reason here to extent it on different fields
		public string Reason { get; set; }
	}
}
