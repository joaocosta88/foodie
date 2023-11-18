using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodie.Entities.Entities {

	public class Location {

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Id { get; set; }
		public string Coordinates { get; set; }
		public string Rating { get; set; }
		
	}
}
