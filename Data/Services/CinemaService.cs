using System;
using eTicketing.Data.Base;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public class CinemaService:EntityBaseRepository<Cinema>,ICinemaService
	{
		public CinemaService(AppDbContext context):base(context) {}
	}
}

