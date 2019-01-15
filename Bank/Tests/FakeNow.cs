using System;
using AT;

namespace Tests
{
	public class FakeNow : INow
	{
		private DateTime _now;

		public FakeNow()
		{
			_now = DateTime.Now;
		}

		public void SetFakeNow(DateTime now)
		{
			_now = now;
		}

		public DateTime UtcNow()
		{
			return _now;
		}
	}
}