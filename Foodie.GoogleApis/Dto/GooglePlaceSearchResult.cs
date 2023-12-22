using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.GoogleApis.Dto {
	public class GooglePlaceSearchResult {
		public IEnumerable<GooglePlace> Places { get; set; }
	}
}
