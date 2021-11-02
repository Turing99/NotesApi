using NotesApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public interface IOwnerCollectionService : ICollectionService<Owner>
    {
        Task<List<Notes>> GetNotesByOwnerId(Guid ownerId);
    }
}
