using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Entities.Entities {

	public class Location {
		public string Id { get; set; }
		public string Coordinates { get; set; }
		public string Rating { get; set; }
		
		public Location()
		{ 

		}
	}
}
