using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Enums
{
	public enum AgeGenderGroup
	{
		Men,
		Women,
		[Display(Name ="Unisex Adults")]
		UnisexAdults,
		Boys,
		Girls,
		[Display(Name ="Unisex Kids")]
		UnisexKids
	}
}
