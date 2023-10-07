using AutoMapper;
using Foodie.Entities.Entities;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Entities.Repositories {
	public class LocationRepository {

		private readonly CollectionReference _locationRepository;
		private readonly IMapper _mapper;
		public LocationRepository(FirestoreDb firestoreDb, IMapper mapper)
		{
			_locationRepository = firestoreDb.Collection("Location");
			_mapper = mapper;
		}

		public async Task AddAsync(Location location)
			=> await _locationRepository.AddAsync(location);

		public async Task<IEnumerable<Location>> GetAllAsync()
		{
			var snapshot = await _locationRepository.GetSnapshotAsync();
			return _mapper.Map<IEnumerable<Location>>(snapshot);
		}

	}
}
