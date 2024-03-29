﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Extensions
{
	public static class DatetimeExtensions
	{
		public static int CalculateAge(this DateTime dob)
		{
			var today = DateTime.Today;
			var age = today.Year - dob.Year;

			if (dob.Date > today.AddYears(-age)) age--;
			return age;
		}
	}
}
